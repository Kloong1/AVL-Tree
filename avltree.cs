using System;
using ElementType = System.Int32;

namespace avltree{

	class Node {
		ElementType Element { get; set; }
		Node Left { get; set; }
		Node Right { get; set; }
		int Height { get; set; }
	}

	class AVLTree{
		Node root;

		public AVLTree(){
			root = null;
		}

		public bool Search(Element e, Node node = root){
			if(node == null){
				Console.WriteLine($"{e} doesn't exist.");
				return false;
			}

			else if(node.Element == e){
				Console.WriteLine($"{e} exist.");
				return true;
			}

			else if(e < node.Element)
				return Search(e, node.Left);
			
			else
				return Search(e, node.Right);
		}

		public Node Insert(Element e, Node node = root){
			if(node == null){
				Node newNode = new Node() { Element = e, Left = null, Right = null, Height = 0 };
				if(root == null){
					root = newNode;
					return null;
				}
				else return newNode;
			}

			else if(e < node.Element){
				node.Left = Insert(e, node.Left);
				if(GetHeight(node.Left) - GetHeight(node.Right) == 2){
					if(e < node.Left.Element)
						node = SingleRotateWithLeft(node);
					else
						node = DoubleRotateWithLeft(node);
				}

			}

			else{
				node.Right = Insert(e, node.Right);
				if(GetHeight(node.Right) - GetHeight(node.Left) == 2){
					if(e > node.Right.Element)
						node = SingleRotateWithRight(node);
					else
						node = DoubleRotateWithRight(node);
				}

			}

			node.Height = Max( GetHeight(node.Left), GetHeight(node.Right) ) + 1;
			return node;
		}
		
		private Node SingleRotateWithLeft(Node nodeA){
			Node nodeB = nodeA.Left;
			nodeA.Left = nodeB.Right;
			nodeB.Right = nodeA;

			nodeA.Height = Max( GetHeight(nodeA.Left), GetHeight(nodeA.Right) ) + 1;
			nodeB.Height = Max( GetHeight(nodeB.Left), GetHeight(nodeB.Right) ) + 1;

			return nodeB;
		}

		private Node SingleRotateWithRight(Node nodeA){
			Node nodeB = nodeA.Right;
			nodeA.Right = nodeB.Left;
			nodeB.Left = nodeA;

			nodeA.Height = Max( GetHeight(nodeA.Left), GetHeight(nodeA.Right) ) + 1;
			nodeB.Height = Max( GetHeight(nodeB.Left), GetHeight(nodeB.Right) ) + 1;

			return nodeB;
		}

		private Node DoubleRotateWithLeft(Node nodeA){
			nodeA.Left = SingleRotateWithRight(nodeA.Left);
			return SingleRotateWithLeft(nodeA);
		}

		private Node DoubleRotateWithRight(Node nodeA){
			nodeA.Right = SingleRotateWithLeft(nodeA.Right);
			return SingleRotateWithRight(nodeA);
		}

		private int GetHeight(Node node){
			return node == null? -1 : node.Height;
		}

		private int Max(int a, int b){
			return a > b? a : b;
		}
	}
}
