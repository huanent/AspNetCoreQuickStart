using ApplicationCore.SharedKernel;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure
{
    public class Coding : ICoding
    {
        public string MD5Encoding(string source, string salt = null)
        {
            var md5 = MD5.Create();
            var nameBytes = Encoding.ASCII.GetBytes(source + salt);
            var md5Code = md5.ComputeHash(nameBytes);
            var sb = new StringBuilder();
            for (int i = 0; i < md5Code.Length; i++)
            {
                sb.Append(md5Code[i].ToString("X2"));
            }
            return sb.ToString();
        }
    }
}
