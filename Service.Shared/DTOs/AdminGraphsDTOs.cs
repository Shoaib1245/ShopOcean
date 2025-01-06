using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Shared.DTOs
{
    public class AdminGraphsDTOs
    {
        // Sellers
        public int TotalSeller { get; set; }
        public int ActiveSeller { get; set; } // Count of active sellers
        public int PendingSeller { get; set; } // Count of pending sellers
        public int RejectedSeller { get; set; } // Count of rejected sellers

        // Customers
        public int TotalCustomer { get; set; }
        public int ActiveCustomer { get; set; } // Count of active customers
        public int PendingCustomer { get; set; } // Count of pending customers
        public int ShippedCustomer { get; set; } // Count of shipped customers

        // Categories
        public int TotalCategory { get; set; }
        public int ActiveCategory { get; set; }
        public int InActiveCategory { get; set; }

        // SubCategories
        public int TotalSubCategory { get; set; }
        public int ActiveSubCategory { get; set; }
        public int InActiveSubCategory { get; set; }

        // Products
        public int TotalProduct { get; set; }
        public int ActiveProduct { get; set; }
        public int InActiveProduct { get; set; }
    }
}
