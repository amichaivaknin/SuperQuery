using System;

namespace businessLogic.Extentions
{
    /// <summary>
    /// Statistics class provide as a information on a specific action
    /// </summary>
    public class Statistics
    {
        public string Name { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public double Duration => (End - Start).TotalMilliseconds;
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}