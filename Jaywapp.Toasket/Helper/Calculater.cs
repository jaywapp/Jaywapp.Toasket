using System;
using System.Collections.Generic;

namespace Jaywapp.Toasket.Helper
{
    public static class Calculater
    {
        public static double Multiply<T>(this IEnumerable<T> items, Func<T, double> selector)
        {
            double value = 1;

            foreach (var item in items)
                value *= selector(item);

            return value;
        }
    }
}
