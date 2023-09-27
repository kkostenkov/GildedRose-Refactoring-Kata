namespace csharp
{
    public class GildedRose
    {
        public const string AgedBrie = "Aged Brie";
        public const string BackstagePasses = "Backstage passes to a TAFKAL80ETC concert";
        public const string SulfurasHandOfRagnaros = "Sulfuras, Hand of Ragnaros";
        public const string Conjured = "Conjured";
        
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
            if (item.Name == SulfurasHandOfRagnaros) {
                return;
            }

            ReduceSellIn(item);

            if (item.Name == BackstagePasses) {
                UpdateQualityTickets(item);
                return;
            }
            else if (item.Name == AgedBrie) {
                IncreaseQualityBrie(item);
                return;
            }
            else if (item.Name.StartsWith(Conjured)) {
                ReduceQualityConjured(item);
            }
            else {
                ReduceQualityRegular(item);
            }
        }

        private static void ReduceQualityConjured(Item item)
        {
            ReduceQualityRegular(item);
            ReduceQualityRegular(item);
        }

        private static void ReduceSellIn(Item item)
        {
            item.SellIn = item.SellIn - 1;
        }

        private static void ReduceQualityRegular(Item item)
        {
            var qualityPenalty = item.SellIn < 0 ? 2 : 1;
            item.Quality = item.Quality - qualityPenalty;
            item.Quality = Math.Max(0, item.Quality);
        }

        private static void IncreaseQualityRegular(Item item)
        {
            if (item.Quality < 50) {
                item.Quality = item.Quality + 1;
            }
        }

        private static void UpdateQualityTickets(Item item)
        {
            if (item.SellIn < 0) {
                item.Quality = 0;
                return;
            }

            IncreaseQualityRegular(item);
            if (item.SellIn < 10) {
                IncreaseQualityRegular(item);
            }

            if (item.SellIn < 5) {
                IncreaseQualityRegular(item);
            }
        }

        private static void IncreaseQualityBrie(Item item)
        {
            IncreaseQualityRegular(item);
            if (item.SellIn < 0) {
                IncreaseQualityRegular(item);
            }
        }
    }
}