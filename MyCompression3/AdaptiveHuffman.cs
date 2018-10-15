using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.IO;

namespace MyCompression3
{
    public class AdaptiveHuffman
    {
        public const int MaxNumber = 10000;
        public Node NYT;
        public readonly Node Root;
        public Node DecodePointer;
        public List<Node> Leaves;
        public List<Node> Nodes;

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
            Leaves = new List<Node>();
            Nodes = new List<Node>();
            Nodes.Add(Root);
        }

        public BitArray Encode(byte word)
        {
            BitArray code;
            if (FindNode(word, out Node existNode))
            {
                code = GetCode(existNode);
                Update(existNode);
            }
            else
            {
                BitArray escapeCode = GetCode(NYT);
                BitArray wordBits = new BitArray(new byte[] { word });
                bool[] bits = new bool[escapeCode.Count + 8];

                escapeCode.CopyTo(bits, 0);
                wordBits.CopyTo(bits, escapeCode.Count);
                code = new BitArray(bits);

                NewNode(word);
            } 

            return code;
        }

        public List<byte> Decode(List<byte> contents)
        {
            List<byte> result = new List<byte>();
            List<bool> decodedContent = new List<bool>();
            BitArray codes = new BitArray(contents.ToArray());

            foreach(var code in codes)
            {
                bool codeValue = Convert.ToBoolean(code);

                // next byte is a raw data
                if(DecodePointer == NYT)
                {
                    decodedContent.Add(codeValue);

                    if(decodedContent.Count == 8)
                    {
                        byte decodedResult = Utility.BoolArrayToByte(decodedContent.ToArray());
                        result.Add(decodedResult);
                        NewNode(decodedResult);
                        decodedContent.Clear();
                        DecodePointer = Root;
                    }

                    continue;
                }

                DecodePointer = codeValue ? DecodePointer.Left : DecodePointer.Right;

                if(DecodePointer.Left == null && DecodePointer != NYT)
                {
                    result.Add(DecodePointer.Word);
                    Update(DecodePointer);
                    DecodePointer = Root;
                }
            }

            return result;
        }

        private void Update(Node node)
        {
            Node highestNode = FindHighestNode(node);
            SwapNode(node, highestNode);
            node.Weight++;
            if(node.Parent != null)
            {
                Update(node.Parent);
            }
        }


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
            Leaves.Add(newNode);

            Nodes = Nodes.OrderBy(node => node.Number).ToList();

            Update(newNode);
        }

        private bool FindNode(byte word, out Node resultNode)
        {
            foreach (Node node in Leaves)
            {
                if (node.Word == word)
                {
                    resultNode = node;
                    return true;
                }
            }
            resultNode = null;
            return false;
        }

        private void SwapNode(Node a, Node b)
        {
            Node temp = a;
            a = b;
            b = temp;
        }

        private Node FindHighestNode(Node node)
        {
            Node highestNode = null;
            List<Node> nodes = Nodes.ToList();
            nodes.Reverse();
            int index = nodes.IndexOf(node);

            for( int i = index; i < nodes.Count && nodes[i].Weight == node.Weight ; i++)
            {
                highestNode = nodes[i];
            }

            return highestNode;
        }

        private BitArray GetCode(Node node)
        {
            List<bool> codes = new List<bool>();
            while(node != Root)
            {
                codes.Add(node == node.Parent.Left ? true : false);
                node = node.Parent;
            }

            codes.Reverse();

            BitArray bitArray = new BitArray(codes.ToArray());

            return bitArray;
        }
    }
}
