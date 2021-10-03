using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TG
{
    public class Graph
    {
        public Dictionary<string, Dictionary<string, int>> graph;
        public bool isDirected = true;

        public bool IsDirected
        {
            get
            {
                return isDirected;
            }
            set
            {
                isDirected = value;
            }
        }

        public Dictionary<string, Dictionary<string, int>> DictGraph
        {
            get
            {
                return graph;
            }

        }

        public Graph()
        {
            graph = new Dictionary<string, Dictionary<string, int>>();
        }

        public Graph(string file)
        {
            graph = new Dictionary<string, Dictionary<string, int>>();
            using (StreamReader fileIn = new StreamReader(file, Encoding.GetEncoding(1251)))
            {
                string temp;
                string[] vert;
                Dictionary<string, int> verts = new Dictionary<string, int>();
                while ((temp = fileIn.ReadLine()) != null)
                {
                    string[] v = temp.Split(' ');
                    Dictionary<string, int> VertsNext = new Dictionary<string, int>();
                    for (int i = 1; i < v.Length; i++)
                    {
                        vert = v[i].Split(':');
                        VertsNext.Add(vert[0], int.Parse(vert[1]));
                    }

                    graph.Add(v[0], VertsNext);
                }
            }
        }

        public void VertAdd(string v)
        {
            graph.Add(v, new Dictionary<string, int>());
        }

        public void EdgeAdd(string v1, string v2, int weight)
        {
            if (isDirected)
            {
                graph[v1].Add(v2, weight);
            }
            else
            {
                graph[v1].Add(v2, weight);
                graph[v2].Add(v1, weight);
            }
        }

        public void VertDel(string v)
        {
            graph.Remove(v);
            foreach (var item in graph)
            {
                graph[item.Key].Remove(v);
            }
        }

        public void EdgeDel(string v1, string v2)
        {
            if (isDirected)
            {
                foreach (var item in graph)
                {
                    if (item.Key.Equals(v1))
                    {
                        graph[item.Key].Remove(v2);
                    }
                }
            }
            else
            {
                foreach (var item in graph)
                {
                    if (item.Key.Equals(v1))
                    {
                        graph[item.Key].Remove(v2);
                    }
                    if (item.Key.Equals(v2))
                    {
                        graph[item.Key].Remove(v1);
                    }
                }

            }
        }

        public void Show()
        {
            Console.WriteLine("Полученный граф: ");
            foreach (var i in graph)
            {
                Console.Write("{0} -> ", i.Key);
                foreach (var j in i.Value)
                {
                    Console.Write("{0}({1}) ", j.Key, j.Value);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public void Print(string fout)
        {
            string vert;
            using (StreamWriter fileOut = new StreamWriter(fout, false, Encoding.GetEncoding(1251)))
            {
                foreach (var i in graph)
                {
                    vert = i.Key + " ";
                    foreach (var j in i.Value)
                    {
                        vert += (j.Key + "(" + j.Value + ") ");
                    }
                    fileOut.WriteLine(vert);
                }
            }
        }

    }
}
