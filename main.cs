using System;
using System.Text;

namespace avltree{
	class MainClass{

		public static void Main(){
			AVLTree tree = new AVLTree();

			string sInput = default(string);
			int input = default(int);
			do{
				printMenu();
				Console.Write("Input: ");
				sInput = Console.ReadLine();
				input = Convert.ToInt32(sInput);

				switch (input)
				{
					case 1:
						CallInsert(tree);
						break;
					case 2:
						CallSearch(tree);
						break;
					case 3:
						CallDelete(tree);
						break;
					case 4:
						tree.PrintInorder(tree.Root);
						break;
					case 5:
						tree.PrintPreorder(tree.Root);
						break;
					case 6:
						tree.PrintPostorder(tree.Root);
						break;
					case 7:
						break;
					default:
						Console.WriteLine("Wrong Input!");
						break;
				}
				Console.WriteLine("\n");
			}while(input != 7);
		}

		static void printMenu(){
			Console.WriteLine("AVLTree");
			Console.WriteLine("1. Insert");
			Console.WriteLine("2. Search");
			Console.WriteLine("3. Delete");
			Console.WriteLine("4. Print Inorder");
			Console.WriteLine("5. Print Preorder");
			Console.WriteLine("6. Print Postorder");
			Console.WriteLine("7. Exit");
		}

		static void CallInsert(AVLTree tree){
			Console.WriteLine("\nInsert");
			Console.Write("Input: ");

			string sInput = Console.ReadLine();
			int input = Convert.ToInt32(sInput);

			tree.Root = tree.Insert(input, tree.Root);
		}

		static void CallSearch(AVLTree tree){
			Console.WriteLine("\nSearch");
			Console.Write("Input: ");

			string sInput = Console.ReadLine();
			int input = Convert.ToInt32(sInput);

			tree.Search(input, tree.Root);
		}

		static void CallDelete(AVLTree tree){
			Console.WriteLine("\nDelete");
			Console.Write("Input: ");

			string sInput = Console.ReadLine();
			int input = Convert.ToInt32(sInput);

			tree.Root = tree.Delete(input, tree.Root);
		}
	}
}
