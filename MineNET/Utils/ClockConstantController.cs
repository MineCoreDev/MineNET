using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MineNET.Utils
{
    public static class ClockConstantController
    {
        static ClockConstantController()
        {
            ClockConstantController.CreateController("server", 1000 / 20);
            ClockConstantController.CreateController("network", 1000 / 20);
        }

        private class ControllerInstance
        {
            public string Name { get; set; }
            public int ClockTime { get; set; }
            public Stopwatch StopWatch { get; set; } = new Stopwatch();

            public ControllerInstance(string name, int clockTime)
            {
                this.Name = name;
                this.ClockTime = clockTime;
            }
        }

        private static Dictionary<string, ControllerInstance> instances = new Dictionary<string, ControllerInstance>();

        public static bool CreateController(string name, int time)
        {
            try
            {
                instances.Add(name, new ControllerInstance(name, time));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool RemoveController(string name)
        {
            try
            {
                instances.Remove(name);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static void Start(string name)
        {
            ControllerInstance instance = instances[name];
            instance.StopWatch.Restart();
        }

        public static Task Stop(string name)
        {
            ControllerInstance instance = instances[name];
            instance.StopWatch.Stop();
            long wait = instance.StopWatch.ElapsedMilliseconds - instance.ClockTime;
            if (wait <= 0)
            {
                return Task.Delay(1);
            }
            else
            {
                return Task.Delay((int) wait);
            }
        }

        //TODO: 
        /*public static Task SyncStop(string name, string syncName)
        {

        }*/
    }
}
