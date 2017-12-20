// ReSharper disable ParameterOnlyUsedForPreconditionCheck.Local

using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Day17.Part2
{
    public class Part2
    {
        private readonly ITestOutputHelper _output;

        public Part2(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void Tests()
        {
            var spinLock = new SpinLock(steps: 3);
            SpinLockAfterSteps(spinLock, 1);
            SpinLockAfterSteps(spinLock, 2);
            SpinLockAfterSteps(spinLock, 2);
            SpinLockAfterSteps(spinLock, 2);
            SpinLockAfterSteps(spinLock, 5);
            SpinLockAfterSteps(spinLock, 5);
            SpinLockAfterSteps(spinLock, 5);
            SpinLockAfterSteps(spinLock, 5);
            SpinLockAfterSteps(spinLock, 9);
            SpinLockAfterSteps(spinLock, 9);
            SpinLockAfterSteps(spinLock, 9);
            SpinLockAfterSteps(spinLock, 12);
            SpinLockAfterSteps(spinLock, 12);
            SpinLockAfterSteps(spinLock, 12);
            SpinLockAfterSteps(spinLock, 12);
            for (int i = 0; i < 202; i++)
            {
                SpinLockAfterSteps(spinLock, 16);
            }
            SpinLockAfterSteps(spinLock, 218);
        }

        private static void SpinLockAfterSteps(SpinLock spinlock, int expected)
        {
            spinlock.MoveNext();
            Assert.Equal(expected, spinlock.Current);
        }

        [Fact]
        public void Answer()
        {
            var spinLock = new SpinLock(356);
            var answer = spinLock.Take(50000000).Last();
            _output.WriteLine($"Part2: {answer}");
            Assert.Equal(47465686, answer);
        }
    }
}