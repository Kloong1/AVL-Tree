using System;
using ElementType = System.Int32;

namespace avltree{

	public class Node {
		public ElementType Element { get; set; }
		public Node Left { get; set; }
		public Node Right { get; set; }
		public int Height { get; set; }
	}

	public interface ITree {
		bool Search(ElementType e, Node node);
		Node Insert(ElementType e, Node node);
		void PrintInorder(Node node);
		void PrintPreorder(Node node);
		void PrintPostorder(Node node);
	}

	public class AVLTree : ITree {
		public Node Root { get; set; }

		public AVLTree(){
			Root = null;
		}

		public void PrintInorder(Node node){
			if(node == null) return;

			if(node.Left != null)
				PrintInorder(node.Left);
			Console.Write($"{node.Element} ");
			if(node.Right != null)
				PrintInorder(node.Right);
		}

		public void PrintPreorder(Node node){
			if(node == null) return;

			Console.Write($"{node.Element} ");
			if(node.Left != null)
				PrintPreorder(node.Left);
			if(node.Right != null)
				PrintPreorder(node.Right);
		}

		public void PrintPostorder(Node node){
			if(node == null) return;

			if(node.Left != null)
				PrintPostorder(node.Left);
			if(node.Right != null)
				PrintPostorder(node.Right);
			Console.Write($"{node.Element} ");
		}

		public bool Search(ElementType e, Node node){
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

		public Node Insert(ElementType e, Node node){
			if(node == null){
				Node newNode = new Node() { Element = e, Left = null, Right = null, Height = 0 };
				return newNode;
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
		
		public Node Delete(ElementType e, Node node){
			if(node == null){
				Console.WriteLine($"{e} doesn't exist.");
				return null;
			}

			if(e < node.Element){
				node.Left = Delete(e, node.Left);
				if(GetHeight(node.Right) - GetHeight(node.Left) == 2)
					node = BalancingWithRight(node);
			}

			else if(e > node.Element){
				node.Right = Delete(e, node.Right);
				if(GetHeight(node.Left) - GetHeight(node.Right) == 2)
					node = BalancingWithLeft(node);
			}

			/* Found node to be deleted (has two children) */
			else if(node.Left != null && node.Right != null){
				Node tempNode = FindMin(node.Right);
				node.Element = tempNode.Element;
				node.Right = Delete(node.Element, node.Right);

				if(GetHeight(node.Left) - GetHeight(node.Right) == 2)
					node = BalancingWithLeft(node);
			}

			/* Found node to be deleted (has 1 or 0 child) */
			else{
				if(node.Left == null && node.Right == null)
					return null;
				else if(node.Left == null)
					node = node.Right;
				else
					node = node.Left;
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

		private Node BalancingWithLeft(Node node){
			if(GetHeight(node.Left.Left) >= GetHeight(node.Left.Right)) //Case LL in insertion
				return SingleRotateWithLeft(node);
			else														//Case LR
				return DoubleRotateWithLeft(node);
		}

		private Node BalancingWithRight(Node node){
			if(GetHeight(node.Right.Right) >= GetHeight(node.Right.Left)) //Case RR
				return SingleRotateWithRight(node);
			else														  //Case RL
				return DoubleRotateWithRight(node);
		}

		private Node FindMin(Node node){
			while(node.Left != null) //Find left-most node
				node = node.Left;

			return node;
		}

		private int GetHeight(Node node){
			return node == null? -1 : node.Height;
		}

		private int Max(int a, int b){
			return a > b? a : b;
		}
	}
}
