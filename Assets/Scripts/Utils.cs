using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RandomGenerator = UnityEngine.Random;

namespace Assets.Scripts
{
    public static class Utils
    {
        public static int[] RandomRangeWithRestrictions(int start, int end, int[] restrictions)
        {
            var intervals = new int[restrictions.Length + 1];
            for (var i = 0; i < intervals.Length; i++)
            {
                intervals[i] = RandomGenerator.Range(i == 0 ? start : restrictions[i - 1] + 1,
                    i == intervals.Length - 1 ? end : restrictions[i] - 1);
            }
            return intervals;
        }

        public static T GetRandomFromEnum<T>()
        {
            var values = Enum.GetValues(typeof(T));
            return (T)values.GetValue(RandomGenerator.Range(0, values.Length));
        }
    }
}
