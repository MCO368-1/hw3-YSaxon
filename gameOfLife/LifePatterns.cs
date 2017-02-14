namespace gameOfLife
{
    public static class LifePatterns
    {
        public static readonly bool[,] Glider =  {
            {false, true, false},
            {false, false, true},
            {true, true, true},
        };
        public static readonly bool[,] Oh =  {
            {true, true, true},
            {true, false, true},
            {true, true, true},
        };
    }
}