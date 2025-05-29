using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemVisualizer.Core.Interfaces
{
    public interface IGraphItem
    {
        public string Name { get; set; }
        public (double X, double Y) Location { get; set; }
        public (double W, double H) ActualSize { get; set; }
        public string Cluster { get; set; }
    }
}
