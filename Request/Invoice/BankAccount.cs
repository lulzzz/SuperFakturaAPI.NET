﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Birko.SuperFaktura.Request.Invoice
{
    public class BankAccount
    {
        [JsonProperty(PropertyName = "default", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(Converters.StringBooleanConverter))]
        public bool Default { get; set; }
        [JsonProperty(PropertyName = "country_id", NullValueHandling = NullValueHandling.Ignore)]
        public int CountryID { get; set; }
        [JsonProperty(PropertyName = "bank_name", NullValueHandling = NullValueHandling.Ignore)]
        public string BankName { get; set; }
        [JsonProperty(PropertyName = "bank_code", NullValueHandling = NullValueHandling.Ignore)]
        public string BankCode { get; set; }
        [JsonProperty(PropertyName = "account", NullValueHandling = NullValueHandling.Ignore)]
        public string Account { get; set; }
        [JsonProperty(PropertyName = "iban", NullValueHandling = NullValueHandling.Ignore)]
        public string IBAN { get; set; }
        [JsonProperty(PropertyName = "swift", NullValueHandling = NullValueHandling.Ignore)]
        public string SWIFT { get; set; }
    }
}
