using NUnit.Framework;

namespace java.util
{
    [TestFixture]
    public class ListTest
    {
        [Test]
        public void EqualsTestVector()
        {
            List<ListItemForTest> list_1 = new Vector<ListItemForTest>();
            List<ListItemForTest> list_2 = new Vector<ListItemForTest>();
            EqualsTestHelper(list_1, list_2);
        }
        
        [Test]
        public void EqualsTestArrayList()
        {
            List<ListItemForTest> list_1 = new ArrayList<ListItemForTest>();
            List<ListItemForTest> list_2 = new ArrayList<ListItemForTest>();
            EqualsTestHelper(list_1, list_2);
        }

        [Test]
        public void EqualsTestLinkedList()
        {
            List<ListItemForTest> list_1 = new LinkedList<ListItemForTest>();
            List<ListItemForTest> list_2 = new LinkedList<ListItemForTest>();
            EqualsTestHelper(list_1, list_2);
        }

        
        private void EqualsTestHelper(List<ListItemForTest> list_1, List<ListItemForTest> list_2)
        {
            list_1.add(new ListItemForTest("A"));
            list_1.add(new ListItemForTest("B"));
            list_1.add(new ListItemForTest("C"));

            
            list_2.add(new ListItemForTest("A"));
            list_2.add(new ListItemForTest("B"));
            list_2.add(new ListItemForTest("C"));

            Assert.AreEqual(list_1, list_2);
        }
    }

    public class ListItemForTest {
        private readonly string str;

        public ListItemForTest(string str)
        {
            this.str = str;
        }

        public override bool Equals(object obj)
        {
		    if (this == obj) return true;
		    if (obj == null) return false;
		    if (obj is ListItemForTest) {
                ListItemForTest other = (ListItemForTest) obj;
                return this.str == other.str;
            }
		    return false;
        }

        public override int GetHashCode()
        {
            return str.GetHashCode();
        }
    }

}
