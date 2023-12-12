using StopCoinCombine.Common.Configs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

namespace StopCoinCombine.Common.Players;

public sealed class StopCoinsCombiningPlayer : ModPlayer
{
    public bool HasItemThatUsesCoins { get; private set; }

    public override void Load()
    {
        On_ItemSorting.SortCoins += PreventSortingDestruction;
        On_Player.DoCoins += PreventCombining;
    }

    // This method doesn't actually sort coins, it destroys them and places them back in order.
    // By default, it isn't called if the player has the Coin Gun.
    private void PreventSortingDestruction(On_ItemSorting.orig_SortCoins orig)
    {
        CoinCombiningConfig config = ModContent.GetInstance<CoinCombiningConfig>();
        if (config.ShouldKeepCoinsSeparate(Main.LocalPlayer)) // Main.LocalPlayer used in Coin Gun check
        {
            return;
        }

        orig();
    }

    // i: Index in self.inventory
    private void PreventCombining(On_Player.orig_DoCoins orig, Player self, int i)
    {
        CoinCombiningConfig config = ModContent.GetInstance<CoinCombiningConfig>();
        if (config.ShouldKeepCoinsSeparate(self))
        {
            Item item = self.inventory[i];
            if (item.stack == 100 && item.IsACoin)
            {
                if (config.HighestCoinTypeToKeep == CoinCombiningConfig.CoinType.Copper)
                {
                    return;
                }
                else if (config.HighestCoinTypeToKeep == CoinCombiningConfig.CoinType.Silver && item.type >= ItemID.SilverCoin)
                {
                    return;
                }
                else if (config.HighestCoinTypeToKeep == CoinCombiningConfig.CoinType.Gold && item.type >= ItemID.GoldCoin)
                {
                    return;
                }
            }
        }

        orig(self, i);
    }

    public override void ResetEffects()
    {
        HasItemThatUsesCoins = false;
    }

    public void MarkAsCarryingItemThatUsesCoins()
    {
        HasItemThatUsesCoins = true;
    }
}