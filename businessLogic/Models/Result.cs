namespace businessLogic.Models
{
    /// <summary>
    /// Result class collect a data of single search result 
    /// </summary>
    public class Result
    {
        public string Title { get; set; } = "";
        public string DisplayUrl { get; set; }
        public string Description { get; set; } = "";
        public double Rank { get; set; }
    }
}