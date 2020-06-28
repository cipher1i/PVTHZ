using Xunit;
using Domain;
using System;

namespace Testing
{
    public class PlayerTesting
    {
        private Player player;

        public PlayerTesting()
        {
            player = new Player();
            player.SetToTesting("1k9I28N!83(#827#(=2938(382-1fqj9KKD92938*(346^*73&639)19");
        }

        #region TestFunctionTesting
        [Fact]
        public void STTTest_ExpectKeyValid()
        {
            Assert.True(player.SetToTesting("1k9I28N!83(#827#(=2938(382-1fqj9KKD92938*(346^*73&639)19"));
        }

        [Fact]
        public void STTTest_ExpectKeyInvalid()
        {
            Assert.False(player.SetToTesting("I crack y0 code"));
        }

        #endregion

        #region InitTests
        [Fact]
        public void InitTest_FullCheckUp()
        {
            //health=100, strength=1, trust=100, daemon_mode=false
            player = new Player();

            const int EXPECTED_HEALTH = 100;
            const int EXPECTED_STRENGTH = 1;
            const int EXPECTED_TRUST = 100;
            const bool EXPECTED_DMODE = false;
            
            Assert.Equal(EXPECTED_HEALTH, player.Health);
            Assert.Equal(EXPECTED_STRENGTH, player.Strength);
            Assert.Equal(EXPECTED_TRUST, player.Trust);
            Assert.Equal(EXPECTED_DMODE, player.Daemon_Mode);
        }
        #endregion

        #region PropertyTests
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        public void PropertyTest_ExpectNotBelowLimit(int value)
        {
            int EXPECTED_ZERO = 0;
            int EXPECTED_ONE = 1;
            player.Health = value;
            player.Strength = value;
            player.Trust = value;

            Assert.Equal(EXPECTED_ZERO, player.Health);
            Assert.Equal(EXPECTED_ZERO, player.Trust);
            Assert.Equal(EXPECTED_ONE, player.Strength);
        }

        [Theory]
        [InlineData(-139)]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(139)]
        public void PropertyTest_ExpectBelowLimit(int value)
        {
            player.Health = player.Strength = player.Trust = value;
            bool EXPECTED_HEALTH = player.Health < 0;
            bool EXPECTED_STRENGTH = player.Strength < 1;
            bool EXPECTED_TRUST = player.Trust < 0;

            Assert.False(EXPECTED_HEALTH);
            Assert.False(EXPECTED_STRENGTH);
            Assert.False(EXPECTED_TRUST);
        }

        [Theory]
        [InlineData(100,10389,29837)]
        [InlineData(0,1,0)]
        [InlineData(0,0,0)]
        [InlineData(-23,-3982,-1)]
        public void PropertyTest_ExpectSetValueFunctional(int hvalue, int svalue, int tvalue)
        {
            player.Health = hvalue;
            player.Strength = svalue;
            player.Trust = tvalue;

            int EXPECTED_HEALTH = hvalue < 0? 0 : hvalue;
            int EXPECTED_STRENGTH = svalue < 1? 1 : svalue;
            int EXPECTED_TRUST = tvalue < 0 ? 0 : tvalue;

            Assert.Equal(EXPECTED_HEALTH, player.Health);
            Assert.Equal(EXPECTED_STRENGTH, player.Strength);
            Assert.Equal(EXPECTED_TRUST, player.Trust);
        }
        #endregion

        #region MethodTests
        #region MethodsWithInput
            [Theory]
            [InlineData("-1")]
            [InlineData("0")]
            [InlineData("2983")]
            public void GuessTest_ExpectInputValid(string guess)
            {
                if (!Int32.TryParse(guess, out int expected))
                    expected = -919;

                int actual = player.Guess(guess);
                Assert.Equal(expected, actual); 
            }

            [Theory]
            [InlineData("A")]
            [InlineData("z")]
            [InlineData("^")]
            [InlineData("")]
            [InlineData(null)]
            [InlineData("Hello")]
            public void GuessTest_ExpectInputInvalid(string guess)
            {
                if(!Int32.TryParse(guess,out int expected))
                    expected = -919;

                int actual = player.Guess(guess);
                Assert.NotEqual(expected, actual);
            }

            [Theory]
            [InlineData('m')]
            [InlineData('p')]
            [InlineData('s')]
            [InlineData('d')]
            public void InputTest_ExpectInputValid(char action)
            {
                bool actual = player.Input(action);
                Assert.True(actual);
            }

            [Theory]
            [InlineData('a')]
            [InlineData('z')]
            [InlineData('M')]
            [InlineData('P')]
            public void InputTest_ExpectInputInvalid(char action)
            {
                bool actual = player.Input(action);
                Assert.False(actual);
            }
            #endregion

        [Fact]
        public void PortTest_ExpectLocationValid()
        {
            int location = player.Port();
            Assert.True(location > -1 && location < Game.Dimension + 1);
        }

        [Fact]
        public void PortTest_ExpectLocationInValid()
        {
            int location = player.Port();
            Assert.False(location < 0 || location > Game.Dimension);
        }

        [Fact]
        public void SwitchTest_ExpectSetDModeFunctional()
        {
            bool[] actual = new bool[2];
            bool[] expected = new bool[2];

            actual[0] = player.Switch();
            expected[0] = player.Daemon_Mode;

            Assert.Equal(expected[0], actual[0]);

            actual[1] = player.Switch();
            expected[1] = player.Daemon_Mode;

            Assert.NotEqual(actual[0], actual[1]);
            Assert.NotEqual(expected[0], expected[1]);
            Assert.Equal(expected[1], actual[1]);
        }

        #endregion
    }
}
