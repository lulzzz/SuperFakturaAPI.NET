﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Birko.SuperFaktura.Request.Invoice
{
    public class AccountingDetail
    {
        [JsonProperty(PropertyName = "place", NullValueHandling = NullValueHandling.Ignore)]
        public string Place { get; set; }
        [JsonProperty(PropertyName = "order", NullValueHandling = NullValueHandling.Ignore)]
        public string Order { get; set; }
        [JsonProperty(PropertyName = "type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; } = AccountingDetailType.Item;
        [JsonProperty(PropertyName = "analytics_account", NullValueHandling = NullValueHandling.Ignore)]
        public string AnalyticsAccount { get; set; }
        [JsonProperty(PropertyName = "synthetic_account", NullValueHandling = NullValueHandling.Ignore)]
        public string SyntheticAccount { get; set; }
    }
}
