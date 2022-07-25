using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Helpers
{
    public static class GenerateNumberAccount
    {
        public static string GenerateAccount()
        {
            string cuenta = "";
            for (int i = 0; cuenta.Length < 9; i++)
            {
                cuenta += new Random().Next(10000, 49999);
            }
            return cuenta;
        }
    }
}
