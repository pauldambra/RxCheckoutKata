using System.Collections.Generic;
using System.Linq;

namespace RxCheckoutKata
{
    public static class DiscountCalculator
    {
        private static readonly Dictionary<string, Discount> Discounts =
            new Dictionary<string, Discount>
            {
                {"B", new Discount("B", 2, 15)},
                {"A", new Discount("A", 3, 30)}
            };

        public static int Total(IReadOnlyList<string> items)
        {
            return items.GroupBy(i => i)
                .Where(skuGroup => Discounts.ContainsKey(skuGroup.Key))
                .Select(skuGroup => new
                {
                    Items = skuGroup.ToList(),
                    Discount = Discounts[skuGroup.Key]
                })
                .SelectMany(x => x.Items.ChunkBy(x.Discount.QuantityNeeded).Select(chunk => new {Chunk = chunk, x.Discount}))
                .Aggregate(0, (sum, x) => sum + (x.Chunk.Count == x.Discount.QuantityNeeded ? x.Discount.Amount : 0));
        }
    }
}