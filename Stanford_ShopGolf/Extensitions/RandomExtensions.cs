using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Stanford_ShopGolf.Extensitions
{
    public static class RandomExtensions
    {
        public static bool NextBool(this Random random)
        {
            return random.Next(0, 1) == 0;
        }

        public static string NextString(this Random random, int totalCharacter, bool appendBackSpace = true)
        {
            var builder = new StringBuilder();
            for (var i = 0; i < totalCharacter; i++)
            {
                builder.Append(char.ConvertFromUtf32(random.Next(65, 122)));
                if (random.NextBool())
                {
                    builder.Append(" ");
                }
            }
            return builder.ToString();
        }
    }
}