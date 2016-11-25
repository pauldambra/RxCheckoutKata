using System;
using System.Collections.Generic;
using FluentAssertions;
using RxCheckoutKata;
using Xunit;

namespace RxCheckoutKataTests
{
    public class WithDiscountsTheTill
    {
        private readonly List<int> _items = new List<int>();
        private Till _till;

        public WithDiscountsTheTill()
        {
            _till = new Till();
            _till.Display.Subscribe(si => _items.Add(si));
        }

        [Fact]
        public void CanScanTwoBs()
        {
            _till.Scan("B");
            _till.Scan("B");

            _items.Should().BeEquivalentTo(new []{30, 45});
        }

        [Fact]
        public void CanScanThreeBs()
        {
            _till.Scan("B");
            _till.Scan("B");
            _till.Scan("B");

            _items.Should().BeEquivalentTo(new []{30, 45, 75});
        }

        [Fact]
        public void CanScanFiveAs()
        {
            _till.Scan("A");
            _till.Scan("A");
            _till.Scan("A");
            _till.Scan("A");
            _till.Scan("A");

            _items.Should().BeEquivalentTo(new []{50, 100, 120, 170, 220});
        }

        [Fact]
        public void CanScanSixAs()
        {
            _till.Scan("A");
            _till.Scan("A");
            _till.Scan("A");
            _till.Scan("A");
            _till.Scan("A");
            _till.Scan("A");

            _items.Should().BeEquivalentTo(new []{50, 100, 120, 170, 220, 240});
        }

        [Fact]
        public void CanScanMixedBaskets()
        {
            new List<string> {"A", "B", "C", "D", "A", "B", "B", "C", "A"}.ForEach(_till.Scan);

            _items.Should().BeEquivalentTo(new []{50, 80, 140, 239, 289, 304, 334, 394, 414});
        }

        [Fact]
        public void CanScanMultipleBaskets()
        {
            new List<string> { "A", "B", "C", "D" }.ForEach(_till.Scan);
            _till = _till.CompleteSale();
            _till.Display.Subscribe(si => _items.Add(si));
            new List<string> { "A", "B", "B", "C", "A" }.ForEach(_till.Scan);
            _items.Should().BeEquivalentTo(new []{50, 80, 140, 239, 50, 80, 95, 155, 205});
        }
    }
}