using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFL.Common.Exceptions
{
    public class NflException : Exception
    {
        public bool IsError { get; private set; } = false;

        public bool IsWarning { get; private set; } = false;

        public bool IsInformation { get; private set; } = false;

        public NflException(string msg, string type = "error") : base(msg)
        {
            if (type == "error") IsError = true;
            else if (type == "warning") IsWarning = true;
            else if (type == "info") IsInformation = true;
            else IsError = true;
        }
    }

    static public class SerilogExtensions
    {
#if DEBUG
        static public void Exception(this ILogger logger, string? key, Exception ex, bool displayTrace = true)
#else
        static public void Exception(this ILogger logger, string key, Exception ex, bool displayTrace = false)
#endif
        {
            if (ex is NflException)
            {
                NflException? nex = ex as NflException;
                string? msg = nex?.Message;
                if (key != null) msg = $"[{key}] {nex?.Message}";

                if (nex?.IsError == true) logger.Error(msg);
                else if (nex?.IsWarning == true) logger.Warning(msg);
                else if (nex?.IsInformation == true) logger.Information(msg);
            }
            else
            {
                string msg = ex.Message;
                if (key != null) msg = $"[{key}] {ex.Message}";
                logger.Error(msg);

                if (ex.InnerException != null)
                {
                    msg = ex.Message;
                    if (key != null) msg = $"[{key}] {ex.InnerException}";
                    logger.Error(msg);
                }
            }

            if (displayTrace == true && ex.StackTrace != null)
            {
                string msg = ex.StackTrace;
                if (key != null) msg = $"[{key}] {ex.StackTrace}";
                logger.Information(msg);
            }
        }
        static public void Exception(this ILogger logger, Exception ex, bool displayTrace = true)
        {
            logger.Exception(null, ex, displayTrace);
        }
    }
}
