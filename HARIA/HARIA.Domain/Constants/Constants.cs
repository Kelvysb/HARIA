namespace HARIA.Domain.Constants
{
    public static class ClaimsNames
    {
        public static string PERMISSIONS => "permissions";
    }

    public static class Permissions
    {
        public static string SERVICE => "SERVICE";

        public static string DASHBOARD => "DASHBOARD";

        public static string CONFIGURE => "CONFIGURE";

        public static string KIOSK => "KIOSK";
    }

    public static class Templates
    {
        public static string LOG_TEMPLATE => "{source};{message};{detail}";
    }
}