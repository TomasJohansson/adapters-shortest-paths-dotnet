import unittest

from Node import *
from project import yensImpl

class TestYensImpl(unittest.TestCase):

	# unit test for the same graph as specified in the file "yens_tomas.txt"

    def test_yensImpl(self):
        n0 = Node(0)
        n1 = Node(1)
        n2 = Node(2)
        n3 = Node(3)
        
        n0.addEdge(1, 5)
        n0.addEdge(2, 6)
        n1.addEdge(2, 7)
        n1.addEdge(3, 8)
        n2.addEdge(3, 9)
		
        nodes = [n0, n1, n2, n3]
        
        paths = yensImpl(nodes, 0, 3, 5) # yensImpl(nodes, fromNode, toNode, numPaths):
        # print(paths[0].printPath()) # 1 -> 2 -> 4 (Cost: 13)
        # print(paths[1].printPath()) # 1 -> 3 -> 4 (Cost: 15)
        # print(paths[2].printPath()) # 1 -> 2 -> 3 -> 4 (Cost: 21)

        self.assertEqual(len(paths), 3)
        self.assertAlmostEqual(paths[0].getPathCost(), 13) # assertAlmostEqual(first, second, places=7, msg=None, delta=None) # https://docs.python.org/3/library/unittest.html#unittest.TestCase.assertEqual
        self.assertAlmostEqual(paths[1].getPathCost(), 15)
        self.assertAlmostEqual(paths[2].getPathCost(), 21)

        self.assertEqual(paths[0].nodes[0], n0)
        self.assertEqual(paths[0].nodes[1], n1)
        self.assertEqual(paths[0].nodes[2], n3)

        self.assertEqual(paths[1].nodes[0], n0)
        self.assertEqual(paths[1].nodes[1], n2)
        self.assertEqual(paths[1].nodes[2], n3)		

        self.assertEqual(paths[2].nodes[0], n0)
        self.assertEqual(paths[2].nodes[1], n1)
        self.assertEqual(paths[2].nodes[2], n2)				
        self.assertEqual(paths[2].nodes[3], n3)				

if __name__ == '__main__':
	unittest.main()

