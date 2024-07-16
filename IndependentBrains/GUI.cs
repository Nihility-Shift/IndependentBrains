using VoidManager.CustomGUI;
using VoidManager.Utilities;

namespace IndependentBrains
{
    internal class GUI : ModSettingsMenu
    {
        public override string Name() => "Independent Brains";

        public override void Draw()
        {
            GUITools.DrawCheckbox("Target separate fighters", ref Configs.randomFightersConfig);
            GUITools.DrawCheckbox("Target separate mines", ref Configs.randomMinesConfig);
            GUITools.DrawCheckbox("Independently target everything", ref Configs.randomAllConfig);
        }
    }
}
