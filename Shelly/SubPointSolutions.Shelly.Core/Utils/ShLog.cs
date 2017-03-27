namespace SubPointSolutions.Shelly.Core.Utils
{
    internal class ShLog
    {
        #region methods
        public static void Log(string message)
        {
            var logService = ShServiceContainer.Instance.TraceService;

            logService.Information(0, message);
        }
        #endregion
    }
}
