using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace MineNET.Manager
{
    public sealed class ConstantClockManager : IDisposable
    {
        private class ClockInstance
        {
            public string Name { get; set; }
            public int ClockTime { get; set; }
            public Stopwatch StopWatch { get; set; } = new Stopwatch();

            public ClockInstance(string name, int clockTime)
            {
                this.Name = name;
                this.ClockTime = clockTime;
            }
        }

        private Dictionary<string, ClockInstance> Datas { get; } = new Dictionary<string, ClockInstance>();

        public void Create(string name, int time)
        {
            this.Datas[name] = new ClockInstance(name, time);
        }

        public bool Remove(string name)
        {
            return this.Datas.Remove(name);
        }

        public void Start(string name)
        {
            this.Datas[name].StopWatch.Restart();
        }

        public void Stop(string name)
        {
            ClockInstance instance = this.Datas[name];
            instance.StopWatch.Stop();
            int wait = instance.StopWatch.Elapsed.Milliseconds - instance.ClockTime;
            if (wait <= 0)
            {
                Thread.Sleep(1);
            }
            else
            {
                Thread.Sleep(wait);
            }
        }

        public void Dispose()
        {
            this.Datas.Clear();
        }
    }
}
