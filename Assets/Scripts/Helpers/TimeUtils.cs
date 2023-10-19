using System;
using UnityEngine;

namespace PesPatron.Helpers
{
    public static class TimeUtils
    {
        public static string TimeFormatToMMSS(int time)
        {
            return TimeSpan.FromSeconds(time).ToString(@"mm\:ss");
        }

        public static string TimeFormatToMMSS(float time)
        {
            return TimeSpan.FromSeconds(time).ToString(@"mm\:ss");
        }
    }
}