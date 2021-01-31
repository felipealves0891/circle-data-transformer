using System;
using System.Diagnostics;
using System.Timers;

namespace Circle.Core.DataTransformer
{
    public abstract class TransformationDiagnostic : IDisposable
    {
        private int[] _before;

        private Stopwatch _stopwatch;

        private Timer _timer;

        public TransformationDiagnostic()
        {
            _before = new int[3] 
            { 
                GC.CollectionCount(0), 
                GC.CollectionCount(1), 
                GC.CollectionCount(3) 
            };

            _stopwatch = new Stopwatch();
            _stopwatch.Start();

            SetTimer();
        }

        private void SetTimer()
        {
            // Create a timer with a two second interval.
            _timer = new System.Timers.Timer(1000);
            // Hook up the Elapsed event for the timer. 
            _timer.Elapsed += OnDiagnose;
            _timer.AutoReset = true;
            _timer.Enabled = true;
        }

        public long Collected(int gen)
        {
            return GC.CollectionCount(gen) - _before[gen];
        }

        public void Dispose()
        {
            _stopwatch.Stop();
        }

        public long ElapsedTimeInMileseconds()
        {
            return _stopwatch.ElapsedMilliseconds;
        }

        public long ElapsedTimeInSeconds()
        {
            return _stopwatch.ElapsedMilliseconds / 1000;
        }

        public long MemoryUsed()
        {
            return Process.GetCurrentProcess().WorkingSet64 / 1024 / 1024;
        }

        protected virtual void OnDiagnose(Object source, ElapsedEventArgs e)
        {
            EventHandler handler = Diagnose;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler Diagnose;
    }
}
