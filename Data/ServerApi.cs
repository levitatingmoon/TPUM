using Newtonsoft.Json;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    internal static class ServerStatics
    {
        public static readonly string GetItemsCommandHeader = "RequestAll";
        public static readonly string SellItemCommandHeader = "RequestTransaction";

        public static readonly string UpdateAllResponseHeader = "UpdateAll";
        public static readonly string InflationChangedResponseHeader = "PriceChanged";
        public static readonly string TransactionResponseHeader = "TransactionResult";
    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "11.0.0.0 (Newtonsoft.Json v13.0.0.0)")]
    public abstract class ServerCommand
    {
        [Newtonsoft.Json.JsonProperty("Header", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Header { get; set; }


    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "11.0.0.0 (Newtonsoft.Json v13.0.0.0)")]
    public partial class GetItemsCommand : ServerCommand
    {

    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "11.0.0.0 (Newtonsoft.Json v13.0.0.0)")]
    public partial class SellItemCommand : ServerCommand
    {
        [Newtonsoft.Json.JsonProperty("Items", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.Collections.Generic.ICollection<System.Guid> Items { get; set; }


    }





    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "11.0.0.0 (Newtonsoft.Json v13.0.0.0)")]
    public partial class ItemDTO
    {
        [Newtonsoft.Json.JsonProperty("Id", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.Guid Id { get; set; }

        [Newtonsoft.Json.JsonProperty("Name", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Name { get; set; }

        [Newtonsoft.Json.JsonProperty("Type", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Type { get; set; }

        [Newtonsoft.Json.JsonProperty("Price", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public float Price { get; set; }

    }

    [GeneratedCode("NJsonSchema", "11.0.0.0 (Newtonsoft.Json v13.0.0.0)")]
    internal class NewPriceDTO
    {
        [JsonProperty("ItemID", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public Guid ItemID { get; set; }

        [JsonProperty("NewPrice", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public float NewPrice { get; set; }
    }


    [GeneratedCode("NJsonSchema", "11.0.0.0 (Newtonsoft.Json v13.0.0.0)")]
    internal abstract class ServerResponse
    {
        [JsonProperty("Header", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Header { get; set; }
    }

    [GeneratedCode("NJsonSchema", "11.0.0.0 (Newtonsoft.Json v13.0.0.0)")]
    internal class UpdateAllResponse : ServerResponse
    {
        [JsonProperty("Items", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public ICollection<ItemDTO> Items { get; set; }
    }

    [GeneratedCode("NJsonSchema", "11.0.0.0 (Newtonsoft.Json v13.0.0.0)")]
    internal class InflationChangedResponse : ServerResponse
    {
        [JsonProperty("Price", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public float Price { get; set; }

        [JsonProperty("ItemID", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public Guid ItemID { get; set; }

    }

    [GeneratedCode("NJsonSchema", "11.0.0.0 (Newtonsoft.Json v13.0.0.0)")]
    internal class TransactionResponse : ServerResponse
    {
        [JsonProperty("TransactionId", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public Guid TransactionId { get; set; }

        [JsonProperty("Succeeded", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public bool Succeeded { get; set; }
    }

}


