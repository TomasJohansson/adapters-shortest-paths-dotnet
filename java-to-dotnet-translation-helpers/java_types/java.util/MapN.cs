using System;
using System.Collections.Generic;

// Java Double is a class and can be used in the 
// same generics types, but in .NET the Double 
// is a struct, which is the reason for creating separate 
// types here in the port, with "N" as suffix 
// for the struct based generics which use 
// a nullable "U?" as return type for one method.
// "Map<T, U>"  ---> ... where U : class
// "MapN<T, U>" ---> ... where U : struct

// Currently most of the two above two types "Map" and "MapN"
// contain lots of duplication. In fact everything is duplicated 
// except "where U : class" vs "where U : struct"
// and "public U get(T t)" vs "public U? get(T t)"
// and of course it would be desirable to 
// eliminate or reduce the duplication ...

namespace java.util
{
    // TODO: make this Map into an interface like in Java
    // https://docs.oracle.com/javase/7/docs/api/java/util/Map.html
    public class MapN<T, U> where U : struct // nullable 
    {
        private Dictionary<T, U> map = new Dictionary<T, U>();

        public void putAll(MapN<T, U> idVertexIndex)
        {
            foreach (KeyValuePair<T, U> kv in idVertexIndex.map)
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
                Console.WriteLine("map nyckel saknas: " + t);
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
        public IList<T> keySet()
        {
            var list = new System.Collections.Generic.List<T>();
            foreach(T t in map.Keys)
            {
                list.Add(t);
            }
            return list;
        }

    }
}
