using System.Collections.Generic;

namespace java_to_dotnet_translation_helpers.dot_net_types {

    // IEnumerable<T> (and also ICollection<T>) are base types for e.g. Dictionary,List,HashSet.

    // Dictionary/IDictionary:
    // public class Dictionary<TKey, TValue> : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>, IReadOnlyDictionary<TKey, TValue>,  IReadOnlyCollection<KeyValuePair<TKey, TValue>>
    // public interface IDictionary<TKey, TValue> : ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>
    // https://msdn.microsoft.com/en-us/library/xfhwa508(v=vs.110).aspx
    // https://msdn.microsoft.com/en-us/library/s4ys34ea(v=vs.110).aspx

    // HashSet/ISet:
    // public class HashSet<T> : ISet<T>, ICollection<T>, IEnumerable<T>, IReadOnlyCollection<T>
    // public interface ISet<T> : ICollection<T>, IEnumerable<T>, IEnumerable
    // https://msdn.microsoft.com/en-us/library/bb359438(v=vs.110).aspx
    // https://msdn.microsoft.com/en-us/library/dd412081(v=vs.110).aspx

    // public class ReadOnlyCollection<T> : IList<T>, ICollection<T>,  IEnumerable<T>, IReadOnlyList<T>, IReadOnlyCollection<T>
    // https://msdn.microsoft.com/en-us/library/ms132474(v=vs.110).aspx

    // public interface IReadOnlyCollection<out T> : IEnumerable<T>
    // https://msdn.microsoft.com/en-us/library/hh881542(v=vs.110).aspx

    public static class ExtensionMethodsForCollectionsAndDictionaries {
        public static void AddAll<T>(
            this ICollection<T> list, 
            // e.g. ICollection<KeyValuePair<TKey, TValue>> for a Dictionary
            IEnumerable<T> list2
        ) {
            // This method can also be used for Dictionary
            // instead of using such a method as below:
            //  parameters: this IDictionary<T,U> dict1, IDictionary<T,U> dict2
            //  foreach(var x in dict2) {
            //    dict1.Add(x.Key, x.Value);
            //  }
            foreach(T t in list2) {
                list.Add(t);
            }
        }

        public static void AddAll<T>(
            this ICollection<T> list, 
            java.util.LinkedList<T> list2
        ) {
            foreach(T t in list2) {
                list.Add(t);
            }
        }

        public static void AddOrReplace<T,U>(
            this IDictionary<T,U> dict, 
            T t, 
            U u
        ) {
            // When translating code from Java (Map) to C#.NET (IDictionary)
            // the behaviour is different regarding when the key already exists.
            // The purpose of this method is to handle that difference
            // i.e. use this method when translating code from Java to C#.NET.
            
            // Java Map:
            // https://docs.oracle.com/javase/8/docs/api/java/util/Map.html#put-K-V-
            // > "If the map previously contained a mapping for the key, the old value is replaced by the specified value"
            
            // C#.NET IDictionary:
            // https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.idictionary-2.add?view=netframework-4.7.1
            // > "the Add method throws an ArgumentException when attempting to add a duplicate key."
            if(dict.ContainsKey(t)) {
                dict.Remove(t);
            }
            dict.Add(t, u);
        }

        //public static void AddOrReplace<T>(
        //    this ICollection<T> set, 
        //    T t
        //) {
        //    if(set.Contains(t)) {
        //        set.Remove(t);
        //    }
        //    set.Add(t);
        //}

        public static U GetValueIfExists<T,U>(
            this IDictionary<T, U> dict, 
            T key
        ) {
            if(dict.ContainsKey(key)) {
                return dict[key];
            }
            return default(U);
        }
    }
}
