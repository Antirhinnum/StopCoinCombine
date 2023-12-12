using StopCoinCombine.Common.Configs;
using StopCoinCombine.Common.Players;
using Terraria;
using Terraria.ModLoader;

namespace StopCoinCombine.Common;

internal static class CallSystem
{
	internal static object Call(object[] args)
	{
		Mod mod = ModContent.GetInstance<StopCoinCombine>();
		CoinCombiningConfig config = ModContent.GetInstance<CoinCombiningConfig>();
		if (args is ["Config", .. object[] configArgs])
		{
			if (configArgs is ["ModEnabled"])
			{
				return config.ModEnabled;
			}

			if (configArgs is ["HighestCoinTypeToKeep"])
			{
				return (int)config.HighestCoinTypeToKeep;
			}

			if (configArgs is ["OnlyKeepSeparateWhenUsingCoins"])
			{
				return config.OnlyKeepSeparateWhenUsingCoins;
			}
		}

		if (args is ["ShouldKeepCoinsSeparate", Player playerToCheckForSeparation])
		{
			return config.ShouldKeepCoinsSeparate(playerToCheckForSeparation);
		}

		if (args is ["HasItemThatUsesCoins", Player playerToCheckForItem])
		{
			return playerToCheckForItem.GetModPlayer<StopCoinsCombiningPlayer>().HasItemThatUsesCoins;
		}

		if (args is ["MarkAsCarryingItemThatUsesCoins", Player playerToMark])
		{
			playerToMark.GetModPlayer<StopCoinsCombiningPlayer>().MarkAsCarryingItemThatUsesCoins();
			return null;
		}

		mod.Logger.WarnFormat("Unknown call arguments: [{0}]", string.Join(", ", args));
		return null;
	}
}