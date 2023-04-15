﻿namespace Ecommerce.Api.Orders.Database
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public double Total { get; set; }
        public List<OrderItem> Items { get; set; }
    }
}
