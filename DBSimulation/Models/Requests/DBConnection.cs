﻿namespace DBSimulation.Models.Requests
{
    public class DBCredentials
    {
        public string? Hostname { get; set; } = string.Empty;
        public string? DatabaseName { get; set; } = string.Empty;
        public int? Port { get; set; } = int.MinValue;
        public string? Username { get; set; } = string.Empty;
        public string? Password { get; set; } = string.Empty;
    }

}
