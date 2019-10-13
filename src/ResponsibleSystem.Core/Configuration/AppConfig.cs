namespace ResponsibleSystem.Configuration
{
    public static class AppConfig
    {
        public const string LocalizationSourceName = "ResponsibleSystem";
        public const string ConnectionStringName = "Default";
        public const bool MultiTenancyEnabled = true;

        public static class SettingsNames
        {
            public const string UiTheme = "App.UiTheme";
        }

        public static class User
        {
            public const string DefaultPassword = "123qwe";
            public const string DefaultPasswordEncrypted = "AM4OLBpptxBYmM79lGOX9egzZk3vIQU3d/gFCJzaBjAPXzYIK3tQ2N7X4fcrHtElTw==";
        }

        public static class Defaults
        {
            public const string Language = "en";
            public const string UiTheme = "red";
            public const string MailFromAddress = "admin@mydomain.com";
            public const string MailFromDisplayName = "mydomain.com mailer";

            public const string MasterUserName = "master@ResponsibleSystem.com";
            public const string MasterName = "master";
            public const string MasterSurname = "admin";
            public const string MasterEmail = "master@ResponsibleSystem.com";

            public const string AdminUserName = "admin@ResponsibleSystem.com";
            public const string AdminName = "host";
            public const string AdminSurname = "admin";
            public const string AdminEmail = "admin@ResponsibleSystem.com";
        }

    }
}