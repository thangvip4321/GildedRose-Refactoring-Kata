using System.Collections.Generic;
using GildedRoseKata;
using NUnit.Framework;

namespace GildedRoseTests;

public class GildedRoseTest
{
    // public static List<Item> allItems =  new List<Item> { 
    //     new() { Name = "normal item", SellIn = 0, Quality = 0 },
    //     new() {Name ="Sulfuras, Hand of Ragnaros",SellIn = 40,Quality=80},
    //     new() {Name="Aged Brie",SellIn = 20,Quality=30},
    //     new() {Name="Aged Brie",SellIn = 20,Quality=30},
    //     };
    [Test]
    public void Test_Item_never_change_name()
    {
        var items = new List<Item> { new Item { Name = "foo", SellIn = 0, Quality = 0 } };
        var app = new GildedRose(items);
        app.UpdateQuality();
        Assert.AreEqual("foo", items[0].Name);
    }
    [Test]
    public void Test_Normal_Item_Degrade_Quality()
    {
        var items = new List<Item> { new() { Name = "random stuff", SellIn = 3, Quality = 2 } };
        var app = new GildedRose(items);
        app.UpdateQuality();
        Assert.AreEqual(1, items[0].Quality);
        app.UpdateQuality();
        Assert.AreEqual(0, items[0].Quality);
        app.UpdateQuality();
        Assert.AreEqual(0, items[0].Quality);
    }



    [Test]
    public void Test_Normal_Item_Degrade_SellIn()
    {
        var items = new List<Item> { new() { Name = "random stuff", SellIn = 1, Quality = 2 } };
        var app = new GildedRose(items);
        app.UpdateQuality();
        Assert.AreEqual(0, items[0].SellIn);
        app.UpdateQuality();
        Assert.AreEqual(-1, items[0].SellIn);
    }

    [Test]
    public void Test_Sulfuras_Never_Change()
    {
        var items = new List<Item> { new() { Name = "Sulfuras, Hand of Ragnaros", SellIn = 3, Quality = 80 } };
        var app = new GildedRose(items);
        app.UpdateQuality();
        Assert.AreEqual(items[0].Quality, 80);
        Assert.AreEqual(items[0].SellIn, 3);
    }
    [Test]
    public void Test_Aged_Brie_Increase_Quality()
    {
        var items = new List<Item> { new() { Name = "Aged Brie", SellIn = 5, Quality = 30 } };
        var app = new GildedRose(items);
        app.UpdateQuality();
        Assert.AreEqual(items[0].Quality, 31);
        Assert.AreEqual(items[0].SellIn, 4);
        for (int i = 0; i < 30; i++)
        {
            app.UpdateQuality();
        }
        Assert.AreEqual(items[0].Quality, 50);
        Assert.AreEqual(items[0].SellIn, -26);
    }

    // is there anything wrong with this test?
    // too long?
    [Test]
    public void Test_Backstage_Passes()
    {
        var items = new List<Item> { new() { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 13, Quality = 5 } };
        var app = new GildedRose(items);
        for (int i = 0; i < 3; i++)
        {
            app.UpdateQuality();
        }
        Assert.AreEqual(items[0].SellIn, 10);
        Assert.AreEqual(items[0].Quality, 8);
        for (int i = 0; i < 5; i++)
        {
            app.UpdateQuality();
        }
        Assert.AreEqual(items[0].SellIn, 5);
        Assert.AreEqual(items[0].Quality, 18);
        for (int i = 0; i < 3; i++)
        {
            app.UpdateQuality();
        }
        Assert.AreEqual(items[0].SellIn, 2);
        Assert.AreEqual(items[0].Quality, 27);
        for (int i = 0; i < 5; i++)
        {
            app.UpdateQuality();
        }
        Assert.AreEqual(items[0].SellIn, -3);
        Assert.AreEqual(items[0].Quality, 0);
    }

}