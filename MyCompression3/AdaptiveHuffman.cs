using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.ComponentModel;
using System.IO;

namespace MyCompression
{
    public class AdaptiveHuffman
    {
        public const int MaxNumber = 10000;
        public Node NYT;
        public readonly Node Root;
        public Node DecodePointer;
        public List<Node> Nodes;
        public Dictionary<byte, Node> Leaves;

        public class Node
        {
            public int Weight;
            public byte Word;
            public int Number;
            public Node Right;
            public Node Left;
            public Node Parent;

            public Node()
            {
                Left = null;
                Right = null;
                Parent = null;
                Word = 0;
            }

            public Node GetSibling()
            {
                return Parent.Left == this ? Parent.Right : Parent.Left;
            }
        }

        public AdaptiveHuffman()
        {
            NYT = new Node
            {
                Weight = 0,
                Number = MaxNumber
            };

            Root = NYT;
            DecodePointer = Root;
            Leaves = new Dictionary<byte, Node>();
            Nodes = new List<Node>();
            Nodes.Add(Root);
        }

        /// <summary>
        /// encode file
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public bool[] Encode(byte word)
        {
            bool[] code;
            if (Leaves.ContainsKey(word))
            {
                Node existNode = Leaves[word];
                code = GetCode(existNode);
                Update(existNode);
            }
            else
            {
                bool[] escapeCode = GetCode(NYT);
                BitArray wordBits = new BitArray(new byte[] { word });
                code = new bool[escapeCode.Length + Utility.ByteSize];

                escapeCode.CopyTo(code, 0);
                wordBits.CopyTo(code, escapeCode.Length);

                code.Reverse();

                NewNode(word);
            } 

            return code;
        }

        /// <summary>
        /// decode from encoded file
        /// </summary>
        /// <param name="contents"></param>
        /// <returns></returns>
        public List<byte> Decode(byte[] contents,BackgroundWorker worker)
        {
            List<byte> result = new List<byte>();
            List<bool> decodedContent = new List<bool>();
            BitArray bits = new BitArray(contents);

            for(int i = 0; i < bits.Count; i++)
            {

                // next byte is a raw data
                if(DecodePointer == NYT)
                {
                    decodedContent.Add(bits[i]);

                    if(decodedContent.Count == Utility.ByteSize)
                    {
                        byte decodedResult = Utility.BoolArrayToByteArray(decodedContent.ToArray())[0];
                        result.Add(decodedResult);
                        NewNode(decodedResult);
                        decodedContent.Clear();
                        DecodePointer = Root;
                    }

                    continue;
                }

                DecodePointer = bits[i] ? DecodePointer.Left : DecodePointer.Right;

                if((DecodePointer.Left == null || DecodePointer.Right == null)  && DecodePointer != NYT)
                {
                    result.Add(DecodePointer.Word);
                    Update(Leaves[DecodePointer.Word]);
                    DecodePointer = Root;
                }

                worker.ReportProgress(i/Utility.ByteSize);
            }

            return result;
        }

        /// <summary>
        /// update tree
        /// </summary>
        /// <param name="currentNode"></param>
        private void Update(Node currentNode)
        {
            // Do swap
            Node highestNode = FindHighestNode(currentNode);
            SwapNode(currentNode, highestNode);

            Nodes = Nodes.OrderBy(node => node.Number).ToList();

            currentNode.Weight++;

            if(currentNode.Parent != null)
            {
                Update(currentNode.Parent);
            }
        }

        /// <summary>
        /// create new node from NYT node
        /// </summary>
        /// <param name="word">the new data</param>
        private void NewNode(byte word)
        {
            Node newNYT = new Node
            {
                Weight = 0,
                Number = NYT.Number - 2,
                Parent = NYT
            };

            Node newNode = new Node
            {
                Weight = 0,
                Parent = NYT,
                Number = NYT.Number - 1,
                Word = word
            };

            NYT.Left = newNYT;
            NYT.Right = newNode;

            NYT = newNYT;

            Nodes.Add(newNYT);
            Nodes.Add(newNode);
            if(!Leaves.ContainsKey(word))
            {
                Leaves.Add(word, newNode);
            }
           

            Nodes = Nodes.OrderBy(node => node.Number).ToList();

            Update(newNode);
        }

        /// <summary>
        /// swap two node and subtrees
        /// </summary>
        /// <param name="a">node a</param>
        /// <param name="b">node b</param>
        private void SwapNode(Node a, Node b)
        {
            // ignored case
            if (a.Parent == b || b.Parent == a || a == Root || b == Root || a == b)
            {
                return;
            }

            //change parent relation then change subtrees
            if(a.Parent.Left == a && b.Parent.Left == b)
            {
                Swap(a.Parent.Left, b.Parent.Left, a.Parent, b.Parent, false, false);
            }
            else if (a.Parent.Left == a && b.Parent.Right == b)
            {
                Swap(a.Parent.Left, b.Parent.Right, a.Parent, b.Parent, false, true);
            }
            else if (a.Parent.Right == a && b.Parent.Left == b)
            {
                Swap(a.Parent.Right, b.Parent.Left, a.Parent, b.Parent, true, false);
            }
            else if (a.Parent.Right == a && b.Parent.Right == b)
            {
                Swap(a.Parent.Right, b.Parent.Right, a.Parent, b.Parent, true, true);
            }

        }

        private void Swap(Node a, Node b, Node aParent, Node bParent,bool aIsRight, bool bIsRight)
        {
            int bNum = b.Number;
            b.Number = a.Number;
            a.Number = bNum;

            if(aIsRight)
            {
                aParent.Right = b;
                b.Parent = aParent;
            }
            else
            {
                aParent.Left = b;
                b.Parent = aParent;
            }

            if(bIsRight)
            {
                bParent.Right = a;
                a.Parent = bParent;
            }
            else
            {
                bParent.Left = a;
                a.Parent = bParent;
            }
        }

        /// <summary>
        /// find the highest node in the same block with node
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private Node FindHighestNode(Node node)
        {
            Node highestNode = null;
            List<Node> nodes = Nodes.ToList();

            int index = nodes.IndexOf(node);

            //find the highest node that have the same weight of current node
            for( int i = index; i < nodes.Count && nodes[i].Weight == node.Weight ; i++)
            {
                highestNode = nodes[i];
            }

            return highestNode;
        }

        /// <summary>
        /// get the huffman code of the node, backtracking from the node to the root
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private bool[] GetCode(Node node)
        {
            List<bool> codes = new List<bool>();
            while(node != Root)
            {
                codes.Add(node == node.Parent.Left ? true : false);
                node = node.Parent;
            }

            codes.Reverse();
            return codes.ToArray();
        }
    }
}
