using System;

namespace UrlShorter.Services.Domain.Entities
{
    public class UrlMapping : IEquatable<UrlMapping>
    {
        public string Code { get; }
        public string ShortLink { get; }
        public string LongLink { get; }

        public DateTime ExpireAt { get; set; }

        public UrlMapping(string code, string shortLink, string longLink, DateTime expireAt)
        {
            Code = code;
            ShortLink = shortLink;
            LongLink = longLink;
            ExpireAt = expireAt;
        }

        public bool Equals(UrlMapping other)
        {
            if (ReferenceEquals(null, other)) return false;
            return ReferenceEquals(this, other) || Code.Equals(other.Code);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((UrlMapping)obj);
        }

        public override int GetHashCode()
        {
            return Code.GetHashCode();
        }
    }
}
