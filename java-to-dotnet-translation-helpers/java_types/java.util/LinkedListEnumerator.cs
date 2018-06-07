using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace java.util
{

    [Obsolete] // ?
    public class LinkedListEnumerator<T> : IEnumerator
    {
        private System.Collections.Generic.LinkedList<T> _linkedList;
        private System.Collections.Generic.LinkedList<T>.Enumerator _enumerator;

        public LinkedListEnumerator(System.Collections.Generic.LinkedList<T> linkedList)
        {
            _linkedList = linkedList;
            _enumerator = _linkedList.GetEnumerator();
        }

        public object Current
        {
            get
            {
                return _enumerator.Current;
            }
        }

        public bool MoveNext()
        {
            return _enumerator.MoveNext();
        }

        public void Reset()
        {
            _enumerator = _linkedList.GetEnumerator();
        }
    }
}
