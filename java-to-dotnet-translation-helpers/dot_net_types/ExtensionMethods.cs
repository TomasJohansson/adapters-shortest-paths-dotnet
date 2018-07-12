using System.Collections.Generic;

namespace java_to_dotnet_translation_helpers.dot_net_types
{
    public static class ExtensionMethods
    {
        public static void AddAll<T>(this IList<T> list, IList<T> list2)
        {
            foreach(T t in list2)
            {
                list.Add(t);
            }
        }
        public static void AddAll<T>(this IList<T> list, java.util.LinkedList<T> list2)
        {
            foreach(T t in list2)
            {
                list.Add(t);
            }
        }
        public static void AddAll<T>(this ISet<T> list, ISet<T> list2)
        {
            foreach(T t in list2)
            {
                list.Add(t);
            }
        }
        public static void AddAll<T>(this ISet<T> list, IList<T> list2)
        {
            foreach(T t in list2)
            {
                list.Add(t);
            }
        }

        public static void PutAll<T,U>(this IDictionary<T,U> dict1, IDictionary<T,U> dict2)
        {
            foreach(var x in dict2)
            {
                dict1.Add(x.Key, x.Value);
            }
        }

        public static void AddOrReplace<T,U>(this IDictionary<T,U> dict, T t, U u) {
            if(dict.ContainsKey(t))
            {
                dict.Remove(t);
            }
            dict.Add(t, u);
        }

        public static void AddOrReplace<T>(this ISet<T> set, T t) {
            if(set.Contains(t))
            {
                set.Remove(t);
            }
            set.Add(t);
        }

        public static U GetValueIfExists<T,U>(this IDictionary<T, U> dict, T key) {
            if(dict.ContainsKey(key))
            {
                return dict[key];
            }
            return default(U);
        }
    }
}
