using System; // ObsoleteAttribute

namespace java.util
{
    public class Arrays
    {
        // https://docs.oracle.com/javase/7/docs/api/java/util/Arrays.html#asList(T...)
        [Obsolete] // maybe this method will not be needed anymore
        public static List<T> asList<T>(params T[] items)
        {
            var list = new Vector<T>();
            foreach(T item in items)
            {
                list.add(item);
            }
            return list;
        }
    }
}
