using System.Text.Json;

namespace AdventOfCode2022
{
    internal static partial class Day13
    {
        public static void Run(string path)
        {
            Console.WriteLine("Day Thirteen");
            List<string> input = FileHelper.ReadFileAsList(path);

            var first = JsonSerializer.Deserialize<JsonDocument>(input[0]);
            var second = JsonSerializer.Deserialize<dynamic>(input[1]);
            
            Solve(input);
        }

        private static void Solve(List<string> input)
        {
            // Create a root node with the values 1, 8, and 9
            var root = new MWayTreeNode<int[]>(new int[] { 1, 8, 9 });

            // Create a child node with the value 2
            var child1 = new MWayTreeNode<int[]>(new int[1] { 2 });
            root.AddChild(child1);

            // Create a grandchild node with the value 3
            var grandchild1 = new MWayTreeNode<int[]>(new int[1] { 3 });
            child1.AddChild(grandchild1);

            // Create a great-grandchild node with the value 4
            var greatGrandchild1 = new MWayTreeNode<int[]>(new int[1] { 4 });
            grandchild1.AddChild(greatGrandchild1);

            // Create three great-great-grandchildren with the values 5, 6, and 7
            var greatGreatGrandchild1 = new MWayTreeNode<int[]>(new int[1] { 5 });
            var greatGreatGrandchild2 = new MWayTreeNode<int[]>(new int[1] { 6 });
            var greatGreatGrandchild3 = new MWayTreeNode<int[]>(new int[1] { 7 });
            greatGrandchild1.AddChild(greatGreatGrandchild1);
            greatGrandchild1.AddChild(greatGreatGrandchild2);
            greatGrandchild1.AddChild(greatGreatGrandchild3);
        }
    }

    internal class MWayTreeNode<T> {
        public T Data { get; set; }
        public List<MWayTreeNode<T>> Children { get; set; }

        public MWayTreeNode(T data) {
            Data = data;
            Children = new List<MWayTreeNode<T>>();
        }

        public void AddChild(MWayTreeNode<T> child) => Children.Add(child);
    }
}

/* 
To compare values between two m-way trees, you would need to traverse the trees 
and compare the values of the nodes at each level. This can be done using a depth-first 
or breadth-first traversal, depending on the specific requirements of the comparison.

In a depth-first traversal, the algorithm would start at the root node of each tree and 
compare the values of the nodes. If the values are the same, it would move on to the next 
level of the tree and compare the values of the child nodes. This process would continue 
until all the nodes in the tree have been compared.

In a breadth-first traversal, the algorithm would start at the root node of each tree and 
compare the values of the nodes. Then, it would move on to the next level of the tree and 
compare the values of all the child nodes at that level. This process would continue 
until all the nodes in the tree have been compared.

Regardless of the specific approach used, the goal of comparing values between two m-way 
trees is to determine whether the trees are identical or not. If the values of all the 
nodes in the trees are the same, the trees are considered to be identical. Otherwise, the 
trees are considered to be different.
 */

/* 
 public class Node
{
    public int Value { get; set; }
    public List<Node> Children { get; set; }

    public Node(int value, List<Node> children)
    {
        Value = value;
        Children = children;
    }
}

public static void DepthFirstTraversal(Node root)
{
    Console.WriteLine(root.Value);

    foreach (var child in root.Children)
    {
        DepthFirstTraversal(child);
    }
}
*/

/* 
 public class Node
{
    public int Value { get; set; }
    public List<Node> Children { get; set; }

    public Node(int value, List<Node> children)
    {
        Value = value;
        Children = children;
    }
}

public static void BreadthFirstTraversal(Node root)
{
    var queue = new Queue<Node>();
    queue.Enqueue(root);

    while (queue.Count > 0)
    {
        var node = queue.Dequeue();
        Console.WriteLine(node.Value);

        foreach (var child in node.Children)
        {
            queue.Enqueue(child);
        }
    }
}
*/