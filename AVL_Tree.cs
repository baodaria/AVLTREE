using System;
using System.Collections.Generic;
using System.Text;
using AVL_Tree;

namespace AVL_Tree
    {
        public class AVLTree<T> : IEnumerable<T> where T : IComparable
        {
            public AVLTreeNode<T> Head
            {
                get;
                internal set;
            }

            public int Count
            {
                get;
                private set;
            }

            //create
            public void Add(T value)
            {
                if (Head == null)
                {
                    Head = new AVLTreeNode<T>(value, null, this);
                }
                else
                {
                    AddTo(Head, value);
                }
                Count++;
            }

            public void AddTo(AVLTreeNode<T> node, T value)
            {
                if (value.CompareTo(node.Value) < 0)
                {
                    if (node.Left == null)
                    {
                        node.Left = new AVLTreeNode<T>(value, node, this);
                    }
                    else
                    {
                        AddTo(node.Left, value);
                    }
                }
                else
                {
                    if (node.Right == null)
                    {
                        node.Right = new AVLTreeNode<T>(value, node, this);
                    }
                    else
                    {
                        AddTo(node.Right, value);
                    }
                }
                node.Balance();
            }

            public IEnumerator<T> InOrderTraversal()
            {
                if (Head != null)
                {
                    Stack<AVLTreeNode<T>> stack = new Stack<AVLTreeNode<T>>();
                    AVLTreeNode<T> current = Head;
                    bool goLeftNext = true;

                    stack.Push(current);
                    while (stack.Count > 0)
                    {
                        if (goLeftNext)
                        {
                            while (current.Left != null)
                            {
                                stack.Push(current);
                                current = current.Left;
                            }
                        }
                        yield return current.Value;

                        if (current.Right != null)
                        {
                            current = current.Right;
                            goLeftNext = true;
                        }
                        else
                        {
                            current = stack.Pop();
                            goLeftNext = false;
                        }
                    }
                }
            }

            public IEnumerator<T> GetEnumerator()
            {
                return InOrderTraversal();
            }

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            //read
            public bool Contains(T value)
            {
                return Find(value) != null;
            }

            private AVLTreeNode<T> Find(T value)
            {
                AVLTreeNode<T> current = Head;

                while (current != null)
                {
                    int result = current.CompareTo(value);

                    if (result > 0)
                    {
                        current = current.Left;
                    }
                    else if (result < 0)
                    {
                        current = current.Right;
                    }
                    else
                    {
                        break;
                    }
                }
                return current;
            }

            //delete
            public bool Remove(T value)
            {
                AVLTreeNode<T> current;
                current = Find(value);

                if (current == null)
                {
                    return false;
                }

                AVLTreeNode<T> treeToBalance = current.Parent;
                Count--;

                if (current.Right == null)
                {
                    if (current.Parent == null)
                    {
                        Head = current.Left;

                        if (Head != null)
                        {
                            Head.Parent = null;
                        }
                    }
                    else
                    {
                        int result = current.Parent.CompareTo(current.Value);

                        if (result > 0)
                        {
                            current.Parent.Left = current.Left;
                        }
                        else if (result < 0)
                        {
                            current.Parent.Right = current.Left;
                        }
                    }
                }
                else if (current.Right.Left == null)
                {
                    current.Right.Left = current.Left;
                    if (current.Parent == null)
                    {
                        Head = current.Right;

                        if (Head != null)
                        {
                            Head.Parent = null;
                        }
                    }
                    else
                    {
                        int result = current.Parent.CompareTo(current.Value);

                        if (result > 0)
                        {
                            current.Parent.Left = current.Right;
                        }
                        else if (result < 0)
                        {
                            current.Parent.Right = current.Right;
                        }
                    }
                }
                else
                {
                    AVLTreeNode<T> leftmost = current.Right.Left;

                    while (leftmost.Left != null)
                    {
                        leftmost = leftmost.Left;
                    }

                    leftmost.Parent.Left = leftmost.Right;
                    leftmost.Left = current.Left;
                    leftmost.Right = current.Right;

                    if (current.Parent == null)
                    {
                        Head = leftmost;

                        if (Head != null)
                        {
                            Head.Parent = null;
                        }
                    }
                    else
                    {
                        int result = current.Parent.CompareTo(current.Value);

                        if (result > 0)
                        {
                            current.Parent.Left = leftmost;
                        }
                        else if (result < 0)
                        {
                            current.Parent.Right = leftmost;
                        }
                    }
                }

                if (treeToBalance != null)
                {
                    treeToBalance.Balance();
                }
                else
                {
                    if (Head != null)
                    {
                        Head.Balance();
                    }
                }

                return true;
            }

            public void Clear()
            {
                Head = null;
                Count = 0;
            }
        }
    }


