using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.ComponentModel;

namespace MyCompression
{
    public class AdaptiveHuffman
    {
        private const int MaxNumber = 1000;
        private Node _nyt;
        private readonly Node _root;
        private Node _decodePointer;
        private readonly Node[] _nodes;
        private readonly Dictionary<byte, Node> _leaves;

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
        }

        public AdaptiveHuffman()
        {
            _nyt = new Node
            {
                Weight = 0,
                Number = MaxNumber - 1
            };

            _root = _nyt;
            _decodePointer = _root;
            _leaves = new Dictionary<byte, Node>();
            _nodes = new Node[MaxNumber];
            _nodes[_root.Number] = _root ;
        }

        /// <summary>
        /// encode file
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public bool[] Encode(byte word)
        {
            bool[] code;
            if (_leaves.ContainsKey(word))
            {
                Node existNode = _leaves[word];
                code = GetCode(existNode);
                Update(existNode);
            }
            else
            {
                bool[] nytCode = GetCode(_nyt);
                BitArray wordBits = new BitArray(new byte[] { word });
                code = new bool[nytCode.Length + Utility.ByteSize];

                nytCode.CopyTo(code, 0);
                wordBits.CopyTo(code, nytCode.Length);

                NewNode(word);
            } 

            return code;
        }

        /// <summary>
        /// decode from encoded file
        /// </summary>
        /// <param name="contents"></param>
        /// <param name="worker">to report progression</param>
        /// <returns></returns>
        public List<byte> Decode(byte[] contents,BackgroundWorker worker)
        {
            List<byte> result = new List<byte>();
            List<bool> decodedContent = new List<bool>();
            BitArray bits = new BitArray(contents);

            for(int i = 0; i < bits.Count; i++)
            {

                // next byte is a raw data, this data is not in the tree so read next byte
                if(_decodePointer == _nyt)
                {
                    decodedContent.Add(bits[i]);

                    if(decodedContent.Count == Utility.ByteSize)
                    {
                        byte decodedResult = Utility.BoolArrayToByteArray(decodedContent.ToArray())[0];
                        result.Add(decodedResult);
                        NewNode(decodedResult);
                        decodedContent.Clear();
                        _decodePointer = _root;
                    }

                    continue;
                }

                // move pointer
                _decodePointer = bits[i] ? _decodePointer.Left : _decodePointer.Right;


                // found exist data
                if((_decodePointer.Left == null || _decodePointer.Right == null)  && _decodePointer != _nyt)
                {
                    result.Add(_decodePointer.Word);
                    Update(_leaves[_decodePointer.Word]);
                    _decodePointer = _root;
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
                Number = _nyt.Number - 2,
                Parent = _nyt
            };

            Node newNode = new Node
            {
                Weight = 0,
                Parent = _nyt,
                Number = _nyt.Number - 1,
                Word = word
            };

            _nyt.Left = newNYT;
            _nyt.Right = newNode;
            _nyt = newNYT;

            _nodes[newNYT.Number] = newNYT;
            _nodes[newNode.Number] = newNode;

            if(!_leaves.ContainsKey(word))
            {
                _leaves.Add(word, newNode);
            }

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
            if (a.Parent == b || b.Parent == a || a == _root || b == _root || a == b)
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

            Node temp = _nodes[a.Number];
            _nodes[a.Number] = _nodes[b.Number];
            _nodes[b.Number] = temp;

            if (aIsRight)
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
            List<Node> nodes = _nodes.ToList();

            //find the highest node that have the same weight of current node
            for( int i = node.Number ; i < nodes.Count && nodes[i].Weight == node.Weight ; i++)
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
            while(node != _root)
            {
                codes.Add(node == node.Parent.Left);
                node = node.Parent;
            }

            codes.Reverse();
            return codes.ToArray();
        }
    }
}
