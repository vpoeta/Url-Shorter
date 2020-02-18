using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace UrlShorter.Services.Common
{
    public interface ICodeService
    {
        Task<string> GenerateCodeAsync();

    }
}
