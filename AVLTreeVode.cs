using System;
using System.Collections.Generic;
using System.Text;
using AVL_Tree;

namespace AVL_Tree
{
    public class AVLTreeNode<TNode> : IComparable<TNode> where TNode : IComparable
    {
        AVLTree<TNode> tree;
        AVLTreeNode<TNode> left;
        AVLTreeNode<TNode> right;

        public AVLTreeNode(TNode value, AVLTreeNode<TNode> parent, AVLTree<TNode> tree)
        {
            Value = value;
            Parent = parent;
            this.tree = tree;
        }

        public AVLTreeNode<TNode> Left
        {
            get { return left; }
            internal set
            {
                left = value;
                if (left != null)
                {
                    left.Parent = this;
                }
            }
        }

        public AVLTreeNode<TNode> Right
        {
            get { return right; }
            internal set
            {
                right = value;
                if (right != null)
                {
                    right.Parent = this;
                }
            }
        }

        public AVLTreeNode<TNode> Parent
        {
            get;
            internal set;
        }

        public TNode Value
        {
            get;
            private set;
        }

        public int CompareTo(TNode other)
        {
            return Value.CompareTo(other);
        }

        private int MaxChildHeight(AVLTreeNode<TNode> node)
        {
            if (node != null)
            {
                return 1 + Math.Max(MaxChildHeight(node.Left), MaxChildHeight(node.Right));
            }

            return 0;
        }

        private int RightHeight
        {
            get
            {
                return MaxChildHeight(Right);
            }
        }

        private int LeftHeight
        {
            get
            {
                return MaxChildHeight(Left);
            }
        }

        enum TreeState
        {
            Balanced,
            LeftHeavy,
            RightHeavy,
        }

        private TreeState State
        {
            get
            {
                if (LeftHeight - RightHeight > 1)
                {
                    return TreeState.LeftHeavy;
                }

                if (RightHeight - LeftHeight > 1)
                {
                    return TreeState.RightHeavy;
                }

                return TreeState.Balanced;
            }
        }

        private int BalanceFactor
        {
            get
            {
                return RightHeight - LeftHeight;
            }
        }

        internal void Balance()
        {
            if (State == TreeState.RightHeavy)
            {
                if (Right != null && Right.BalanceFactor < 0)
                {
                    LeftRightRotation();
                }
                else
                {
                    LeftRotation();
                }
            }
            else if (State == TreeState.LeftHeavy)
            {
                if (Left != null && Left.BalanceFactor > 0)
                {
                    RightLeftRotation();
                }
                else
                {
                    RightRotation();
                }
            }
        }

        private void ReplaceRoot(AVLTreeNode<TNode> newRoot)
        {
            if (this.Parent != null)
            {
                if (this.Parent.Left == this)
                {
                    this.Parent.Left = newRoot;
                }
                else if (this.Parent.Right == this)
                {
                    this.Parent.Right = newRoot;
                }
            }
            else
            {
                tree.Head = newRoot;
            }

            newRoot.Parent = this.Parent;
            this.Parent = newRoot;
        }

        private void LeftRotation()
        {
            AVLTreeNode<TNode> newRoot = Right;   //делаем потомка справа новым корнем
            ReplaceRoot(newRoot);
            Right = newRoot.Left; // ставим вместо правого потомка  - левого потомка нашего нового корня
            newRoot.Left = this; // делаем текущий узел  - левым потомком нашего ного корня
        }

        private void RightRotation()
        {
            AVLTreeNode<TNode> newRoot = Left; //делаем потомка слева новым корнем
            ReplaceRoot(newRoot);
            Left = newRoot.Right; // ставим вместо левого потомка  - правого потомка нашего нового корня
                                 
            newRoot.Right = this; // делаем текущий узел  - левым потомком нашего ного корня
        }
            

        private void LeftRightRotation()
        {
            AVLTreeNode<TNode> newRoot = Left.Right; //делаем правого потомка левого потомка новым корнем
            ReplaceRoot(newRoot);
            newRoot.Left = Left; //ставим на место левого потомка нового корня - левого потомка
            Left = newRoot.Right; //ставим  на место левого потомка  - правого потомка нового корня 
            //Left = newRoot.Right;

            newRoot.Right = this;  //делаем текущий узел правым потомком нового корня
        }
      

    private void RightLeftRotation()
        {
            AVLTreeNode<TNode> newRoot = Right.Left; // делаем левого потомка правого потомка новым корнем
            ReplaceRoot(newRoot);
            newRoot.Right = Right; //ставим на место правого потомка нового корня - правого потомка
            Right = newRoot.Left; //ставим  на место правого потомка  - левого потомка нового корня 
            newRoot.Left = this; //делаем текущий узел левым потомком нового корня
        }
    }
} 