using System;

namespace businessLogic.Models
{
    public class Result
    {
        public string Title { get; set; } = "";
        public string DisplayUrl { get; set; }
        public string Description { get; set; } = "";
        public double Rank { get; set; }
    }
}