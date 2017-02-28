namespace gameOfLife
{

    using FluentAssertions;
    using NUnit.Framework;


    [TestFixture]
    public class LifeTests
    {
        private Board _mainBoard;

        [SetUp]
        public void Init()
        {
            _mainBoard = new Board(4, 3);
            _mainBoard.SetPortionOfBoard(LifePatterns.Glider);
        }

        [Test]
        public void NextState()
        {
            var gliderGen2 = new[,]
            {
                {false, false, false},
                {true, false, true},
                {false, true, true},
                {false, true, false},
            };
            _mainBoard.NextState();
            _mainBoard.GridSquaresAsBools.Should().Equal(gliderGen2);
        }
    }
}
