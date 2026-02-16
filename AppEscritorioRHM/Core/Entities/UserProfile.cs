using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppEscritorioRHM.Core.Entities
{
    public class UserProfile
    {
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string PasswordHashed { get; set; }

        /// <summary>
        /// Último proyecto seleccionado por el usuario.
        /// </summary>
        public string ProjectSelected { get; set; }

        /// <summary>
        /// Lista de proyectos asociadas al usuario.
        /// </summary>
        public List<ProjectConfiguration> ProjectsConfigured { get; set; }
    }

    public class ProjectConfiguration  : ICloneable
    {
        public string ProjectId { get; set; } = Guid.NewGuid().ToString();
        public string ProjectName { get; set; } 
        public string EcommerceIdSelected { get; set; }
        public string Domain { get; set; }
        public List<Tokens> ConnectionsTokens { get; set; }

        public object Clone()
        {
            List<Tokens> clonedConnectionsTokens = [];
            ConnectionsTokens.ForEach(token => clonedConnectionsTokens.Add((Tokens)token.Clone()));
            return new ProjectConfiguration
            {
                ProjectId = this.ProjectId,
                ProjectName = this.ProjectName,
                EcommerceIdSelected = this.EcommerceIdSelected,
                Domain = this.Domain,
                ConnectionsTokens = clonedConnectionsTokens
            };
        }
    }

    public class Tokens : ICloneable
    {
        public string EndpointId { get; set; }
        public string ConsumerKey { get; set; }
        public string ConsumerSecret { get; set; }

        public object Clone()
        {
            return new Tokens
            {
                EndpointId = this.EndpointId,
                ConsumerKey = this.ConsumerKey,
                ConsumerSecret = this.ConsumerSecret
            };
        }
    }
}
