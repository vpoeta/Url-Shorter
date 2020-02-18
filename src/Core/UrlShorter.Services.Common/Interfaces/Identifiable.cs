using System;
using System.Collections.Generic;
using System.Text;

namespace UrlShorter.Services.Common
{
    public interface IIdentifiable
    {
        Guid Id { get; }
    }
}
