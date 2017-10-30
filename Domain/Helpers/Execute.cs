using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Telegram4Net.Domain.Helpers
{
    public static class Execute
    {
        private static ILogger _logger;

        private static ILogger _l
        {
            get
            {
                if (_logger == null)
                    _logger = new LoggerFactory().CreateLogger("Execute logger");
                return _logger;
            }
        }

        public static void BeginOnThreadPool(TimeSpan delay, Action action)
        {
            Task.Run(
                async delegate
                {
                    await Task.Delay(delay);
                    try
                    {
                        action?.Invoke();
                    }
                    catch (Exception e)
                    {
                        _l.LogError($"{e}");
                    }
                });
        }

        public static Task BeginOnThreadPoolAsync(Action action)
        {
            return Task.Run(
                () =>
                {
                    try
                    {
                        action?.Invoke();
                    }
                    catch (Exception e)
                    {
                        _l.LogError($"{e}");
                    }
                });
        }

        public static void BeginOnThreadPool(Action action)
        {
            Task.Run(
                () =>
                {
                    try
                    {
                        action?.Invoke();
                    }
                    catch (Exception e)
                    {
                        _l.LogError($"{e}");
                    }
                });
        }
    }
}