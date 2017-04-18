namespace Wiki.Sms.Constants
{
    public class Constants
    {
        public static string CONNECTIONSTRING = Wiki.Service.Configuration.ConfigurationContainer.Configuration["ConnectionString"];
        public static string BASEMODEMURL = Wiki.Service.Configuration.ConfigurationContainer.Configuration["ModemUrl"];
        public const string GETURLPREFIX = "/goform/goform_get_cmd_process";
        public const string POSTURLPREFIX = "/goform/goform_set_cmd_process";
    }
}
