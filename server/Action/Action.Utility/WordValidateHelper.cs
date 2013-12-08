using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using Action.Core;

namespace Action.Utility
{
    public static class WordValidateHelper
    {
        public static void Init()
        {
            _ks = new KeywordSearch(KeywordsFilterClass.WordArray);
        }

        private static KeywordSearch _ks = null;
        /// <summary>
        /// 关键字验证
        /// </summary>
        /// <param name="word">验证字段或语句</param>
        /// <returns>true:验证通过;false:验证失败</returns>
        public static bool FilterForBool(string word)
        {
            return _ks.FindAllKeywords(word).Count<=0;
        }

        public static bool RegexName(string str)
        {
            byte[] byte_len = Encoding.Default.GetBytes(str);
            if (byte_len.Length < 4 || byte_len.Length>12)
            {
                return false;
            }
            bool flag = Regex.IsMatch(str, @"^[a-zA-Z0-9_\u4e00-\u9fa5]+$");
            return flag;
        }

       
        /// <summary>
        /// 关键字过滤
        /// </summary>
        /// <param name="word">验证字段或语句</param>
        /// <returns>过滤后的语句</returns>
        public static string FilterForStr(string word)
        {
            var restule = _ks.FilterKeywords(word);
            return restule;
        }
    }
    #region 关键字过滤类
    /// <summary>    
    /// 关键字过滤类   
    /// /// </summary>    
    public class KeywordsFilterClass
    {

        //词组
        private static IOrderedEnumerable<string> wordArray;
        //词组
        public static IOrderedEnumerable<string> WordArray
        {
            get
            {
                if (wordArray == null)
                {
                    var file = Path.Combine(Global.ResDir, "Common/WordFilter.txt");
                    wordArray = System.IO.File.ReadAllText(file, Encoding.Default).Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).OrderByDescending(p=>p.Length);
                }
                return wordArray;
            }
        }
      
    }
    #endregion


    /// <summary>
    /// 表示一个查找结果
    /// </summary>
    public struct KeywordSearchResult
    {
        private int index;
        private string keyword;
        public static readonly KeywordSearchResult Empty = new KeywordSearchResult(-1, string.Empty);

        public KeywordSearchResult(int index, string keyword)
        {
            this.index = index;
            this.keyword = keyword;
        }

        /// <summary>
        /// 位置
        /// </summary>
        public int Index
        {
            get { return index; }
        }

        /// <summary>
        /// 关键词
        /// </summary>
        public string Keyword
        {
            get { return keyword; }
        }
    }


    /// <summary>
    /// Aho-Corasick算法实现
    /// </summary>
    public class KeywordSearch
    {
        /// <summary>
        /// 构造节点
        /// </summary>
        private class Node
        {
            private Dictionary<char, Node> transDict;

            public Node(char c, Node parent)
            {
                this.Char = c;
                this.Parent = parent;
                this.Transitions = new List<Node>();
                this.Results = new List<string>();

                this.transDict = new Dictionary<char, Node>();
            }

            public char Char
            {
                get;
                private set;
            }

            public Node Parent
            {
                get;
                private set;
            }

            public Node Failure
            {
                get;
                set;
            }

            public List<Node> Transitions
            {
                get;
                private set;
            }

            public List<string> Results
            {
                get;
                private set;
            }

            public void AddResult(string result)
            {
                if (!Results.Contains(result))
                {
                    Results.Add(result);
                }
            }

            public void AddTransition(Node node)
            {
                this.transDict.Add(node.Char, node);
                this.Transitions = this.transDict.Values.ToList();
            }

            public Node GetTransition(char c)
            {
                Node node;
                if (this.transDict.TryGetValue(c, out node))
                {
                    return node;
                }

                return null;
            }

            public bool ContainsTransition(char c)
            {
                return GetTransition(c) != null;
            }
        }

        private Node root; // 根节点
        private string[] keywords; // 所有关键词

        public KeywordSearch(IEnumerable<string> keywords)
        {
            this.keywords = keywords.ToArray();
            this.Initialize();
        }

        /// <summary>
        /// 根据关键词来初始化所有节点
        /// </summary>
        private void Initialize()
        {
            this.root = new Node(' ', null);

            // 添加模式
            foreach (string k in this.keywords)
            {
                Node n = this.root;
                foreach (char c in k)
                {
                    Node temp = null;
                    foreach (Node tnode in n.Transitions)
                    {
                        if (tnode.Char == c)
                        {
                            temp = tnode; break;
                        }
                    }

                    if (temp == null)
                    {
                        temp = new Node(c, n);
                        n.AddTransition(temp);
                    }
                    n = temp;
                }
                n.AddResult(k);
            }

            // 第一层失败指向根节点
            List<Node> nodes = new List<Node>();
            foreach (Node node in this.root.Transitions)
            {
                // 失败指向root
                node.Failure = this.root;
                foreach (Node trans in node.Transitions)
                {
                    nodes.Add(trans);
                }
            }
            // 其它节点 BFS
            while (nodes.Count != 0)
            {
                List<Node> newNodes = new List<Node>();
                foreach (Node nd in nodes)
                {
                    Node r = nd.Parent.Failure;
                    char c = nd.Char;

                    while (r != null && !r.ContainsTransition(c))
                    {
                        r = r.Failure;
                    }

                    if (r == null)
                    {
                        // 失败指向root
                        nd.Failure = this.root;
                    }
                    else
                    {
                        nd.Failure = r.GetTransition(c);
                        foreach (string result in nd.Failure.Results)
                        {
                            nd.AddResult(result);
                        }
                    }

                    foreach (Node child in nd.Transitions)
                    {
                        newNodes.Add(child);
                    }
                }
                nodes = newNodes;
            }
            // 根节点的失败指向自己
            this.root.Failure = this.root;
        }

        /// <summary>
        /// 找出所有出现过的关键词
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public List<KeywordSearchResult> FindAllKeywords(string text)
        {
            List<KeywordSearchResult> list = new List<KeywordSearchResult>();

            Node current = this.root;
            for (int index = 0; index < text.Length; ++index)
            {
                Node trans;
                do
                {
                    trans = current.GetTransition(text[index]);

                    if (current == this.root)
                        break;

                    if (trans == null)
                    {
                        current = current.Failure;
                    }
                } while (trans == null);

                if (trans != null)
                {
                    current = trans;
                }

                foreach (string s in current.Results)
                {
                    list.Add(new KeywordSearchResult(index - s.Length + 1, s));
                }
            }

            return list;
        }

        /// <summary>
        /// 简单地过虑关键词
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public string FilterKeywords(string text)
        {
            StringBuilder sb = new StringBuilder();

            Node current = this.root;
            for (int index = 0; index < text.Length; index++)
            {
                Node trans;
                do
                {
                    trans = current.GetTransition(text[index]);

                    if (current == this.root)
                        break;

                    if (trans == null)
                    {
                        current = current.Failure;
                    }

                } while (trans == null);

                if (trans != null)
                {
                    current = trans;
                }

                // 处理字符
                if (current.Results.Count > 0)
                {
                    string first = current.Results[0];
                    sb.Remove(sb.Length - first.Length + 1, first.Length - 1);// 把匹配到的替换为**
                    sb.Append(new string('*', current.Results[0].Length));

                }
                else
                {
                    sb.Append(text[index]);
                }
            }

            return sb.ToString();
        }
    }
}
