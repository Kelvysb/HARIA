﻿namespace HARIA.Domain.Constants
{
    public static class DeviceType
    {
        public static string SENSOR => "SENSOR";

        public static string ACTUATOR => "ACTUATOR";
    }

    public static class TimeMode
    {
        public static string REALTIME => "REALTIME";

        public static string SIMULATED => "SIMULATED";
    }

    public static class ScenarioMode
    {
        public static string MANUAL => "MANUAL";

        public static string AUTOMATIC => "AUTO";
    }

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

    public static class StateDefaultKeys
    {
        public static string ACTIVE_SCENARIO => "ACTIVE_SCENARIO";

        public static string SCENARIO_MODE => "SCENARIO_MODE";

        public static string TIME_MODE => "TIME_MODE";

        public static string TIME_OFFSET => "TIME_OFFSET";
    }
}