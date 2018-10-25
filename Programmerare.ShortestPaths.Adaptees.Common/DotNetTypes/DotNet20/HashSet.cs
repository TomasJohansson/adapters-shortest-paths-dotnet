#if NET20
// The purpose of this file is to support .NET 2.0 
// which did not include the two used types ISet and HashSet
// and this file include a minimal implementation of the 
// HashSet with those methods actually being used.
using System.Collections;
using System.Collections.Generic;

namespace Programmerare.ShortestPaths.Adaptees.Common.DotNetTypes.DotNet20
{
    // HashSet was added in .NET 3.5 
    // https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1?view=netframework-4.7.2

    // ISet was added in .NET 4.0
    // https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.iset-1?view=netframework-4.7.2
    
    public class HashSet<T> : ISet<T>
    {
        private IDictionary<T, bool> dict = new Dictionary<T, bool>();

        public void Add(T t){
            if(dict.ContainsKey(t))
            {
                dict.Remove(t);
            }
            dict.Add(t, true);
        }

        public void AddAll(IList<T> list) {
            foreach(T t in list) {
                this.Add(t);
            }
        }

        public void Clear() {
            this.dict.Clear();
        }

        public bool Contains(T t) {
            return this.dict.ContainsKey(t);
        }

        public void Remove(T t) {
            this.dict.Remove(t);
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return this.dict.Keys.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.dict.Keys.GetEnumerator();
        }
    }
}
#endif