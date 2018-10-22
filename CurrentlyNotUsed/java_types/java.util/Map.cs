using System;
using System.Collections.Generic;

// see comment in type "MapN" regarding the "N" suffix
namespace java.util
{
    // TODO: make this Map into an interface like in Java
    // https://docs.oracle.com/javase/7/docs/api/java/util/Map.html
    public class Map<T, U> where U : class // nullable 
    {
        private Dictionary<T, U> map = new Dictionary<T, U>();

        public void putAll(Map<T, U> idVertexIndex)
        {
            foreach(KeyValuePair<T, U> kv in idVertexIndex.map)
            {
                this.put(kv.Key, kv.Value);
            }
        }

        public void clear()
        {
            this.map.Clear();
        }

        public void put(T t, U u)
        {
            // TODO: dotnet Add can not automatically overwrite
            // but Java .. ?
            // i.e. check if this is now correctly implemented as in java:
            if(map.ContainsKey(t))
            {
                map.Remove(t);
            }
            this.map.Add(t, u);
        }

        public bool containsKey(T t)
        {
            return map.ContainsKey(t);
        }

        // https://docs.oracle.com/javase/7/docs/api/java/util/Map.html#get(java.lang.Object)
        // Returns the value to which the specified key is mapped, or null if this map contains no mapping for the key. 
        public U get(T t)
        {
            if(!containsKey(t))
            {
                //Console.WriteLine("map nyckel saknas: " + t);
                //return null;//default(U);
                return default(U);
            }
            // https://msdn.microsoft.com/en-us/library/9tee9ht2(v=vs.110).aspx
            // If the specified key is not found, a get operation throws a KeyNotFoundException,
            return map[t];
        }

        public int size()
        {
            return map.Count;
        }

        // IList is not a java type but still exposed
        // here in method signature to support foreach statement
        public IList<T> __keySetAsDotNetList()
        {
            var list = new System.Collections.Generic.List<T>();
            foreach(T t in map.Keys)
            {
                list.Add(t);
            }
            return list;
        }

        public IEnumerable<U> __valuesAsDotNetEnumerable()
        {
            return map.Values;
        }

        public U remove(T t)
        {
            var u = map[t];
            this.map.Remove(t);
            return u;
        }

        public Set<T> keySet()
        {
            var set = new Set<T>();
            var keys = map.Keys;
            foreach(var key in keys)
            {
                set.add(key);
            }
            return set;
        }

        public Collection<U> values()
        {
            throw new NotImplementedException();
        }

    }
}
