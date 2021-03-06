﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using MineNET.IO;

namespace MineNET.Manager
{
    public sealed class ConstantClockManager : IDisposable
    {
        private class ClockInstance
        {
            public string Name { get; set; }
            public int ClockTime { get; set; }
            public Stopwatch StopWatch { get; set; } = new Stopwatch();
            public int NextDelayDiff { get; set; }

            public ClockInstance(string name, int clockTime)
            {
                this.Name = name;
                this.ClockTime = clockTime;
            }
        }

        private Dictionary<string, ClockInstance> Datas { get; } = new Dictionary<string, ClockInstance>();

        public ConstantClockManager()
        {
            this.Create("server.update", 1000 / 20);
            this.Create("network.update", 1000 / 100);
        }

        public void Create(string name, int time)
        {
            this.Datas[name] = new ClockInstance(name, time);
        }

        public bool Remove(string name)
        {
            return this.Datas.Remove(name);
        }

        public bool Start(string name)
        {
            if (this.Datas.ContainsKey(name))
            {
                this.Datas[name].StopWatch.Restart();

                return true;
            }

            return false;
        }

        public bool Stop(string name)
        {
            if (this.Datas.ContainsKey(name))
            {
                ClockInstance instance = this.Datas[name];
                instance.StopWatch.Stop();
                int wait = instance.ClockTime - instance.StopWatch.Elapsed.Milliseconds - instance.NextDelayDiff;
                if (wait <= 0)
                {
                    if (Server.Instance.Config.ClockDelayDebug)
                    {
                        Logger.Debug("%server.constantClock.lowTickRate", name);
                    }

                    instance.NextDelayDiff = Math.Abs(wait);
                }
                else
                {
                    instance.NextDelayDiff = 0;
                    Thread.Sleep(wait);
                }

                return true;
            }

            return false;
        }

        public void Dispose()
        {
            this.Datas.Clear();
        }
    }
}
