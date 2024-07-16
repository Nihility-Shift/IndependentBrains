using VoidManager.MPModChecks;

namespace IndependentBrains
{
    public class VoidManagerPlugin : VoidManager.VoidPlugin
    {
        public override MultiplayerType MPType => MultiplayerType.Host;

        public override string Author => "18107";

        public override string Description => "Makes B.R.A.I.N turrets select different targets";
    }
}
