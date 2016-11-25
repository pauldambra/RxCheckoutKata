using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using RxCheckoutKata;
using Xunit;

namespace RxCheckoutKataTests
{
    public class WithoutDiscountsTheTill
	{
		private readonly List<int> _items = new List<int>();
	    private readonly Till _till;

	    public WithoutDiscountsTheTill()
	    {
	        _till = new Till();
	        _till.Display.Subscribe(si => _items.Add(si));
	    }

		[Theory]
		[InlineData("A", 50)]
		[InlineData("B", 30)]
		[InlineData("C", 60)]
		[InlineData("D", 99)]
		public void CanScanSingleItems(string item, int total)
		{
			_till.Scan(item);

			_items.Single().Should().Be(total);
		}

	    [Theory]
	    [InlineData("A", 100)]
	    [InlineData("C", 120)]
	    [InlineData("D", 198)]
	    public void CanScanTwoOfTheSame(string item, int total)
	    {
	        _till.Scan(item);
	        _till.Scan(item);

	        _items.Should().BeEquivalentTo(new[] {total/2, total});
	    }

	    [Theory]
	    [InlineData("A", "B", new [] {50, 80})]
	    [InlineData("A", "C", new [] {50, 110})]
	    [InlineData("A", "D", new [] {50, 149})]
	    [InlineData("B", "A", new [] {30, 80})]
	    [InlineData("B", "C", new [] {30, 90})]
	    [InlineData("B", "D", new [] {30, 129})]
	    [InlineData("C", "A", new [] {60, 110})]
	    [InlineData("C", "B", new [] {60, 90})]
	    [InlineData("C", "D", new [] {60, 159})]
	    [InlineData("D", "A", new [] {99, 149})]
	    [InlineData("D", "B", new [] {99, 129})]
	    [InlineData("D", "C", new [] {99, 159})]
	    public void CanScanMixedPairs(string itemOne, string itemTwo, int[] totals)
	    {
	        _till.Scan(itemOne);
	        _till.Scan(itemTwo);

	        _items.Should().BeEquivalentTo(totals);
	    }

	    [Theory]
	    [InlineData(new [] {"A", "B", "C", "D"}, new [] {50, 80, 140, 239})]
	    [InlineData(new [] {"A", "B", "C", "D", "C", "A"}, new [] {50, 80, 140, 239, 299, 349})]
	    public void CanScanLongerSequences(string[] items, int[] totals)
	    {
	        foreach (var item in items)
	        {
	            _till.Scan(item);
	        }

	        _items.Should().BeEquivalentTo(totals);
	    }
	}
}
