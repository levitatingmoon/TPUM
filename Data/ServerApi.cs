using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "11.0.0.0 (Newtonsoft.Json v13.0.0.0)")]
    public partial class ShopItem
    {
        [Newtonsoft.Json.JsonProperty("Name", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Name { get; set; }

        [Newtonsoft.Json.JsonProperty("Price", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public float Price { get; set; }

        [Newtonsoft.Json.JsonProperty("Id", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.Guid Id { get; set; }

        [Newtonsoft.Json.JsonProperty("Type", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int Type { get; set; }


    }
}

