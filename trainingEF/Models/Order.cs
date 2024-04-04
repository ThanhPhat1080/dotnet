﻿namespace trainingEF.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderPlaced { get; set; }

        public DateTime Orderfulfilled { get; set; }
        public int CustomerId { get; set; }

        public Customer Customer { get; set; } = null;

        public ICollection<OrderDetail> OrderDetails { get; set; }

    }
}