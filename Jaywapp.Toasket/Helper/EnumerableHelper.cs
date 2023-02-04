using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Jaywapp.Toasket.Helper
{
    public static class EnumerableHelper
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> enumerable)
        {
            return new ObservableCollection<T>(enumerable);
        }
    }
}
