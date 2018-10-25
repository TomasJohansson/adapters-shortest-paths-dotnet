#if ( NET20 || NET30 || NET35 ) // See comments in the file HashSet.cs
namespace Programmerare.ShortestPaths.Adaptees.Common.DotNetTypes.DotNet20
{
    public interface ISet<T> : System.Collections.Generic.IEnumerable<T>
    {
        void AddAll(System.Collections.Generic.IList<T> list);
        void Add(T t);
        void Clear();
        void Remove(T t);
        bool Contains(T t);
    }
}
#endif