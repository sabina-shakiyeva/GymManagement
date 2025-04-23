using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.Entities.Concrete
{
	public class PurchaseHistory
	{
		public int Id { get; set; }
		public int UserId { get; set; }
		public int ProductId { get; set; }
		public int Quantity { get; set; }
		public int TotalPoints { get; set; }
		public DateTime PurchasedAt { get; set; }
	}
}
