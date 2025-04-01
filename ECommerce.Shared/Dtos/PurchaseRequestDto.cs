using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Shared.Dtos
{
    public class PurchaseRequestDto
    {
        public string ProductId { get; set; }
        public int RequestedQuantity { get; set; }
    }
}