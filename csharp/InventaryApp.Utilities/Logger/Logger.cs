using Serilog;
using Serilog.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Permissions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventaryApp.Utilities.Logger
{
    public static class Logger
    {
        static Logger()
        {
            CreateLogFolder();

            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .Enrich.FromLogContext()
            .Enrich.WithExceptionDetails()
            .WriteTo.File(@"logs/logfile-.log", rollingInterval: RollingInterval.Day, rollOnFileSizeLimit: false, buffered: false)
            .CreateLogger();
        }

        static void CreateLogFolder()
        {
            Directory.SetCurrentDirectory(System.AppDomain.CurrentDomain.BaseDirectory);

            var directory = AppDomain.CurrentDomain.BaseDirectory + @"\logs";
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);

                FileIOPermission fileIOPermission = new FileIOPermission(FileIOPermissionAccess.Read, directory);
                fileIOPermission.AddPathList(FileIOPermissionAccess.AllAccess, directory);

            }
        }

        public static void CloseAndFlush()
        {
            Log.CloseAndFlush();
        }

        #region logger

        public static void LogDebug(string msg)
        {

            Log.Debug(msg);
        }

        public static void LogDebug(string msg, params object[] parameters)
        {

            Log.Debug(msg, parameters);
        }

        public static void LogDebug(Exception e, string msg)
        {

            Log.Debug(e, msg);
        }

        public static void LogDebug(Exception e, string msg, params object[] parameters)
        {

            Log.Debug(e, msg, parameters);
        }

        public static void LogWarning(string msg)
        {

            Log.Warning(msg);
        }

        public static void LogWarning(string msg, params object[] parameters)
        {

            Log.Warning(msg, parameters);
        }

        public static void LogWarning(Exception e, string msg)
        {

            Log.Warning(e, msg);
        }

        public static void LogWarning(Exception e, string msg, params object[] parameters)
        {

            Log.Warning(e, msg, parameters);
        }


        public static void LogInfo(string msg)
        {

            Log.Information(msg);
        }

        public static void LogInfo(string msg, params object[] parameters)
        {

            Log.Information(msg, parameters);
        }

        public static void LogInfo(Exception e, string msg)
        {

            Log.Information(e, msg);
        }

        public static void LogInfo(Exception e, string msg, params object[] parameters)
        {

            Log.Information(e, msg, parameters);
        }

        public static void LogError(string msg)
        {

            Log.Error(msg);
        }

        public static void LogError(string msg, params object[] parameters)
        {

            Log.Error(msg, parameters);
        }

        public static void LogError(Exception e, string msg)
        {

            Log.Error(e, msg);
        }

        public static void LogError(Exception e, string msg, params object[] parameters)
        {

            Log.Error(e, msg, parameters);
        }
        #endregion logger
    }
}
