using System.Collections.Generic;
using System.Linq;

namespace RxCheckoutKata
{
    public static class PriceCalculator
    {
        private static readonly Dictionary<string, int> Prices =
            new Dictionary<string, int>
            {
                {"A", 50},
                {"B", 30},
                {"C", 60},
                {"D", 99}
            };


        public static int Total(IReadOnlyList<string> items) =>
            items.GroupBy(i => i)
                .Aggregate(0, (total, sku) => total + Prices[sku.Key] * sku.Count());
    }
}