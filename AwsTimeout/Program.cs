using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using System.Runtime.InteropServices;

internal struct LASTINPUTINFO
{
    public uint cbSize;
    public uint dwTime;
}

namespace AwsTimeout
{
    internal class Program
    {
        [DllImport("User32.dll")]
        private static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);

        public static uint IdleTime()
        {
            LASTINPUTINFO lastinputinfo = new LASTINPUTINFO();
            lastinputinfo.cbSize = (uint)Marshal.SizeOf(lastinputinfo);
            GetLastInputInfo(ref lastinputinfo);
            return ((uint)Environment.TickCount - lastinputinfo.dwTime);
        }

        static void Main(string[] args)
        {
            while(true)
            {
                uint idle = IdleTime();
                Console.WriteLine("Idle time is {0:D}", idle);
                Thread.Sleep(5000);
            }
        }
    }
}
