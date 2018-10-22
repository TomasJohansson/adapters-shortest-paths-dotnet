namespace java.util
{
    // https://docs.oracle.com/javase/7/docs/api/java/util/ArrayList.html
    public class ArrayList<T> : ListImplBase<T>
    {
        protected override List<T> CreateList()
        {
            return new ArrayList<T>();
        }
    }
}
