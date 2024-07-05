using Dalamud.Game.ClientState.Objects.Types;

namespace Stormtalons
{
    public class TargetInfo
    {
        public static uint Health { get; set; }
        public bool IsValidTarget(IGameObject target)
        {
            if (target is IBattleNpc bnpc)
            {
                Health = bnpc.CurrentHp;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}