using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Jaywapp.Toasket.Helper
{
    public static class EnumerableHelper
    {
        /// <summary>
        /// <paramref name="enumerable"/>을 <see cref="ObservableCollection{T}"/>형식으로 반환합니다.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <returns></returns>
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> enumerable)
        {
            return new ObservableCollection<T>(enumerable);
        }
    }
}
