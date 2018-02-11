using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ConsoleGhost.Impl
{
    public class TreeNode<T>
    {        
        private readonly List<TreeNode<T>> _children = new List<TreeNode<T>>();        

        public TreeNode(T value)
        {
            Value = value;
            Depth = 0;
            Parent = null;
        }

        public TreeNode<T> this[int i]
        {
            get { return _children[i]; }
        }

        /// <summary>
        /// The distance from the node to the root of the tree (starts with 0)
        /// </summary>
        public int Depth { get; private set; }

        public TreeNode<T> Parent { get; private set; }

        public T Value { get; private set; }

        public string StringValue { get { return Value.ToString(); } }

        public ReadOnlyCollection<TreeNode<T>> Children
        {
            get { return _children.AsReadOnly(); }
        }

        public TreeNode<T> AddChild(T value)
        {
            var node = new TreeNode<T>(value) { Depth = (this.Depth)+1, Parent = this };
            _children.Add(node);
            return node;
        }

        public TreeNode<T>[] AddChildren(params T[] values)
        {
            return values.Select(AddChild).ToArray();
        }

        public bool RemoveChild(TreeNode<T> node)
        {
            node.Depth = 0;
            node.Parent = null;            
            return _children.Remove(node);
        }

        public void Traverse(Action<TreeNode<T>> action)
        {
            action(this);
            foreach (var child in _children)
                child.Traverse(action);
        }

        public void PostTraverse(Action<TreeNode<T>> action)
        {
            foreach (var child in _children)
                child.PostTraverse(action);
            action(this);
        }
    }
}
