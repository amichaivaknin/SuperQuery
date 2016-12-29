using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace businessLogic.Extentions
{
    public class Statistics
    {
        public string Name { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public long Duration
        {
            get { return (End - Start).Seconds; }
            set { Duration = 0; }
        }

        public bool Success { get; set; }
    }
}
