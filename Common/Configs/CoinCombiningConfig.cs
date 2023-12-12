using StopCoinCombine.Common.Players;
using System.ComponentModel;
using Terraria;
using Terraria.ModLoader.Config;

namespace StopCoinCombine.Common.Configs;

public sealed class CoinCombiningConfig : ModConfig
{
	public enum CoinType
	{
		Copper,
		Silver,
		Gold
	}

	public override ConfigScope Mode => ConfigScope.ClientSide;

	[DefaultValue(true)]
	public bool ModEnabled;

	[DefaultValue(CoinType.Silver)]
	[DrawTicks]
	public CoinType HighestCoinTypeToKeep;

	[DefaultValue(true)]
	public bool OnlyKeepSeparateWhenUsingCoins;

	public bool ShouldKeepCoinsSeparate(Player player)
	{
		return ModEnabled && (!OnlyKeepSeparateWhenUsingCoins || player.GetModPlayer<StopCoinsCombiningPlayer>().HasItemThatUsesCoins);
	}
}