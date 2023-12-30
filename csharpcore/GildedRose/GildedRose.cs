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

    private BaseItemUpdater GetRightItemUpdater(Item item)
    {
        switch (item.Name)
        {
            case AGED_BRIE:
                return new AgedBrieUpdater();
            case BACKSTAGE_PASS:
                return new BackStageUpdater();
            case SULFURAS:
                return new SulfurasUpdater();
            case CONJURED_ITEM:
                return new ConjuredItemUpdater();
            default:
                return new BaseItemUpdater();
        }
    }
    private void UpdateStat(Item item)
    {
        // if (item.Name != SULFURAS)
        // {
        //     item.SellIn--;
        // }

        var updater = GetRightItemUpdater(item);
        updater.Update(item);

        // var qualityChange = 0;
        // switch (item.Name)
        // {
        //     case AGED_BRIE:
        //         qualityChange = item.IsExpired ? 2 : 1;
        //         break;
        //     case BACKSTAGE_PASS:
        //         if (item.IsExpired) qualityChange = -item.Quality;
        //         else if (item.SellIn < 5)
        //             qualityChange = 3;
        //         else if (item.SellIn < 10)
        //             qualityChange = 2;
        //         else
        //             qualityChange = 1;
        //         break;
        //     case SULFURAS:
        //         break;
        //     case CONJURED_ITEM:
        //         qualityChange = item.IsExpired ? -4 : -2;
        //         break;
        //     default:
        //         qualityChange = item.IsExpired ? -2 : -1;
        //         break;
        // }
        // IncreaseQuality(item, qualityChange);
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


    class BaseItemUpdater
    {
        protected void IncreaseQuality(Item item, int value = 1)
        {
            if (value == 0) return;
            item.Quality = Math.Max(0, Math.Min(item.Quality + value, 50));
        }
        public virtual void Update(Item item)
        {
            item.SellIn -= 1;
            var qualityChange = item.IsExpired ? -2 : -1;
            IncreaseQuality(item, qualityChange);
        }
    }
    class SulfurasUpdater : BaseItemUpdater
    {
        public override void Update(Item item)
        {
            return;
        }
    }
    class ConjuredItemUpdater : BaseItemUpdater
    {
        public override void Update(Item item)
        {
            item.SellIn--;
            var qualityChange = item.IsExpired ? -4 : -2;
            IncreaseQuality(item, qualityChange);
        }
    }
    class BackStageUpdater : BaseItemUpdater
    {
        public override void Update(Item item)
        {
            item.SellIn--;
            int qualityChange;
            if (item.IsExpired) qualityChange = -item.Quality;
            else if (item.SellIn < 5)
                qualityChange = 3;
            else if (item.SellIn < 10)
                qualityChange = 2;
            else
                qualityChange = 1;
            IncreaseQuality(item, qualityChange);

        }
    }
    class AgedBrieUpdater : BaseItemUpdater
    {
        public override void Update(Item item)
        {
            item.SellIn--;
            var qualityChange = item.IsExpired ? 2 : 1;
            IncreaseQuality(item, qualityChange);
        }
    }
}

