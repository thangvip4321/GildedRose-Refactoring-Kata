using System;
using System.Collections.Generic;

namespace GildedRoseKata;

public class GildedRose
{
    private readonly IList<Item> _items;

    public GildedRose(IList<Item> items)
    {
        _items = items;
    }

    private const string BACKSTAGE_PASS = "Backstage passes to a TAFKAL80ETC concert";
    private const string SULFURAS = "Sulfuras, Hand of Ragnaros";
    private const string AGED_BRIE = "Aged Brie";
    private const string CONJURED_ITEM = "Conjured Mana Cake";

    public void UpdateQuality()
    {
        foreach (var item in _items)
        {
            UpdateStat(item);
        }
    }

    private void UpdateStat(Item item)
    {
        if (item.Name != SULFURAS)
        {
            item.SellIn--;
        }


        var qualityChange = 0;
        switch (item.Name)
        {
            case AGED_BRIE:
                qualityChange = item.IsExpired ? 2 : 1;
                break;
            case BACKSTAGE_PASS:
                if (item.IsExpired) qualityChange = -item.Quality;
                else if (item.SellIn < 5)
                    qualityChange = 3;
                else if (item.SellIn < 10)
                    qualityChange = 2;
                else
                    qualityChange = 1;
                break;
            case SULFURAS:
                break;
            case CONJURED_ITEM:
                qualityChange = item.IsExpired ? -4 : -2;
                break;
            default:
                qualityChange = item.IsExpired ? -2: -1;
                break;
        }
        IncreaseQuality(item, qualityChange);
    }

    /// <summary>
    /// Updates the quality of an item.
    /// </summary>
    /// <param name="item">The item to update.</param>
    /// <param name="value">The value by which to update the quality. Default is 1.</param>
    private static void IncreaseQuality(Item item, int value = 1)
    {
        if (value == 0) return;
        item.Quality = Math.Max(0, Math.Min(item.Quality + value, 50));
    }

    class ItemUpdater{
        protected
         void UpdateSellIn(Item item);
         void UpdateQuality(Item item);
    }
    class DefaultUpdater {
        
        void UpdateSellIn(Item item){
            item.Quality
        }
    }
}
