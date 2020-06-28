using Xunit;
using Domain;

namespace Testing
{
    public class GameTesting
    {
        public GameTesting()
        {
            Game.SetToTesting("aofofjw98j(*FJ(*JF(WJF(*j8fifodknfoqhf98h298hff8298h98udfh98298hf9(*H(@*HF898f47");
        }

        #region TestFunctionTesting
        [Fact]
        public void STTTest_ExpectKeyValid()
        {
            Assert.True(Game.SetToTesting("aofofjw98j(*FJ(*JF(WJF(*j8fifodknfoqhf98h298hff8298h98udfh98298hf9(*H(@*HF898f47"));
        }

        [Fact]
        public void STTTest_ExpectKeyInvalid()
        {
            Assert.False(Game.SetToTesting("I crack y0 code"));
        }
        #endregion

        #region PropertyTests
        [Fact]
        public void PropertyTest_ExpectDimensionSetDefault()
        {
            int expected = 10;
            Game.Dimension = -100;
            Assert.Equal(expected, Game.Dimension);
        }

        [Theory]
        [InlineData(100)]
        [InlineData(10)]
        [InlineData(1)]
        public void PropertyTest_ExpectDimensionSetValid(int value)
        {
            int expected = value;
            Game.Dimension = value;
            Assert.Equal(expected, Game.Dimension);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(0)]
        [InlineData(-10)]
        public void PropertyTest_ExpectDimensionSetValidFunctional(int value)
        {
            int expected = value < 1 ? 10 : value;
            Game.Dimension = value;
            Assert.Equal(expected, Game.Dimension);
        }
        #endregion

        #region MethodTests
        #region SetMethod
        [Fact]
        public void SetTest_ExpectNewGameResetDimension()
        {
            int expected = 10;
            Game.Dimension = 10293;
            Game.Set(true);
            Assert.Equal(expected, Game.Dimension);
        }

        [Fact]
        public void SetTest_ExpectNewGameNoReset()
        {
            int expected = 10293;
            Game.Dimension = expected;
            Game.Set(true);
            Assert.NotEqual(expected, Game.Dimension);
        }

        [Fact]
        public void SetTest_ExpectInGameNoReset()
        {
            int expected = 10293;
            Game.Dimension = expected;
            Game.Set(false);
            Assert.Equal(expected, Game.Dimension);
        }

        [Fact]
        public void SetTest_ExpectInGameReset()
        {
            int expected = 10;
            Game.Dimension = 10293;
            Game.Set(false);
            Assert.NotEqual(expected, Game.Dimension);
        }
        #endregion

        #region PlaceMethod
        [Fact]
        public void PlaceTest_ExpectDefaultLocation()
        {
            Game.Dimension = 30;
            const int EXPECTED = 11;
            int actual = Game.Place(false);
            Assert.Equal(EXPECTED, actual);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 0)]
        [InlineData(100, 96)]
        public void PlaceTest_ExpectLocationNoChange(int dimension, int location)
        {
            Game.Dimension = dimension;
            int expected = location;
            int actual = Game.Place(false, location);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(0, false, -1)]      //location < 0
        [InlineData(1, false, 1)]       //location > dimension-1
        [InlineData(-19, true, -298)]   //cpu true, daemon automated placement (always expect change)
        [InlineData(1,true,11)]         //""
        public void PlaceTest_ExpectLocationChange(int dimension,bool cpu, int location)
        {
            Game.Set(true); //for init of variables used in the Place method
            Game.Dimension = dimension;
            int actual = Game.Place(cpu, location);

            Assert.NotEqual(location, actual);
        }

        [Theory]
        [InlineData(false, -1389)]
        [InlineData(false, 0)]
        [InlineData(false, 1038)]
        [InlineData(true, -1389)]
        [InlineData(true, 0)]
        [InlineData(true, 1038)]
        public void PlaceTest_ExpectLocationSetValid(bool cpu, int location)
        {
            Game.Set(true); //for init of variables used in the Place method
            int actual = Game.Place(cpu, location);
            Assert.True(actual > -1 && actual < Game.Dimension);
        }
        #endregion

