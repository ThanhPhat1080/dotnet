﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using trainingEF.Models.DTOs;

namespace trainingEF.Models;

public class Order
{
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; } = null!;
    public DateTime OrderPlaced { get; set; }
    public DateTime OrderFulfilled { get; set; }
    public string UserId { get; set; } = null!;

    public UserDto User { get; set; } = null!;
    public ICollection<OrderDetail> OrderDetails { get; set; } = null!;
}
