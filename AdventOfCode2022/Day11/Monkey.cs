using System;
using System.Collections.Generic;

namespace AdventOfCode2022.Day11
{
    class Monkey
    {
        public Queue<long> items;
        public Func<long, long> operation;
        public int inspectedItems;
        public int mod;
        public int passToMonkeyIfDivides, passToMonkeyOtherwise;
    }
}
