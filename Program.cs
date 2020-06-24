using System;

namespace AVL_Tree.ver2
{
    class Program
    {
        static void Main(string[] args)
        {
            AVLTree<int> balancetree = new AVLTree<int>();

            balancetree.Add(5);
            balancetree.Add(3);
            balancetree.Add(7);
            balancetree.Add(2);
            balancetree.Add(12);
            balancetree.Add(15);
            balancetree.Add(21);

            foreach (var number in balancetree)
            {
                Console.WriteLine(number);
            }

            Console.WriteLine(balancetree.Contains(5));

            balancetree.Remove(3);

            foreach (var number in balancetree)
            {
                Console.WriteLine(number);
            }

            balancetree.Clear();

            foreach (var number in balancetree)
            {
                Console.WriteLine(number);
            }
        }
    }
}