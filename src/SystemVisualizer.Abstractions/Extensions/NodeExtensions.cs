using System;
using System.Collections.Generic;
using System.Text;
using SystemVisualizer.Abstractions.Interfaces;

namespace SystemVisualizer.Abstractions.Extensions
{
    public static class NodeExtensions
    {
        public static INode GetNodeByTitle(this IEnumerable<INode> nodes, string title)
        {
            foreach (var node in nodes)
            {
                if (node.Title.Equals(title, StringComparison.OrdinalIgnoreCase))
                {
                    return node;
                }
            }
            return null;
        }
    }
}
