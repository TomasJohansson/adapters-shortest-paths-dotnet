namespace java.util
{
    // https://docs.oracle.com/javase/7/docs/api/java/util/Iterator.html
    public class Iterator<T> // though interface actually
    {
        private System.Collections.Generic.List<T> list; // .NET does not have the same iteration methods regarding Command-Query Separation 
        // (Current/next/MoveNext/hasNext)
        // Therefore using List here

        public Iterator(System.Collections.Generic.IEnumerable<T> enumerable)
        {
            list = new System.Collections.Generic.List<T>();
            foreach(T t in enumerable)
            {
                list.Add(t);
            }
        }

        int current = -1;

        public T next()
        {
            current++;
            return list[current];
        }
        public bool hasNext()
        {
            return current < list.Count;
        }
    }
}