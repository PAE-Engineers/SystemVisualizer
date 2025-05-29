using System;
using System.Collections.Generic;
using System.Text;
using SystemVisualizer.Abstractions.Interfaces;

namespace SystemVisualizer.Abstractions.Models
{
    public class Point : IPoint
    {
        public double X { get; set; }
        public double Y { get; set; }
    }
}
