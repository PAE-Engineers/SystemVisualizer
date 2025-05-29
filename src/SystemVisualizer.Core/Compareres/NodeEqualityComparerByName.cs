using SystemVisualizer.Core.Interfaces;

namespace SystemVisualizer.Core.Compareres
{
    public class NodeEqualityComparerByName : IEqualityComparer<IGraphItem>
    {
        public bool Equals(IGraphItem? x, IGraphItem? y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            if (x.GetType() != y.GetType()) return false;
            return x.Name == y.Name;
        }

        public int GetHashCode(IGraphItem obj)
        {
            return (obj.Name.GetHashCode());
        }
    }
}
