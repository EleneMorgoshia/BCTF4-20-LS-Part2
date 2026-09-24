namespace G04_tree
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TreeNode root = new TreeNode("Root");

            // Each node can have a maximum of two child nodes.
            root.Left = new TreeNode("Left");
            root.Right = new TreeNode("Right");

            // Child nodes can also have their own children.
            root.Left.Left = new TreeNode("Left.Left");
            root.Left.Right = new TreeNode("Left.Right");
            root.Right.Right = new TreeNode("Right.Right");

            root.Left.Left.Left = new TreeNode("Left.Left.Left");
            root.Left.Left.Right = new TreeNode("Left.Left.Right");

            // Printing begins at the root of the tree.
            PrintTree(root);

            // Count the total number of nodes in the tree.
            //Console.WriteLine($"\nTotal nodes: {GetNodesCount(root)}");
        }

        //static int GetNodesCount(TreeNode? node)
        //{
        //    if (node == null)
        //        throw new ArgumentNullException(nameof(node));
        //    int count = 0;
        //    if (node.Value == "Root")
        //    {
        //        //ეს დასქიფე
        //    } 


            
        //}

        static void PrintTree(TreeNode? node, string indentation = "", string branch = "Root: ")
        {
            // This is the recursion's stopping condition.
            if (node is null)
            {
                return;
            }

            // Print the value stored in the current node.
            Console.WriteLine($"{indentation}{branch}{node.Value}");

            // Indentation visually represents the node's depth.
            string childIndentation = indentation + "    ";

            // Recursively visit the complete left subtree.
            PrintTree(node.Left, childIndentation, "L: ");

            // Recursively visit the complete right subtree.
            PrintTree(node.Right, childIndentation, "R: ");
        }
    }

    class TreeNode
    {
        public string Value { get; }
        public TreeNode? Left { get; set; }
        public TreeNode? Right { get; set; }

        public TreeNode(string value)
        {
            Value = value;
        }
    }
}