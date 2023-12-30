namespace GildedRoseKata;

public class Item
{
    public string Name { get; set; }
    public int SellIn { get; set; }
    public int Quality { get; set; }

    public bool IsExpired
    {
        get { return SellIn < 0; }
    }
}

