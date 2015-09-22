﻿using Raven.Imports.Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Birko.SuperFaktura.Entities
{
    public partial class Stock
    {
        public class Filter : PagedSearchParams
        {
            [JsonProperty(PropertyName = "price_from")]
            public decimal PriceFrom { get; set; } = 0;
            [JsonProperty(PropertyName = "price_to")]
            public decimal PriceTo { get; set; } = 0;

            public override string ToParams(bool listInfo = true)
            {
                string paramString = base.ToParams(listInfo);
                paramString += "/price_from:" + this.PriceFrom;
                paramString += "/price_to:" + this.PriceTo;

                return paramString;
            }
        }
    }
}