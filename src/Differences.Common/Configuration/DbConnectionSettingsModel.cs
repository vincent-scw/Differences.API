namespace Differences.Common.Configuration
{
    public class DbConnectionSettingsModel
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConnectionString { get; set; }
        public bool UseSsL { get; set; }

        public string Database { get; set; }
    }
}
