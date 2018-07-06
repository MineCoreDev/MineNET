using System;

namespace MineNET.Reports
{
    public interface IReport
    {
        string Title { get; }
        string Description { get; }

        Exception Cause { get; }

        DateTime Time { get; }

        string OSData { get; }

        void SendReport();
    }
}
