using System;

namespace Jaywapp.Toasket.Interface
{
    public interface IMatch
    {
        int No { get; }
        DateTime Time { get; }
        string Home { get; }
        string Away { get; }
        double Win { get; }
        double Draw { get; }
        double Lose { get; }
    }
}
