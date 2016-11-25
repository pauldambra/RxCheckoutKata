namespace RxCheckoutKata
{
    public struct Discount
    {
        public string Sku { get; }
        public int QuantityNeeded { get; }
        public int Amount { get; }

        public Discount(string sku, int quantityNeeded, int amount)
        {
            Sku = sku;
            QuantityNeeded = quantityNeeded;
            Amount = amount;
        }
    }
}