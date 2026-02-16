using AppEscritorioRHM.Core.Entities;
using AppEscritorioRHM.Core.Interfaces.Infrastructure;
using AppEscritorioRHM.Core.Utilities;
using Microsoft.Data.Sqlite;
using Newtonsoft.Json;

namespace AppEscritorioRHM.Infrastructure.Data
{
    public class UserConnectionSqlite : IUserConnecction
    {
        // Documentación códigos de error sqlite: https://www.sqlite.org/rescode.html

        // ---------------------------------------------------------
        // LOGIN: Recupera usuario, verifica pass y deserializa JSON
        // ---------------------------------------------------------
        public async Task<(ContextResult Result, UserProfile? User)> GetUserAsync(string username, string password)
        {
            try
            {
                using (var connection = new SqliteConnection(DatabaseHelper.ConnectionString))
                {
                    await connection.OpenAsync();

                    var command = connection.CreateCommand();
                    command.CommandText = @"
                        SELECT UserID, UserName, PasswordHashed, ProjectSelected, ProjectsConfiguredJson 
                        FROM Users 
                        WHERE UserName = @username";

                    command.Parameters.AddWithValue("@username", username);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            var userId = reader.GetString(0);
                            var dbUser = reader.GetString(1);
                            var dbHash = reader.GetString(2);
                            var dbProjectSelected = reader.IsDBNull(3) ? null : reader.GetString(3);

                            // Leer y Deserializar la estructura compleja (JSON)
                            var dbJsonProjects = reader.IsDBNull(4) ? "[]" : reader.GetString(4);
                            var projectsList = JsonConvert.DeserializeObject<List<ProjectConfiguration>>(dbJsonProjects) ?? [];

                            // Verificar Contraseña
                            if (SecurityService.Verify(password, dbHash))
                            {
                                // Desencriptar configuración de proyectos
                                if (projectsList.Count > 0)
                                {
                                    projectsList.ForEach(p =>
                                    {
                                        foreach (var connectionToken in p.ConnectionsTokens)
                                        {
                                            connectionToken.ConsumerKey = SecurityService.Decrypt(connectionToken.ConsumerKey, password) ?? string.Empty;
                                            connectionToken.ConsumerSecret = SecurityService.Decrypt(connectionToken.ConsumerSecret, password) ?? string.Empty;
                                        }
                                    });
                                }
                                    

                                var userProfile = new UserProfile
                                {
                                    UserID = userId,
                                    UserName = dbUser,
                                    PasswordHashed = dbHash,
                                    ProjectSelected = dbProjectSelected,
                                    ProjectsConfigured = projectsList
                                };

                                return (new ContextResult { Success = true }, userProfile);
                            }
                            else
                            {
                                return (new ContextResult { Success = false, ErrorMessage = "Contraseña incorrecta." }, null);
                            }
                        }
                        else
                        {
                            return (new ContextResult { Success = false, ErrorMessage = "Usuario no encontrado." }, null);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores de conexión o SQL
                return (new ContextResult { Success = false, ErrorMessage = $"Error de base de datos: {ex.Message}" }, null);
            }
        }

        // ---------------------------------------------------------
        // REGISTER: Crea usuario y guarda lista vacía como JSON
        // ---------------------------------------------------------
        public async Task<ContextResult> RegisterUserAsync(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                return new ContextResult { Success = false, ErrorMessage = "Usuario y contraseña son obligatorios." };

            using (var connection = new SqliteConnection(DatabaseHelper.ConnectionString))
            {
                await connection.OpenAsync();

                // Generación de credenciales
                string newHash = SecurityService.Hash(password);
                string newId = Guid.NewGuid().ToString();

                try
                {
                    var command = connection.CreateCommand();
                    command.CommandText = @"
                        INSERT INTO Users (UserID, UserName, PasswordHashed, ProjectSelected, ProjectsConfiguredJson)
                        VALUES (@id, @name, @hash, @sel, @json)";

                    command.Parameters.AddWithValue("@id", newId);
                    command.Parameters.AddWithValue("@name", username);
                    command.Parameters.AddWithValue("@hash", newHash);
                    command.Parameters.AddWithValue("@sel", DBNull.Value);
                    // Inicializamos con un array JSON vacío
                    command.Parameters.AddWithValue("@json", JsonConvert.SerializeObject(new List<ProjectConfiguration>()));

                    await command.ExecuteNonQueryAsync();

                    return new ContextResult { Success = true };
                }
                catch (SqliteException ex) when (ex.SqliteErrorCode == 19)
                {
                    // Error 19 = Constraint Violation (Unique). El usuario ya existe.
                    return new ContextResult { Success = false, ErrorMessage = "El nombre de usuario ya está en uso." };
                }
                catch (Exception ex)
                {
                    return new ContextResult { Success = false, ErrorMessage = $"Error al registrar: {ex.Message}" };
                }
            }
        }

        // ---------------------------------------------------------
        // REMOVE: Borra el usuario de la BBDD
        // ---------------------------------------------------------
        public async Task<ContextResult> RemoveUserAsync(string username, string password)
        {
            // Verificación extra
            var loginResult = await VerifyCredentialsAsync(username, password);
            if (!loginResult.Success)
                return new ContextResult { Success = false, ErrorMessage = "No se pudo verificar la contraseña para actualizar." };

            using (var connection = new SqliteConnection(DatabaseHelper.ConnectionString))
            {
                var loginCheck = await GetUserAsync(username, password);
                if (!loginCheck.Result.Success)
                    return new ContextResult { Success = false, ErrorMessage = "Contraseña incorrecta." };

                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText = "DELETE FROM Users WHERE UserName = @username";
                command.Parameters.AddWithValue("@username", username);

                int affectedRows = await command.ExecuteNonQueryAsync();

                if (affectedRows > 0)
                    return new ContextResult { Success = true };
                else
                    return new ContextResult { Success = false, ErrorMessage = "Usuario no encontrado." };
            }
        }

        public async Task<ContextResult> UpdateUserNameAsync(string oldUsername, string newUserName, string password)
        {
            var loginCheck = await GetUserAsync(oldUsername, password);
            if (!loginCheck.Result.Success)
                return new ContextResult { Success = false, ErrorMessage = "Contraseña incorrecta." };

            using (var connection = new SqliteConnection(DatabaseHelper.ConnectionString))
            {
                await connection.OpenAsync();
                try
                {
                    var command = connection.CreateCommand();
                    command.CommandText = "UPDATE Users SET UserName = @new WHERE UserName = @old";
                    command.Parameters.AddWithValue("@new", newUserName);
                    command.Parameters.AddWithValue("@old", oldUsername);

                    await command.ExecuteNonQueryAsync();
                    return new ContextResult { Success = true };
                }
                catch (SqliteException ex) when (ex.SqliteErrorCode == 19)
                {
                    return new ContextResult { Success = false, ErrorMessage = $"El nombre de usuario {newUserName} ya existe." };
                }
                catch (Exception ex)
                {
                    return new ContextResult { Success = false, ErrorMessage = ex.Message };
                }
            }
        }

        public async Task<ContextResult> SetProjectSelected(UserProfile user, string projectId)
        {
            var index = user.ProjectsConfigured.FindIndex(p => p.ProjectId == projectId);
            if (index == -1)
                return new ContextResult { Success = false, ErrorMessage = "Proyecto no encontrado." };

            using (var connection = new SqliteConnection(DatabaseHelper.ConnectionString))
            {
                await connection.OpenAsync();
                try
                {
                    var command = connection.CreateCommand();
                    command.CommandText = "UPDATE Users SET ProjectSelected = @project WHERE UserID = @id";
                    command.Parameters.AddWithValue("@project", projectId);
                    command.Parameters.AddWithValue("@id", user.UserID);

                    int rows = await command.ExecuteNonQueryAsync();

                    if (rows > 0)
                        return new ContextResult { Success = true };
                    else
                        return new ContextResult { Success = false, ErrorMessage = "No se encontró el usuario para actualizar." };
                }
                catch (Exception ex)
                {
                    return new ContextResult { Success = false, ErrorMessage = ex.Message };
                }
            }
        }

        public async Task<ContextResult> AddProject(UserProfile user, ProjectConfiguration project, string password)
        {
            if (user.ProjectsConfigured.Any(p => p.ProjectId == project.ProjectId))
                return new ContextResult { Success = false, ErrorMessage = "Ya existe un proyecto con ese ID." };
            
            var projectsCloned = CloneProjectList(user.ProjectsConfigured);
            projectsCloned.Add(project);

            return await UpdatePojectsAsync(user.UserName, projectsCloned, password);
        }

        public async Task<ContextResult> UpdateProject(UserProfile user, ProjectConfiguration project, string password)
        {
            var index = user.ProjectsConfigured.FindIndex(p => p.ProjectId == project.ProjectId);
            if (index == -1)
                return new ContextResult { Success = false, ErrorMessage = "No existe un proyecto con ese ID." };

            var projectsCloned = CloneProjectList(user.ProjectsConfigured);
            projectsCloned[index] = (ProjectConfiguration)project.Clone();

            return await UpdatePojectsAsync(user.UserName, projectsCloned, password);
        }

        public async Task<ContextResult> RemoveProjectAsync(UserProfile user, string projectId, string password)
        {
            var index = user.ProjectsConfigured.FindIndex(p => p.ProjectId == projectId);
            if (index == -1)
                return new ContextResult { Success = false, ErrorMessage = "No existe un proyecto con ese ID." };

            var projectsCloned = CloneProjectList(user.ProjectsConfigured);
            projectsCloned.RemoveAt(index);

            return await UpdatePojectsAsync(user.UserName, projectsCloned, password);
        }

        // ---------------------------------------------------------
        // UPDATE: Método CRÍTICO para guardar cambios (Tokens, Proyectos)
        // ---------------------------------------------------------
        /// <summary>
        /// Guarda el estado actual del objeto UserProfile en la base de datos.
        /// Úsalo después de añadir tokens o cambiar configuración.
        /// </summary>
        private async Task<ContextResult> UpdatePojectsAsync(string username, List<ProjectConfiguration> projects, string password)
        {
            if (string.IsNullOrEmpty(username))
                return new ContextResult { Success = false, ErrorMessage = "Usuario inválido." };

            // Verificación extra
            var loginResult = await VerifyCredentialsAsync(username, password);
            if (!loginResult.Success)
                return new ContextResult { Success = false, ErrorMessage = "No se pudo verificar la contraseña para actualizar." };

            try
            {
                using (var connection = new SqliteConnection(DatabaseHelper.ConnectionString))
                {
                    await connection.OpenAsync();

                    var command = connection.CreateCommand();
                    command.CommandText = @"
                        UPDATE Users 
                        SET ProjectsConfiguredJson = @json
                        WHERE UserName = @name";

                    // Realizamos una copia de los proyectos para encriptar los tokens
                    projects.ForEach(p => EncryptProjectConfiguration(p, password));
                    // Convertimos toda la lista de proyectos y tokens a JSON String
                    string projectsEncryptedJson = JsonConvert.SerializeObject(projects);

                    command.Parameters.AddWithValue("@json", projectsEncryptedJson);
                    command.Parameters.AddWithValue("@name", username);

                    int rows = await command.ExecuteNonQueryAsync();

                    if (rows > 0)
                        return new ContextResult { Success = true };
                    else
                        return new ContextResult { Success = false, ErrorMessage = "No se encontró el usuario para actualizar." };
                }
            }
            catch (Exception ex)
            {
                return new ContextResult { Success = false, ErrorMessage = $"Error al guardar cambios: {ex.Message}" };
            }
        }

        public async Task<ContextResult> VerifyCredentialsAsync(string username, string password)
        {
            using (var conn = new SqliteConnection(DatabaseHelper.ConnectionString))
            {
                await conn.OpenAsync();
                var cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT PasswordHashed FROM Users WHERE UserName = @username";
                cmd.Parameters.AddWithValue("@username", username);

                var result = await cmd.ExecuteScalarAsync();

                // El usuario no existe en la BBDD
                if (result == null)
                {
                    return new ContextResult
                    {
                        Success = false,
                        ErrorMessage = "El usuario no existe."
                    };
                }

                string dbHash = result.ToString();

                // El usuario existe, verificamos la contraseña
                if (SecurityService.Verify(password, dbHash))
                {
                    return new ContextResult { Success = true };
                }
                else
                {
                    return new ContextResult
                    {
                        Success = false,
                        ErrorMessage = "La contraseña es incorrecta."
                    };
                }
            }
        }

        private static List<ProjectConfiguration> CloneProjectList(List<ProjectConfiguration> projects) => 
            projects.Select(x => (ProjectConfiguration)x.Clone()).ToList();
        

        private void EncryptProjectConfiguration(ProjectConfiguration project, string password)
        {
            foreach (var connection in project.ConnectionsTokens)
            {
                connection.ConsumerKey = SecurityService.Encrypt(connection.ConsumerKey, password) ?? string.Empty;
                connection.ConsumerSecret = SecurityService.Encrypt(connection.ConsumerSecret, password) ?? string.Empty;
            }
        }
    }
}
