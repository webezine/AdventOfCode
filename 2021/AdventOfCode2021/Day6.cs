using NUnit.Framework;

namespace AdventOfCode2022
{
    [TestFixture]
    public class Day6
    {
        string[] input;
        private int[] inputs;

        [SetUp]
        public void SetUp()
        {
            input = File.ReadAllText("Day6.txt").Split(",");
            inputs = (Array.ConvertAll(input, s => Int32.Parse(s)));
        }

        [Test]
        public void Part1()
        {

            Assert.That(-1, Is.EqualTo(0));
        }

        [Test]
        public void Part2()
        {

            long[] fishGeneration = new long[9];
            foreach (int i in inputs)
            {
                fishGeneration[i]++;
            }

            for (int iteration = 0; iteration < 256; iteration++)
            {
                long newOnes = fishGeneration[0];
                for (int i = 1; i < fishGeneration.Length; i++)
                {
                    fishGeneration[i - 1] = fishGeneration[i];
                }

                fishGeneration[8] = newOnes;
                fishGeneration[6] += newOnes;
            }

            var result = fishGeneration.Sum();
            Assert.That(result, Is.EqualTo(1650309278600));
        }
    }
}