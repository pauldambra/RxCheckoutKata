using System.Collections.Generic;
using System.Linq;

namespace RxCheckoutKata
{
    public class Basket
    {
        public IReadOnlyList<string> Items { get; }

        public Basket()
        {
            Items = new List<string>();
        }

        public Basket(IEnumerable<string> initialItems)
        {
            Items = new List<string>(initialItems);
        }

        public Basket AddItem(string item) => new Basket(Items.Concat(new[] {item}));
    }
}