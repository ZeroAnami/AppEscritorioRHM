using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace AppEscritorioRHM.Core.Models.Domain
{
    public class Meta
    {
        public string ignore_trailing_slashes { get; set; }
        public string ignore_parameters { get; set; }
        public string ignore_case { get; set; }
        public string pass_on_parameters { get; set; }
        public string redirect_code { get; set; }
        public string inclusion_exclusion_rules { get; set; }
        public string redirect_options { get; set; }
        public string redirection_http_headers { get; set; }
        public RulesGroup1 rules_group1 { get; set; }
        public RulesGroup2 rules_group2 { get; set; }
        public RulesGroup3 rules_group3 { get; set; }
        public RulesGroup4 rules_group4 { get; set; }
        public RulesGroup5 rules_group5 { get; set; }
        public RulesGroup6 rules_group6 { get; set; }
        public RulesGroup7 rules_group7 { get; set; }
        public RulesGroup8 rules_group8 { get; set; }

        public Meta(string redirect_code = "301")
        {
            this.ignore_trailing_slashes = "1";
            this.ignore_parameters = "1";
            this.ignore_case = "1";
            this.pass_on_parameters = string.Empty;
            this.redirect_code = string.IsNullOrWhiteSpace(redirect_code) ? "301" : redirect_code.Trim();
            this.inclusion_exclusion_rules = string.Empty;
            this.redirect_options = string.Empty;
            this.redirection_http_headers = string.Empty;

            this.rules_group1 = new RulesGroup1();
            this.rules_group2 = new RulesGroup2();
            this.rules_group3 = new RulesGroup3();
            this.rules_group4 = new RulesGroup4();
            this.rules_group5 = new RulesGroup5();
            this.rules_group6 = new RulesGroup6();
            this.rules_group7 = new RulesGroup7();
            this.rules_group8 = new RulesGroup8();
        }

        [JsonConstructor]
        private Meta() { }


    }

    public class RulesGroup1
    {
        public string enabled { get; set; }
        public string login_info { get; set; }

        public RulesGroup1(string enabled = "0", string login_info = "")
        {
            this.enabled = string.IsNullOrWhiteSpace(enabled) ? "0" : enabled;
            this.login_info = login_info ?? string.Empty;
        }

        [JsonConstructor]
        private RulesGroup1() { }
    }

    public class RulesGroup2
    {
        public string enabled { get; set; }
        public string role { get; set; }
        public string role_name { get; set; }

        public RulesGroup2(string enabled = "0", string role = "", string role_name = "[]")
        {
            this.enabled = string.IsNullOrWhiteSpace(enabled) ? "0" : enabled;
            this.role = role ?? string.Empty;
            this.role_name = role_name ?? "[]";
        }

        [JsonConstructor]
        private RulesGroup2() { }
    }

    public class RulesGroup3
    {
        public string enabled { get; set; }
        public string referrer { get; set; }
        public string referrer_value { get; set; }
        public string referrer_regex { get; set; }

        public RulesGroup3(string enabled = "0", string referrer = "", string referrer_value = "", string referrer_regex = "0")
        {
            this.enabled = string.IsNullOrWhiteSpace(enabled) ? "0" : enabled;
            this.referrer = referrer ?? string.Empty;
            this.referrer_value = referrer_value ?? string.Empty;
            this.referrer_regex = referrer_regex ?? "0";
        }

        [JsonConstructor]
        private RulesGroup3() { }
    }

    public class RulesGroup4
    {
        public string enabled { get; set; }
        public string agent { get; set; }
        public string agent_value { get; set; }
        public string agent_regex { get; set; }

        public RulesGroup4(string enabled = "0", string agent = "", string agent_value = "", string agent_regex = "0")
        {
            this.enabled = string.IsNullOrWhiteSpace(enabled) ? "0" : enabled;
            this.agent = agent ?? string.Empty;
            this.agent_value = agent_value ?? string.Empty;
            this.agent_regex = agent_regex ?? "0";
        }

        [JsonConstructor]
        private RulesGroup4() { }
    }

    public class RulesGroup5
    {
        public string enabled { get; set; }
        public string cookie { get; set; }
        public string cookie_name { get; set; }
        public string cookie_value { get; set; }
        public string cookie_regex { get; set; }

        public RulesGroup5(string enabled = "0", string cookie = "", string cookie_name = "", string cookie_value = "", string cookie_regex = "0")
        {
            this.enabled = string.IsNullOrWhiteSpace(enabled) ? "0" : enabled;
            this.cookie = cookie ?? string.Empty;
            this.cookie_name = cookie_name ?? string.Empty;
            this.cookie_value = cookie_value ?? string.Empty;
            this.cookie_regex = cookie_regex ?? "0";
        }

        [JsonConstructor]
        private RulesGroup5() { }
    }

    public class RulesGroup6
    {
        public string enabled { get; set; }
        public string ip { get; set; }
        public string ip_value { get; set; }

        public RulesGroup6(string enabled = "0", string ip = "", string ip_value = "")
        {
            this.enabled = string.IsNullOrWhiteSpace(enabled) ? "0" : enabled;
            this.ip = ip ?? string.Empty;
            this.ip_value = ip_value ?? string.Empty;
        }

        [JsonConstructor]
        private RulesGroup6() { }
    }

    public class RulesGroup7
    {
        public string enabled { get; set; }
        public string server { get; set; }
        public string server_value { get; set; }

        public RulesGroup7(string enabled = "0", string server = "", string server_value = "")
        {
            this.enabled = string.IsNullOrWhiteSpace(enabled) ? "0" : enabled;
            this.server = server ?? string.Empty;
            this.server_value = server_value ?? string.Empty;
        }

        [JsonConstructor]
        private RulesGroup7() { }
    }

    public class RulesGroup8
    {
        public string enabled { get; set; }
        public string language { get; set; }
        public string language_value { get; set; }

        public RulesGroup8(string enabled = "0", string language = "", string language_value = "")
        {
            this.enabled = string.IsNullOrWhiteSpace(enabled) ? "0" : enabled;
            this.language = language ?? string.Empty;
            this.language_value = language_value ?? string.Empty;
        }

        [JsonConstructor]
        private RulesGroup8() { }
    }
}
