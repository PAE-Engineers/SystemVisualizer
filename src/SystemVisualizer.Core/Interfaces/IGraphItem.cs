using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia;

namespace SystemVisualizer.Core.Interfaces
{
    public interface IGraphItem
    {
        public string Name { get; set; }
        public Point Location { get; set; }
        public Rect Bounds { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public string Cluster { get; set; }
    }
}
