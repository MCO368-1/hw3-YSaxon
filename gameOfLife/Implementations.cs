using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Numerics;
using System.Reflection;
using FsAlg.Generic;
using Microsoft.FSharp.Core;
using NUnit.Framework.Constraints;

using VectorClass;
using Microsoft.FSharp.Collections;

namespace gameOfLife
{

    public class Square : ISquare
    {
        private int _x, _y;
        private bool _stateIsAlive;
        private bool _nextState;
        private IEnumerable<ISquare> _neighbors;

        public Square(int x, int y)
        {
            _x = x;
            _y = y;
        }

        public override string ToString()
        {
            return //_x.ToString()+_y+
                   (StateIsAlive?"*":" ");
        }

        public bool StateIsAlive
        {
            get { return _stateIsAlive; }

            set { _stateIsAlive = value; }
        }

        public bool NextState
        {
            get { return _nextState; }
            set { _nextState = value;
                NextStatePrepared = true;
            }
        }

        public bool NextStatePrepared { get; private set; }

        public void PrepareNextState()
        {
            var aliveAround = Neighbors.Count(n => n.StateIsAlive);


            _nextState = _stateIsAlive
                ? Constants.StayAlive[aliveAround]
                : Constants.ComeAlive[aliveAround];

            if (Constants.Debug && aliveAround >= 2)
            {
                Console.WriteLine(this);
                Console.WriteLine(String.Join(", ",_neighbors.Select(x=>x.ToString()).ToArray()));
                Console.WriteLine(aliveAround);
                Console.WriteLine(_stateIsAlive);
                Console.WriteLine(_nextState);
                Console.WriteLine("---");
            }

            NextStatePrepared = true;
        }

        public void ShiftToNextState()
        {
            if (!NextStatePrepared) throw new Exception("must prepare next state before calling shift");
            _stateIsAlive = _nextState;
            NextStatePrepared = false;
        }

        public IEnumerable<ISquare> Neighbors
        {
            get { return _neighbors; }
            set { _neighbors = value; }
        }
    }

    public class Board : IBoard
    {
        public int RowsY { get; private set; }

        public int ColsX { get; private set; }


        public ISquare[,] GridSquares { get; }
        public IEnumerable<ISquare> GridSquaresAsEnumerable => GridSquares.Cast<ISquare>();

        public bool[,] GridSquaresAsBools
        {
            get {  return Array2DModule.Map(
                FSharpFunc<ISquare, bool>.FromConverter(s => s.StateIsAlive), GridSquares);
                //Matrix.map(FSharpFunc<ISquare, bool>.FromConverter(s => s.StateIsAlive), m);
            }
            set
            {
                SetPortionOfBoard(value);
            }

        }

        public void NextState()
        {
            GridSquaresAsEnumerable.AsParallel().ForAll(s=>s.PrepareNextState());
            GridSquaresAsEnumerable.AsParallel().ForAll(s=>s.ShiftToNextState());
        }

        public void SetPortionOfBoard(bool[,] newField, int xOfTopLeftCorner = 0, int yOfTopLeftCorner = 0)
        {
            if (newField == null) throw new ArgumentNullException(nameof(newField));
            if (xOfTopLeftCorner < 0) throw new ArgumentOutOfRangeException(nameof(xOfTopLeftCorner));
            if (yOfTopLeftCorner < 0) throw new ArgumentOutOfRangeException(nameof(yOfTopLeftCorner));
            if (newField.GetLength(0) + xOfTopLeftCorner > ColsX)
                throw new ArgumentOutOfRangeException(nameof(newField));
            if (newField.GetLength(1) + yOfTopLeftCorner > RowsY)
                throw new ArgumentOutOfRangeException(nameof(newField));


            for (var y = 0; y < newField.GetLength(1); y++)
            {
                for (var x = 0; x < newField.GetLength(0); x++)
                {
                    GridSquares[x+xOfTopLeftCorner, y+yOfTopLeftCorner].StateIsAlive = newField[x, y];
                }
            }
            if (Constants.Debug)
            {
                Program.Print2DArray(newField);
                Program.Print2DArray(GridSquaresAsBools);

            }
        }

        public Board(int colsX, int rowsY)
        {
            if (colsX <= 0) throw new ArgumentOutOfRangeException(nameof(colsX));
            if (rowsY <= 0) throw new ArgumentOutOfRangeException(nameof(rowsY));

            ColsX = colsX;
            RowsY = rowsY;

            GridSquares = new ISquare[ColsX, RowsY];
            for (var y = 0; y < RowsY; y++)
            {
                for (var x = 0; x < ColsX; x++)
                {
                    GridSquares[x,y]=new Square(x,y);
                }
            }
            for (var y = 0; y < RowsY; y++)
            {
                for (var x = 0; x < ColsX; x++)
                {
                    var neighbors = new List<ISquare>();
                    for (var i = -1; i <= 1; i++) {
                        for (var j = -1; j <= 1; j++) {
//                            if (Constants.WrapAround)
//                            {
//                                x = x + ColsX % ColsX;
//                                y = y + RowsY % RowsY;
//                            }
                            if(!(i==0&&j==0||x+i<0||y+j<0||x+i>=ColsX||y+j>=RowsY)){
                                neighbors.Add(GridSquares[x + i,y + j]);
                            }
                        }
                    }
                    ((Square) GridSquares[x, y]).Neighbors = neighbors;
                }
            }
        }
    }


}