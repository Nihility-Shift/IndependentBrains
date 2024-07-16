using BepInEx.Configuration;

namespace IndependentBrains
{
    internal class Configs
    {
        internal static ConfigEntry<bool> randomFightersConfig;
        internal static ConfigEntry<bool> randomMinesConfig;
        internal static ConfigEntry<bool> randomAllConfig;

        internal static void Load(BepinPlugin plugin)
        {
            randomFightersConfig = plugin.Config.Bind("IndependentBrains", "RandomFighters", true);
            randomMinesConfig = plugin.Config.Bind("IndependentBrains", "RandomMines", true);
            randomAllConfig = plugin.Config.Bind("IndependentBrains", "RandomAll", false);
        }
    }
}
