﻿using System;
using System.Collections.Generic;

namespace ShipmentApi.model
{
    public partial class Shipmentagent
    {
        public int Id { get; set; }
        public int? Orderid { get; set; }
        public DateTime? Orderplacedate { get; set; }
        public DateTime? Deliverydate { get; set; }
        public string DeliveryGuy { get; set; }
        public string Statuss { get; set; }
    }
}
