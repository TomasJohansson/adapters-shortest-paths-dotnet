namespace java.util
{
    public abstract class AbstractList<T> 
    {
        public bool equals(object o)
        {
            return this.Equals(o);
        }

        public int hashCode()
        {
            return this.GetHashCode();
        }

        public override bool Equals(object obj)
        {
		    if (this == obj) return true;
		    if (obj == null) return false;
		    if (obj is List<T>) {
                List<T> otherList = (List<T>) obj;
                if(this.size() != otherList.size()) return false;
                for(int i=0; i<this.size(); i++)
                {
                    T t1 = this.get(i);
                    T t2 = otherList.get(i);
                    if(!t1.Equals(t2)) {
                        return false;
                    }
                }
                return true;
            }
            else {
                return false;
            }
        }

        public override int GetHashCode()
        {
            // return base.GetHashCode();
            // IMPORTANT: The above standard hashcode 
            // will NOT work! Tests will fail !

            // The below implementation are copied from here:
            // https://docs.oracle.com/javase/7/docs/api/java/util/List.html#hashCode()
            int hashCode = 1;
            for(int i=0; i<this.size(); i++) {
                T e = this.get(i);
                hashCode = 31*hashCode + (e==null ? 0 : e.GetHashCode());
            }
            return hashCode;
        }

        public abstract int size();
        public abstract T get(int i);
    }
}
