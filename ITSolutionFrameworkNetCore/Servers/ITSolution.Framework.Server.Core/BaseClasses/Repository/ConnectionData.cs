using System.Data.Common;
using System.Diagnostics;

namespace ITSolution.Framework.Core.Server.BaseClasses.Repository
{
    public class ConnectionData
    {
        private Stopwatch _stopwatch;

        public ConnectionData(DbConnection connection)
        {
            _stopwatch = Stopwatch.StartNew();
            this.DbConnection = connection;
        }

        public DbConnection DbConnection { get; private set; }

        public double SecondsOpened
        {
            get { return _stopwatch.Elapsed.TotalSeconds; }
        }

        public void StopTimer()
        {
            _stopwatch.Stop();
        }

        public bool HasExecuting { get; set; }
    }
}