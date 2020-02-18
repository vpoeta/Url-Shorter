using System;

namespace UrlShorter.Services.Common
{
    public interface IDateTime
    {
        DateTime Now { get; }
    }
}
