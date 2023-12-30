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

        switch (item.Name)
        {
            case AGED_BRIE:
                UpdateQuality(item);
                break;
            case BACKSTAGE_PASS:
                if (item.SellIn < 5)
                {
                    UpdateQuality(item,3);
                }else if(item.SellIn<10){
                    UpdateQuality(item,2);
                }else{
                    UpdateQuality(item,1);
                }
                break;
            case SULFURAS:
                break;
            default:
                UpdateQuality(item,-1);
                break;
        }



        if (item.SellIn < 0)
        {
            switch (item.Name)
            {
                case AGED_BRIE:
                    UpdateQuality(item, 1);
                    break;
                case BACKSTAGE_PASS:
                    item.Quality = 0;
                    break;
                case SULFURAS:
                    break;
                default:
                    UpdateQuality(item, -1);
                    break;
            }
        }
    }

    private static void UpdateQuality(Item item, int value = 1)
    {
            item.Quality =  Math.Max(0, Math.Min(item.Quality+value, 50));
    }
}