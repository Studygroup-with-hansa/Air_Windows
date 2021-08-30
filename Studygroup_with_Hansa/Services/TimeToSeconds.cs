using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studygroup_with_Hansa.Services
{
    class TimeToSeconds
    {
        public static int ToSeconds(int hour, int minute, int second)
        {
            return hour * 60 * 60 + minute * 60 + second;
        }

        public static int[] FromSeconds(int second)
        {
            int[] time = { 0, 0, 0 };

            time[0] += second / 3600;
            time[1] += second % 3600 / 60;
            time[2] += second % 3600 % 60;

            return time;
        }
    }
}
