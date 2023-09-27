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
            for (var i = 0; i < Items.Count; i++) {
                var item = Items[i];
                ProcessItem(item);
            }
        }

        private static void ProcessItem(Item item)
        {
            if (item.Name == BackstagePassesToATafkal80EtcConcert) {
                IncreaseQualityTickets(item);
            }
            else if (item.Name != AgedBrie) {
                if (item.Quality > 0) {
                    if (item.Name != SulfurasHandOfRagnaros) {
                        item.Quality = item.Quality - 1;
                    }
                }
            }

            if (item.Name != SulfurasHandOfRagnaros) {
                item.SellIn = item.SellIn - 1;
            }

            if (item.Name == AgedBrie) {
                IncreaseQualityBrie(item);
                return;
            }

            if (item.SellIn < 0) {
                if (item.Name == BackstagePassesToATafkal80EtcConcert) {
                    item.Quality = 0;
                }
                else {
                    if (item.Quality > 0) {
                        if (item.Name != SulfurasHandOfRagnaros) {
                            item.Quality = item.Quality - 1;
                        }
                    }
                }
            }
        }

        private static void IncreaseQualityRegular(Item item)
        {
            if (item.Quality < 50) {
                item.Quality = item.Quality + 1;
            }
        }

        private static void IncreaseQualityTickets(Item item)
        {
            IncreaseQualityRegular(item);
            if (item.SellIn < 11) {
                IncreaseQualityRegular(item);
            }

            if (item.SellIn < 6) {
                IncreaseQualityRegular(item);
            }
        }

        private static void IncreaseQualityBrie(Item item)
        {
            if (item.Name != AgedBrie) {
                return;
            }
            IncreaseQualityRegular(item);
            if (item.SellIn < 0) {
                IncreaseQualityRegular(item);
            }
        }
    }
}