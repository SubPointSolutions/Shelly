using System;
using System.ComponentModel;

namespace SubPointSolutions.Shelly.Core.Utils
{
    public class ShAsyncOptions
    {
        public Action Do { get; set; }
        public Action<object, RunWorkerCompletedEventArgs> OnError { get; set; }
        public Action OnDone { get; set; }
    }

    public static class ShAsync
    {
        #region methods

        //public static void DoAsync(Action action)
        //{
        //    DoAsync(action, null);
        //}

        //public static void DoAsync(Action action, Action completed)
        //{
        //    DoAsync(new ShAsyncOptions
        //    {

        //    });
        //}

        public static void DoAsync(ShAsyncOptions options)
        {
            var worker = new BackgroundWorker
            {
                WorkerSupportsCancellation = true,
                WorkerReportsProgress = true
            };

            worker.RunWorkerCompleted += (s, e) =>
            {
                if (e.Error != null)
                {
                    if (options.OnError != null)
                        options.OnError(s, e);

                    throw e.Error;
                }

                if (options.OnDone != null)
                    options.OnDone();
            };

            worker.DoWork += (s, e) =>
            {
                if (options.Do != null)
                    options.Do();
            };

            worker.RunWorkerAsync();
        }

        #endregion
    }
}