        #region ShowPlayerStatsMethod
        [Fact]
        public void ShowPlayerStatsTest_DModeOn()
        {
            Game.Set(true);
            Player player = new Player();
            player.Switch();
            const string EXPECTED = "@-1\n❤️ 100\n☯   99\n☀   1\n☣\n\n\n";
            string actual = Game.ShowPlayerStats(player);

            Assert.Equal(EXPECTED, actual);
        }

        [Fact]
        public void ShowPlayerStatsTest_DModeOff()
        {
            Game.Set(true);
            Player player = new Player();
            const string EXPECTED = "@-1\n❤️ 100\n☯   100\n☀   1\n\n\n";
            string actual = Game.ShowPlayerStats(player);

            Assert.Equal(EXPECTED, actual);
        }
        #endregion

        #region ApplyCreditsMethod
        [Theory]
        [InlineData(3,10,98)]
        [InlineData(-82,-382,-28)]
        [InlineData(1983,2983928,293892)]
        public void ApplyCreditsTest_ExpectStatsUpgrade(int health, int strength, int trust)
        {
            Player player = new Player();
            Daemon daemon = new Daemon();
            int expected_health = player.Health = health;
            int expected_strength = player.Strength = strength;
            int expected_trust = player.Trust = trust;

            Game.ApplyCredits(player, daemon);

            Assert.True(player.Health > expected_health);
            Assert.True(player.Strength > expected_strength);
            Assert.True(player.Trust > expected_trust);
        }

        [Theory]
        [InlineData(3, 10, 98)]
        [InlineData(-82, -382, -28)]
        [InlineData(1983, 2983928, 293892)]
        public void ApplyCreditsTest_ExpectStatsDowngrade(int health, int strength, int trust)
        {
            Player player = new Player();
            Daemon daemon = new Daemon();
            int expected_health = player.Health = health;
            int expected_strength = player.Strength = strength;
            int expected_trust = player.Trust = trust;

            Game.ApplyCredits(player, daemon);

            Assert.False(player.Health < expected_health);
            Assert.False(player.Strength < expected_strength);
            Assert.False(player.Trust < expected_trust);
        }
        #endregion

        #region ApplyHitDamageMethod
        [Theory]
        [InlineData(1,100)]
        [InlineData(100,1)]
        public void ApplyHitDamageTest_ExpectDamageTaken(int expected_dhealth, int expected_phealth)
        {
            Player player = new Player();
            Daemon daemon = new Daemon();
            daemon.Health = expected_dhealth;
            player.Health = expected_phealth;
            Game.ApplyHitDamage(player, daemon);

            Assert.True(daemon.Health < expected_dhealth);
            Assert.True(player.Health < expected_phealth);
        }

        [Theory]
        [InlineData(0,0)]
        [InlineData(-2,-183)]
        [InlineData(-3,1)]
        [InlineData(1,1)]
        public void ApplyHitDamageTest_ExpectNoDamageTakenTrue(int dhealth, int phealth)
        {
            Player player = new Player();
            Daemon daemon = new Daemon();
            const int EXPECTED = 0;
            daemon.Health = dhealth;
            player.Health = phealth;
            Game.ApplyHitDamage(player, daemon);

            Assert.Equal(EXPECTED, player.Health);
            Assert.Equal(EXPECTED, daemon.Health);
        }
        #endregion
        
        #region NarratorMethod
        [Fact]
        public void NarratorTest_ExpectFullNarration()
        {
            string value = "死へようこそ";
            string expected = value;
            string actual = Game.Narrator(value);
            Assert.Equal(expected, actual);
        }
        #endregion
    
        #endregion
    }
}
