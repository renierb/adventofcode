// ReSharper disable ParameterOnlyUsedForPreconditionCheck.Local

using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Day17.Part1
{
    public class Part1
    {
        private readonly ITestOutputHelper _output;

        public Part1(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void Tests()
        {
            SpinLock spinlock = new SpinLock(3);
            SpinLockAfterSteps(spinlock, 0);
            SpinLockAfterSteps(spinlock, 1);
            SpinLockAfterSteps(spinlock, 1);
            SpinLockAfterSteps(spinlock, 3);
            SpinLockAfterSteps(spinlock, 2);
            SpinLockAfterSteps(spinlock, 1);
        }

        private static void SpinLockAfterSteps(SpinLock spinlock, int expected)
        {
            Assert.Equal(expected, Compute(spinlock));
        }

        private static int Compute(SpinLock spinlock)
        {
            spinlock.MoveNext();
            return spinlock.Current;
        }

        [Fact]
        public void Answer()
        {
            SpinLock spinlock = new SpinLock(356);
            var answer = spinlock.Take(2017).Last();
            _output.WriteLine($"Part1: {answer}");
        }
    }
}