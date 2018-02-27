using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace BST
{
    class Program
    {
        static void Main(string[] args)
        {
            UI Uinterface = new UI();
            Uinterface.run_program();
        }
    }


    class Tree
    {
        private Node root;

        public Tree()
        {
            root = null;
        }

        public int count_helper()
        {
            return count(root);
        }

        private int count(Node n)
        {
            if (n == null)
                return 0;
            return 1 + count(n.leftNode) + count(n.rightNode);
        }

        public void insert_helper(int info)
        {
            insert(ref root, info);
        }

        private void insert(ref Node root, int info)
        {


            if (root == null)
            {
                Node newNode = new Node(info);
                root = newNode;
                Console.WriteLine("Node Inserted... \n");
            }

            else if (info < root.get_data())
            {
                insert(ref root.leftNode, info);
            }
            else if (info > root.get_data())
            {
                insert(ref root.rightNode, info);
            }
            else
            {
                Console.WriteLine("DuplicateData... \n");
            }

        }

        public void inorder_print_helper()
        {
            inorder_print(root);
        }

        private void inorder_print(Node root)
        {
            if (root != null)
            {
                inorder_print(root.leftNode);
                Console.Write(root.get_data() + " ");
                inorder_print(root.rightNode);
            }
        }

        public int optimal_height(int count)
        {
            return (int)Math.Log(count, 2) + 1;
        }

        public int max_height_helper()
        {
            return max_height(root);
        }

        private int max_height(Node n)
        {
            if (n == null)
                return 0;
            int leftHeight = max_height(n.leftNode);
            int rightHeight = max_height(n.rightNode);
            return (leftHeight > rightHeight) ? leftHeight + 1 : rightHeight + 1;
        }

    }


    class Node
    {
        public Node leftNode, rightNode;
        private int data;

        public Node(int data)
        {
            this.data = data;
            leftNode = null;
            rightNode = null;
        }

        public int get_data()
        {
            return data;
        }

    }


    class UI
    {
        public void run_program()
        {
            List<int> nodeElements = new List<int>();
            List<int> toRemove = new List<int>();
            //Tree BST = new Tree();

            BSTtree<int> BST = new BSTtree<int>();

            int count = 0, optHeight = 0, realHeight = 0;

            nodeElements = read_line();
            foreach (var element in nodeElements)
            {
                if (element < 0 || element > 100)
                {
                    Console.WriteLine(element + " Outside of BST range");
                    toRemove.Add(element);
                }
            }
            foreach (var element in toRemove)
            {
                nodeElements.Remove(element);
            }

            foreach (var element in nodeElements)
            {
                BST.insert(element);
            }
            Console.Write("The tree contains: ");
            BST.InOrder();
            count = BST.count();
            Console.WriteLine("");
            Console.WriteLine(count + " Nodes in BST");
            optHeight = BST.optimal_height(count);
            Console.WriteLine(optHeight + " is the optimal height of the tree");
            realHeight = BST.max_height();
            Console.WriteLine(realHeight + " is the real height of the tree");



            Console.ReadLine();
        }

        public List<int> read_line()
        {
            String line;
            String whiteSpace = " ";
            int x = 0;
            List<int> nodeElements = new List<int>();

            Console.WriteLine("Enter the numbers you would like in the BST separated by spaces with no spaces at the front, back, or more than one between inputs (An extra 0 indicates bad input): ");
            line = Console.ReadLine();
            String[] elements = Regex.Split(line, whiteSpace);
            foreach (var element in elements)
            {
                Int32.TryParse(element, out x);
                nodeElements.Add(x);
            }
            return nodeElements;
        }
    }

    public abstract class BinTree<T> where T : IComparable
    {
        public abstract void insert(T val); // Insert new item of type T
        public abstract bool Contains(T val); // Returns true if item is in tree
        public abstract void InOrder(); // Print elements in tree inorder traversal
        public abstract void PreOrder();
        public abstract void PostOrder();

    }

    public class BSTnode<T> where T : IComparable
    {
        public T data;
        public BSTnode<T> leftNode, rightNode;

        public BSTnode(T data)
        {
            this.data = data;
            leftNode = null;
            rightNode = null;
        }

        public T getData()
        {
            return data;
        }

        public BSTnode<T> getLeft()
        {
            return leftNode;
        }

        public BSTnode<T> getRight()
        {
            return rightNode;
        }

        public static bool operator <(BSTnode<T> n1, BSTnode<T> n2)
        {
            bool result = false;

            if (n1.data.CompareTo(n2.data) < 0)
            {
                result = true;
            }

            return result;
        }

        public static bool operator >(BSTnode<T> n1, BSTnode<T> n2)
        {
            bool result = false;

            if (n1.data.CompareTo(n2.data) > 0)
            {
                result = true;
            }

            return result;
        }

        public static bool operator <=(BSTnode<T> n1, BSTnode<T> n2)
        {
            bool result = false;

            if (n1.data.CompareTo(n2.data) <= 0)
            {
                result = true;
            }

            return result;
        }

        public static bool operator >=(BSTnode<T> n1, BSTnode<T> n2)
        {
            bool result = false;

            if (n1.data.CompareTo(n2.data) >= 0)
            {
                result = true;
            }

            return result;
        }

        public static bool operator ==(BSTnode<T> n1, BSTnode<T> n2)
        {

            if (System.Object.ReferenceEquals(n1, n2))
                return true;

            if (((object)n1 == null) || ((object)n2 == null))
                return false;

            return n1.Equals(n2);
        }

        public static bool operator !=(BSTnode<T> n1, BSTnode<T> n2)
        {

            if (System.Object.ReferenceEquals(n1, n2))
                return false;

            if (((object)n1 == null) || ((object)n2 == null))
                return false;

            return !(n1.Equals(n2));
        }


        public override bool Equals(object obj)
        {
            BSTnode<T> nodeObj = obj as BSTnode<T>;
            if (nodeObj == null)
            {
                return false;
            }
            return data.Equals(nodeObj.data);
        }

    }

    public class BSTtree<T> : BinTree<T> where T : IComparable
    {
        


        protected BSTnode<T> root;

        public override void insert(T val)
        {
            insert_helper(val, ref root);
        }

        private void insert_helper(T val, ref BSTnode<T> node)
        {
            BSTnode<T> comp = new BSTnode<T>(val);

            if (node == null)
                node = comp;
            else if (comp < node)
                insert_helper(val, ref node.leftNode);
            else if (comp > node)
                insert_helper(val, ref node.rightNode);
        }

        public override bool Contains(T val)
        {
            return search(val, root);
        }

        private bool search(T val, BSTnode<T> node)
        {
            if (node != null)
            {
                if (val.Equals(node.getData()))
                    return true;
                else
                    return search(val, node.leftNode) || search(val, node.rightNode);

            }
            else
                return false;
        }

        public override void InOrder()
        {
            inOrderPrint(root);
        }

        private void inOrderPrint(BSTnode<T> node)
        {
            if (node == null)
            {
                return;
            }
                inOrderPrint(node.leftNode);
                Console.WriteLine(node.data.ToString());
                inOrderPrint(node.rightNode);
            
        }

        public override void PreOrder()
        {
            preOrderPrint(root);
        }

        private void preOrderPrint(BSTnode<T> node)
        {
            if (node != null)
            {
                Console.WriteLine(node.data.ToString());
                preOrderPrint(node.leftNode);
                preOrderPrint(node.rightNode);
            }
        }

        public override void PostOrder()
        {
            postOrderPrint(root);
        }

        private void postOrderPrint(BSTnode<T> node)
        {
            if (node != null)
            {
                postOrderPrint(node.leftNode);
                postOrderPrint(node.rightNode);
                Console.WriteLine(node.data.ToString());
            }
        }

        public int count()
        {
            return countHelper(root);
        }

        private int countHelper(BSTnode<T> node)
        {
            if (node == null)
                return 0;
            else
                return 1 + countHelper(node.leftNode) + countHelper(node.rightNode);
        }

        public int optimal_height(int count)
        {
            return (int)Math.Log(count, 2) + 1;
        }

        public int max_height()
        {
            return max_height_helper(root);
        }

        private int max_height_helper(BSTnode<T> n)
        {
            if (n == null)
                return 0;
            int leftHeight = max_height_helper(n.leftNode);
            int rightHeight = max_height_helper(n.rightNode);
            return (leftHeight > rightHeight) ? leftHeight + 1 : rightHeight + 1;
        }


    }

   
}

