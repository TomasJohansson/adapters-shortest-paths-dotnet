namespace java.util
{
    // https://docs.oracle.com/javase/7/docs/api/java/util/Iterator.html
    public class Iterator<T> // though interface actually
    {
        //private IEnumerable<T> _enumerable;
        //private IEnumerator<T> _enumerator;
        private System.Collections.Generic.List<T> list; // .NET does not have the same iteration methods regarding Command-Query Separation 
        // (Current/next/MoveNext/hasNext)
        // Therefore using List here

        public Iterator(System.Collections.Generic.IEnumerable<T> enumerable)
        {
            //_enumerable = enumerable;
            //_enumerator = enumerable.GetEnumerator();
            list = new System.Collections.Generic.List<T>();
            foreach(var ii in enumerable)
            {
                list.Add(ii);
            }
            //list.addAll(enumerable);
            //_enumerable.GetEnumerator()
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
