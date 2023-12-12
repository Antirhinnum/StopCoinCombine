using StopCoinCombine.Common;
using Terraria.ModLoader;

namespace StopCoinCombine;

public sealed class StopCoinCombine : Mod
{
	public override object Call(params object[] args)
	{
		return CallSystem.Call(args);
	}
}