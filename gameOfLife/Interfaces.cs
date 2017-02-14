using System;
using System.Collections.Generic;
using System.Data;
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
    public static class Extensions
    {
        public static DataSet MultidimensionalArrayToDataSet(string[,] input)
        {
            var dataSet = new DataSet();
            var dataTable = dataSet.Tables.Add();
            var iFila = input.GetLongLength(0);
            var iCol = input.GetLongLength(1);

            //Fila
            for (var f = 1; f < iFila; f++)
            {
                var row = dataTable.Rows.Add();
                //Columna
                for (var c = 0; c < iCol; c++)
                {
                    if (f == 1) dataTable.Columns.Add(input[0, c]);
                    row[c] = input[f, c];
                }
            }
            return dataSet;
        }
//        public static bool GetNextState(this ISquare square)
//        {
//            var aliveAround = square.Neighbors.AsParallel().Select(n => n.StateIsAlive).Count();
//            return square.StateIsAlive
//                ? Constants.StayAlive.Contains(aliveAround)
//                : Constants.ComeAlive.Contains(aliveAround);
//        }
    }
}