using BepInEx.Logging;

namespace Core.Shared
{
    public class CoreLogger
    {
        public static ManualLogSource Log { get; set; }

        public static void Info(string message)
        {
            Log.LogInfo(message);
        }

        public static void Info(string caller, string message)
        {
            Log.LogInfo($"{caller}: {message}");
        }

        public static void Warn(string message)
        {
            Log.LogWarning(message);
        }

        public static void Warn(string caller, string message)
        {
            Log.LogWarning($"{caller}: {message}");
        }

        public static void Error(string message)
        {
            Log.LogError(message);
        }

        public static void Error(string caller, string message)
        {
            Log.LogError($"{caller}: {message}");
        }

        public static void Fatal(string message)
        {
            Log.LogFatal(message);
        }

        public static void Fatal(string caller, string message)
        {
            Log.LogFatal($"{caller}: {message}");
        }

        public static void Debug(string message)
        {
            Log.LogDebug(message);
        }

        public static void Debug(string caller, string message)
        {
            Log.LogDebug($"{caller}: {message}");
        }
    }
}
