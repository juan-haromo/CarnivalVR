using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTrees
{
    public class Selector : Node
    {
        public Selector(string name) : base(name) { }

        public override status Process()
        {
            foreach (var child in children)
            {
                switch (child.Process())
                {
                    case status.Success:
                        return status.Success;
                    case status.Running:
                        return status.Running;
                    default:
                        continue;
                }
            }
            return status.Failure;
        }
    }

    public class Sequence : Node
    {
        public Sequence(string name) : base(name) { }

        public override status Process()
        {
            if (currentChild < children.Count)
            {
                switch (children[currentChild].Process())
                {
                    case status.Running:
                        Reset();
                        return status.Running;
                    case status.Failure:
                        Reset();
                        return status.Failure;
                    case status.Success:
                        currentChild++;
                        return currentChild == children.Count ? status.Success : status.Running;
                }
            }
            Reset();
            return status.Success;
        }
    }
    public class Node
    {
        public enum status
        {
            Success,
            Failure,
            Running
        }

        public readonly string name;

        public readonly List<Node> children = new List<Node>();
        protected int currentChild = 0;
        public string GetCurrentChildName()
        {
            return children[currentChild].name;
        }
        public Node(string name)
        {
            this.name = name;
        }

        public void AddChild(Node child)
        {
            children.Add(child);
        }

        public virtual status Process() => children[currentChild].Process();

        public virtual void Reset()
        {
            currentChild = 0;
            foreach (Node child in children)
            {
                child.Reset();
            }
        }
    }

    public class Leaf : Node
    {
        readonly IStrategy strategy;

        public Leaf(string name, IStrategy strategy) : base(name)
        {
            this.strategy = strategy;
        }


        public override status Process() => strategy.Process();

        public override void Reset() => strategy.Reset();
    }

    public class BehaviourTree : Node
    {
        public BehaviourTree(string name) : base(name) { }

        public override status Process()
        {
            while (currentChild < children.Count)
            {
                var status = children[currentChild].Process();

                if (status != status.Success)
                {
                    return status;
                }
                currentChild++;
            }
            Reset();
            return status.Success;
        }
    }
}

