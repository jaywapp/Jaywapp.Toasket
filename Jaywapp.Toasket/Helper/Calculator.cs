using System;
using System.Collections.Generic;

namespace Jaywapp.Toasket.Helper
{
    public static class Calculator
    {
        /// <summary>
        /// <paramref name="items"/>내의 값(<paramref name="selector"/>을 통해 추출한)을 모두 곱하여 반환합니다.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static double Multiply<T>(this IEnumerable<T> items, Func<T, double> selector)
        {
            double value = 1;

            foreach (var item in items)
                value *= selector(item);

            return value;
        }
    }
}
