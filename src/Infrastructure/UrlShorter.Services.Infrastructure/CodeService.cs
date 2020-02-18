using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UrlShorter.Services.Common;

namespace UrlShorter.Services.Infrastructure
{

    public class CodeService : ICodeService
    {
        private static readonly Random _random = new Random();
        private const string alphabet = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const ulong basis = 62;

        public async Task<string> GenerateCodeAsync()
        {
            ulong number = (ulong)(DateTime.Now.Ticks + await Task.FromResult(GetRandomNumber()));
            string code = string.Empty;
            while (number > 0)
            {
                ulong position = number % basis;
                code = alphabet[(int)position] + code;
                number = (number / basis);
            }
            return code;
        }

        private static int GetRandomNumber()
        {
            return _random.Next();
        }
    }
}
