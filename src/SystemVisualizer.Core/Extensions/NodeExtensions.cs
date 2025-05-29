using SystemVisualizer.Core.Models;

namespace SystemVisualizer.Core.Extensions
{
    public static class NodeExtensions
    {
        public static Node? GetNodeByTitle(this IEnumerable<Node?> nodes, string title)
        {
            foreach (var node in nodes)
            {
                if (node.Name.Equals(title, StringComparison.OrdinalIgnoreCase))
                {
                    return node;
                }
            }
            return null;
        }
    }
}
