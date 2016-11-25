using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace RxCheckoutKata
{
    public class Till
    {
        private readonly Subject<string> _scannedItems = new Subject<string>();

        public IObservable<int> Display =>
            _scannedItems
                .Scan(new Basket(), (basket, sku) => basket.AddItem(sku))
                .Select(basket => new {Total = PriceCalculator.Total(basket.Items), basket})
                .Select(x => new {x.Total, x.basket, Discount = DiscountCalculator.Total(x.basket.Items)})
                .Select(x => x.Total - x.Discount);

        public void Scan(string itemSku)
        {
            _scannedItems.OnNext(itemSku);
        }

        public Till CompleteSale()
        {
            return new Till();
        }
    }
}