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
            item.SellIn = item.SellIn - 1;
        }

        if (item.Name != AGED_BRIE && item.Name != BACKSTAGE_PASS && item.Name != SULFURAS)
        {
            DecreaseQuality(item);
        }
        else
        {
            AddQuality(item);
            if (item.Name == BACKSTAGE_PASS)
            {
                if (item.SellIn < 10)
                {
                    AddQuality(item);
                }

                if (item.SellIn < 5)
                {
                    AddQuality(item);
                }
            }
        }



        if (item.SellIn < 0)
        {
            if (item.Name == AGED_BRIE)
            {
                AddQuality(item);
            }
            else if (item.Name == BACKSTAGE_PASS)
            {
                item.Quality = 0;
            }
            else if (item.Name == SULFURAS)
            {
            }
            else{
                 DecreaseQuality(item);
            }

        }
    }

    private static void AddQuality(Item item, int value = 1)
    {
        if (item.Quality < 50)
        {
            item.Quality = item.Quality + value;
        }
    }
    private static void DecreaseQuality(Item item, int value = 1)
    {
        if (item.Quality > 0)
        {
            item.Quality = item.Quality - value;
        }
    }
}