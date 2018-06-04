namespace java.util
{
    // see comment in type "MapN" regarding the "N" 
    // suffix in the type "HashMapN" vs "HashMap"

    // https://docs.oracle.com/javase/7/docs/api/java/util/HashMap.html
    public class HashMapN<T, U> : MapN<T, U> where U : struct // nullable 
    {
    }
}
