using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace gameOfLife
{
    public static class Constants
    {
        public static bool Debug = false;
        public static readonly bool[] ComeAlive =
            {false, false, false, true, false, false, false, false, false};
        public static readonly bool[] StayAlive =
            {false, false, true, true, false, false, false, false, false};

      //  public static bool WrapAround { get; } = false;
    }

    public interface ISquare
    {
        bool StateIsAlive { get; set; }
        //bool NextState { set; }
        void PrepareNextState();
        void ShiftToNextState();
    }

    public interface IBoard
    {
        //ISquare[,] GridSquares { get; }
        bool[,] GridSquaresAsBools { get; set; }
        //IEnumerable<ISquare> GridSquaresAsEnumerable { get; }
        void NextState();
        void SetPortionOfBoard(bool[,] newField, int xOfTopLeftCorner = 0, int yOfTopLeftCorner = 0);
    }

    public interface IBoardDisplay
    {

    }
}