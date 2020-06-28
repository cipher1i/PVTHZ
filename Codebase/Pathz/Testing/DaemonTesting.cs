using Xunit;
using Domain;

namespace Testing
{
    public class DaemonTesting
    {
        #region InitTests
        [Fact]
        public void InitTest_HealthCheckUp()
        {
            //health = 100, strength = ?, wisdom = ?, name = ?
            Daemon daemon = new Daemon();

            const int EXPECTED_HEALTH = 100;
            Assert.Equal(EXPECTED_HEALTH, daemon.Health);
        }

        [Theory]
        [InlineData(1000)]
        [InlineData(101)]
        [InlineData(100)]
        public void InitTest_ExpectHealthNotBelowZero(int value)
        {
            Daemon daemon = new Daemon();
            daemon.Health -= value;
            const int EXPECTED_HEALTH = 0;
            Assert.Equal(EXPECTED_HEALTH, daemon.Health);
        }
        #endregion

        #region PropertyTests
        [Theory]
        [InlineData(-10)]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(10)]
        public void PropertyTest_ExpectHealthBelowZero(int value)
        {
            Daemon daemon = new Daemon();
            daemon.Health = value;
            bool EXPECTED = daemon.Health < 0;
            Assert.False(EXPECTED);
        }

        [Theory]
        [InlineData(100)]
        [InlineData(28398)]
        [InlineData(0)]
        public void PropertyTest_ExpectHealthSetValue(int value)
        {
            Daemon daemon = new Daemon();
            daemon.Health = value;
            int EXPECTED_HEALTH = value;
            Assert.Equal(EXPECTED_HEALTH, daemon.Health);
        }

        [Theory]
        [InlineData(-1938)]
        [InlineData(0)]
        [InlineData(103)]
        public void PropertyTest_ExpectHealthSetValueFunctional(int value)
        {
            Daemon daemon = new Daemon();
            daemon.Health = value;
            int EXPECTED_HEALTH = value < 0 ? 0 : value;
            Assert.Equal(EXPECTED_HEALTH, daemon.Health);
        }
        #endregion
    }
}
