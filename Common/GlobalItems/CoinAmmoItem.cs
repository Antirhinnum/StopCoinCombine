using StopCoinCombine.Common.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StopCoinCombine.Common.GlobalItems;

public sealed class CoinAmmoItem : GlobalItem
{
	public override bool AppliesToEntity(Item entity, bool lateInstantiation)
	{
		return lateInstantiation && entity.useAmmo == AmmoID.Coin;
	}

	public override void UpdateInventory(Item item, Player player)
	{
		player.GetModPlayer<StopCoinsCombiningPlayer>().MarkAsCarryingItemThatUsesCoins();
	}
}