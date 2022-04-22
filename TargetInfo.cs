using Dalamud.Game.ClientState.Objects.Types;

namespace Stormtalons
{
	public class TargetInfo
	{
		public static uint Health { get; set; }
		public bool IsValidTarget(GameObject target)
		{
			if (target is BattleNpc bnpc)
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