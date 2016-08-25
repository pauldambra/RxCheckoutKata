using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using FluentAssertions;
using Xunit;

namespace RxCheckoutKata
{
	public class TestTill
	{
		private readonly List<string> _items = new List<string>();
			
		[Fact]
		public void CanSubscribeToScannedItems()
		{
			var till = new Till();
			till.ScannedItems.Subscribe(si => _items.Add(si));

			till.Scan("A");

			_items.Single().Should().Be("A");
		}
	}

	public class Till
	{
		public Till()
		{
			ScannedItems = new Subject<string>();
		}

		public IObservable<string> ScannedItems { get; }

		public void Scan(string itemSku)
		{
			
		}
	}
}
