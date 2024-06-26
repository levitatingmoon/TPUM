﻿using System;
using System.Text;

namespace Data
{
    internal static class Extensions
    {
        internal static ArraySegment<byte> GetArraySegment(this string message)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(message);
            return new ArraySegment<byte>(buffer);
        }

        public static ItemType ItemTypeFromString(string typeAsString)
        {
            return (ItemType)Enum.Parse(typeof(ItemType), typeAsString);
        }

        public static string ToString(this ItemType typeAsString)
        {
            return Enum.GetName(typeof(ItemType), typeAsString) ?? throw new InvalidOperationException();
        }
        public static IItem ToItem(this ItemDTO itemDTO)
        {
            return new Item(
                itemDTO.Id,
                itemDTO.Name,
                itemDTO.Price,
                ItemTypeFromString(itemDTO.Type)
            ) ;
        }
    }


}
