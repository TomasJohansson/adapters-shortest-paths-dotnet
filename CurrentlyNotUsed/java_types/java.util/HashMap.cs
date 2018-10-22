using System;

namespace java.util
{
    // see comment in type "MapN" regarding the "N" 
    // suffix in the type "HashMapN" vs "HashMap"

    // https://docs.oracle.com/javase/7/docs/api/java/util/HashMap.html
    public class HashMap<T, U> : Map<T, U> where U : class // nullable 
    {
    }
}
