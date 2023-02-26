using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFL.Common.Logging
{
    /// <summary>
    /// Provides a LoggerFactory and ILogger instance to requesting classes.
    /// </summary>
    public static class ApplicationLogger
    {
        /// <summary>
        /// Gets LoggerFactory that can be configured to support logging providers.
        /// </summary>
        public static ILoggerFactory LoggerFactory { get; } = new LoggerFactory();

        /// <summary>
        /// Returns a logger instance that can be consumed by each requesting class.
        /// </summary>
        /// <returns>Logger instance to be used by requesting class.</returns>
        public static ILogger CreateSessionLogger()
        {
            return LoggerFactory.CreateLogger("session");
        }

        /// <summary>
        /// Returns a logger instance that can be consumed by each requesting class.
        /// </summary>
        /// <returns>Logger instance to be used by requesting class.</returns>
        public static ILogger CreateAppLogger()
        {
            return LoggerFactory.CreateLogger("app");
        }
    }
}
