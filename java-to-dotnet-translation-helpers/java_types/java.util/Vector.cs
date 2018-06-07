namespace java.util
{
    // https://docs.oracle.com/javase/7/docs/api/java/util/Vector.html
    public class Vector<T> : ListImplBase<T>
    {
        protected override List<T> CreateList()
        {
            return new Vector<T>();
        }
    }
}