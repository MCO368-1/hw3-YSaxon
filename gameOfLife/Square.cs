using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace gameOfLife
{
    public static class Constants
    {
        public static readonly int[] ComeAliveArray=
        {
            3
        };
        public static readonly ISet<int> ComeAlive = new HashSet<int>(ComeAliveArray);

        public static readonly int[] StayAliveArray=
        {
            2,
            3
        };
        public static readonly ISet<int> StayAlive = new HashSet<int>(StayAliveArray);
    }

    public interface ISquare
    {
        bool StateIsAlive { get; }
        bool NextState { set; }
        void ShiftToNextState();
        IEnumerable<ISquare> Neighbors { get; set; }

    }

    public static class ExtendsISquare
    {
        public static bool GetNextState(this ISquare square)
        {
            var aliveAround = square.Neighbors.AsParallel().Select(n => n.StateIsAlive).Count();
            return square.StateIsAlive
                ? Constants.StayAlive.Contains(aliveAround)
                : Constants.ComeAlive.Contains(aliveAround);
        }
    }
}