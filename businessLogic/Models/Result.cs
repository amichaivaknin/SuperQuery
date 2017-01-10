using System;

namespace businessLogic.Models
{
    public class Result : IEquatable<Result>
    {
        public string Title { get; set; }
        public string DisplayUrl { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public double Rank { get; set; }

        public bool Equals(Result other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Title, other.Title) && string.Equals(DisplayUrl, other.DisplayUrl) && string.Equals(Url, other.Url) && string.Equals(Description, other.Description) && Rank.Equals(other.Rank);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Result) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Title != null ? Title.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (DisplayUrl != null ? DisplayUrl.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Url != null ? Url.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Description != null ? Description.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Rank.GetHashCode();
                return hashCode;
            }
        }
    }
}