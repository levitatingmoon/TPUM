using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationServer
{
    [Serializable]
    public abstract class ServerCommand
    {
        public string Header { get; set; }

        protected ServerCommand(string header)
        {
            Header = header;
        }
    }

    [Serializable]
    public class GetItemsCommand : ServerCommand
    {
        public static string StaticHeader = "RequestAll";

        public GetItemsCommand()
        : base(StaticHeader)
        {

        }
    }

    [Serializable]
    public class SellItemCommand : ServerCommand
    {
        public static string StaticHeader = "RequestTransaction";

        public List<Guid>? Items { get; set; }

        public SellItemCommand(Guid id)
        : base(StaticHeader)
        {

        }
    }

    [Serializable]
    public class ItemDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public float Price { get; set; }

        public ItemDTO()
        {
            Id = Guid.Empty;
            Name = "None";
            Type = "None";
            Price = 0f;
        }

        public ItemDTO(Guid id, string name, string type, float price)
        {
            Id = id;
            Name = name;
            Type = type;
            Price = price;
        }
    }

    [Serializable]
    public struct NewPriceDTO
    {
        public Guid ItemID { get; set; }
        public float NewPrice { get; set; }

        public NewPriceDTO(Guid itemId, float newPrice)
        {
            ItemID = itemId;
            NewPrice = newPrice;
        }
    }

    [Serializable]
    public abstract class ServerResponse
    {
        public string Header { get; private set; }

        protected ServerResponse(string header)
        {
            Header = header;
        }
    }

    [Serializable]
    public class UpdateAllResponse : ServerResponse
    {
        public static readonly string StaticHeader = "UpdateAll";

        public ItemDTO[]? Items { get; set; }

        public UpdateAllResponse()
            : base(StaticHeader)
        {
        }
    }

    [Serializable]
    public class InflationChangedResponse : ServerResponse
    {
        public static readonly string StaticHeader = "PriceChanged";

        public float Price { get; set; }

        public Guid ItemID { get; set; }

        public InflationChangedResponse()
            : base(StaticHeader)
        {
        }

    }

    [Serializable]
    public class TransactionResponse : ServerResponse
    {
        public static readonly string StaticHeader = "TransactionResult";

        public Guid TransactionId { get; set; }
        public bool Succeeded { get; set; }

        public TransactionResponse()
            : base(StaticHeader)
        {
        }

    }
}
