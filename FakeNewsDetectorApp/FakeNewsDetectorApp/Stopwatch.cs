using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeNewsDetectorApp
{
    public class Stopwatch
    {
        private DateTime startTime;
        private DateTime stopTime;
        private bool running = false;

        public void Start()
        {
            this.startTime = DateTime.Now;

            this.running = true;
        }

        public void Stop()
        {
            this.stopTime = DateTime.Now;

            this.running = false;
        }

        public double GetElapsedTime()
        {
            TimeSpan interval;
            if (running)
                interval = DateTime.Now - startTime;
            else
                interval = stopTime - startTime;

            return interval.TotalMilliseconds;
        }
    }
}
