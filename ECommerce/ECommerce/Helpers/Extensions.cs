using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerce.Helpers
{
    public static class Extensions
    {
        public static bool IsValid(this Order value)
        {
            Func<Order, bool> isValidTitle = o => !string.IsNullOrWhiteSpace(o.Title);
            Func<Order, bool> isWellPlaced = o => o.PlacedOn > DateTime.MinValue;

            return isValidTitle(value) && isWellPlaced(value);
        }
        public static bool IsAllValid(this Order value)
        {
            Func<Order, bool>[] rules = {
                o => !string.IsNullOrWhiteSpace(o.Title),
                o => o.PlacedOn > DateTime.MinValue
            };

            return rules.All(rule => rule(value));
        }
    }

    public class Order
    {
        public string Title { get; set; }
        public DateTime PlacedOn { get; set; }
        public float Price { get; set; }
    }
}