using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoWebAPI.Utilities
{
    public static class RandomGenerator
    {
        public static string GenerateAccountNumber(int size)
        {
            return $"{GenerateAlphabeth(size)}{GenerateNumber(size)}";
        }

        private static string GenerateNumber(int size)
        {
            Random res = new();

            String str = "0123456789";

            String randomstring = string.Empty;

            for (int i = 0; i < size; i++)
            {
                int x = res.Next(str.Length);

                randomstring += str[x];
            }

            return randomstring;
        }

        private static string GenerateAlphabeth(int size)
        {
            Random res = new();

            String str = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            String randomstring = string.Empty;

            for (int i = 0; i < size; i++)
            {
                int x = res.Next(str.Length);

                randomstring += str[x];
            }

            return randomstring;
        }
    }
}
