using System.Collections.Generic;

namespace csharp
{
    public class GildedRose
    {
        public const string AgedBrie = "Aged Brie";
        public const string BackstagePassesToATafkal80EtcConcert = "Backstage passes to a TAFKAL80ETC concert";
        public const string SulfurasHandOfRagnaros = "Sulfuras, Hand of Ragnaros";
        IList<Item> Items;

        public GildedRose(IList<Item> Items)
        {
            this.Items = Items;
        }

        public void UpdateQuality()
        {
            for (var i = 0; i < Items.Count; i++)
            {
                var item = Items[i];
                ProcessItem(item);
            }
        }

        private static void ProcessItem(Item item)
        {
            if (item.Name == SulfurasHandOfRagnaros)
            {
                return;
            }

            item.SellIn = item.SellIn - 1;

            if (item.Name == BackstagePassesToATafkal80EtcConcert)
            {
                UpdateQualityTickets(item);
                return;
            }

            if (item.Name == AgedBrie)
            {
                IncreaseQualityBrie(item);
                return;
            }

            ReduceQualityRegular(item);
            if (item.SellIn < 0)
            {
                ReduceQualityRegular(item);
            }
        }

        private static void ReduceQualityRegular(Item item)
        {
            if (item.Quality > 0)
            {
                item.Quality = item.Quality - 1;
            }
        }

        private static void IncreaseQualityRegular(Item item)
        {
            if (item.Quality < 50)
            {
                item.Quality = item.Quality + 1;
            }
        }

        private static void UpdateQualityTickets(Item item)
        {
            if (item.SellIn < 0)
            {
                item.Quality = 0;
                return;
            }
            
            IncreaseQualityRegular(item);
            if (item.SellIn < 10)
            {
                IncreaseQualityRegular(item);
            }

            if (item.SellIn < 5)
            {
                IncreaseQualityRegular(item);
            }
        }

        private static void IncreaseQualityBrie(Item item)
        {
            IncreaseQualityRegular(item);
            if (item.SellIn < 0)
            {
                IncreaseQualityRegular(item);
            }
        }
    }
}