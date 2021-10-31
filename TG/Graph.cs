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
        public bool isWeighted = true;

        Dictionary<string, bool> nov = new Dictionary<string, bool>();
        private int[,] array; //матрица смежности
        public int this[int i, int j] //индексатор для обращения к матрице смежности
        {
            get
            {
                return array[i, j];
            }
            set
            {
                array[i, j] = value;
            }
        }
        public int Size //свойство для получения размерности матрицы смежности
        {
            get
            {
                return array.GetLength(0);
            }
        }
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

        public bool IsWeighted
        {
            get
            {
                return isWeighted;
            }
            set
            {
                isWeighted = value;
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
                isDirected = bool.Parse(fileIn.ReadLine());
                isWeighted = bool.Parse(fileIn.ReadLine());
                Dictionary<string, int> verts = new Dictionary<string, int>();
                while ((temp = fileIn.ReadLine()) != null)
                {
                    string[] v = temp.Split(' ');
                    Dictionary<string, int> VertsNext = new Dictionary<string, int>();
                    for (int i = 1; i < v.Length; i++)
                    {
                        if (IsWeighted)
                        {
                            vert = v[i].Split(':');
                            VertsNext.Add(vert[0], int.Parse(vert[1]));
                        }
                        else
                            VertsNext.Add(v[i], 0);
                    }

                    graph.Add(v[0], VertsNext);
                }
            }
        }

        public void NovSet() //метод помечает все вершины графа как непросмотреные
        {
            foreach (var item in graph)
            {
                nov[item.Key] = true;
            }
        }

        public void Dfs(string v)
        {
            Console.WriteLine("{0} ", v);
            nov[v] = false;

            foreach (var u in graph.Keys)
            {
                //если вершины v и u смежные, к тому же вершина u не просмотрена,
                if (graph[v].ContainsKey(u) && nov[u])
                {
                    Dfs(u); // то рекурсивно просматриваем вершину
                }
            }
        }

        public long[] ShortPaths(string v)
        {
            NovSet();
            nov[v] = false;
            int[,] c = new int[graph.Keys.Count, graph.Keys.Count];
            Dictionary<string, int> VERSINI = new Dictionary<string, int>();
            int i = 0;
            foreach(var u in graph.Keys)
            {
                VERSINI.Add(u, i);
                i++;
            }

            foreach (var u in graph.Keys)
            {
                foreach(var j in VERSINI.Keys)
                {
                    if (!graph[u].ContainsKey(j) || u == j)
                        c[VERSINI[u], VERSINI[j]] = int.MaxValue;
                    else
                        c[VERSINI[u], VERSINI[j]] = 1;
                }
            }
            long[] d = new long[graph.Keys.Count];

            foreach (var u in graph.Keys)
            {
                if (u != v)
                {
                    d[VERSINI[u]] = c[VERSINI[v], VERSINI[u]];
                }
            }

            for (int j = 0; j < graph.Keys.Count - 1; j++)
            {
                long min = int.MaxValue;
                string w = v;
                foreach(var u in graph.Keys)
                {
                    if (nov[u] && min > d[VERSINI[u]])
                    {
                        min = d[VERSINI[u]];
                        w = u;
                    }
                }
                nov[w] = false;
                foreach(var u in graph.Keys)
                {
                    long dist = d[VERSINI[w]] + c[VERSINI[w], VERSINI[u]];
                    if (nov[u] && d[VERSINI[u]] > dist)
                    {
                        d[VERSINI[u]] = dist;
                    }
                }
            }
            return d;   
                
        }

        public void Center()
        {
            long[][] AllDist = new long[graph.Keys.Count][];
            int l = 0;
            Dictionary<string, int> VERSINI = new Dictionary<string, int>();
            foreach (var u in graph.Keys)
            {
                VERSINI.Add(u, l);
                l++;
            }

            foreach (var u in VERSINI.Keys)
            {
                AllDist[VERSINI[u]] = new long[graph.Keys.Count];
                long[] dist = ShortPaths(u);
                for (int i = 0; i < dist.Length; i++)
                    AllDist[VERSINI[u]][i] = dist[i];
            }

            long[] MaX = new long[graph.Keys.Count];

            for (int j = 0; j < graph.Keys.Count; j++)
            {
                long MMaX = -1;
                for (int i = 0; i < graph.Keys.Count; i++)
                {
                    if (AllDist[i][j] != int.MaxValue && AllDist[i][j] != 0 && AllDist[i][j] > MMaX)
                        MMaX = AllDist[i][j];
                }
                MaX[j] = MMaX;   
            }


            long min = int.MaxValue;
            for (int i = 0; i < MaX.Length; i++)
            {
                if (MaX[i] < min && MaX[i] != -1)
                {
                    min = MaX[i];
                }
            }

            Console.WriteLine("Центр графа: ");
            foreach(var item in graph.Keys)
            {
                if (MaX[VERSINI[item]] == min)
                {
                    Console.Write(item + " ");
                }
            }
            Console.WriteLine();

        }

        public bool IsStrongConnect()
        {
            bool f = true;
            string v = "";
            foreach (var item in graph.Keys)
            {
                v = item;
                break;
            }
            NovSet();
            Dfs(v);
            foreach (var elem in nov)
            {
                if (elem.Value == true)
                {
                    f = false;
                    break;
                }
            }
            return f;
        }

        public void VertAdd(string v)
        {
            if (graph.ContainsKey(v))
            {
                Console.WriteLine("Вершина уже существует");
                Console.WriteLine();
            }
            else
                graph.Add(v, new Dictionary<string, int>());
        }

        public void EdgeAdd(string v1, string v2, int weight)
        {
            if (graph.ContainsKey(v1) && graph.ContainsKey(v2))
            {
                bool exst = false;
                foreach (var item in graph[v1])
                {
                    if (item.Key == v2)
                    {
                        exst = true;
                        break;
                    }
                }
                if (exst == true)
                {
                    Console.WriteLine("Ребро уже существует");
                    Console.WriteLine();
                }
                else
                {
                    if (weight >= 0)
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
                }
            }
            else
            {
                Console.WriteLine("Неправильно введены вершины");
                Console.WriteLine();
            }
        }

        public void EdgeAdd(string v1, string v2)
        {
            
            if (graph.ContainsKey(v1) && graph.ContainsKey(v2))
            {
                bool exst = false;
                foreach (var item in graph[v1])
                {
                    if (item.Key == v2)
                    {
                        exst = true;
                        break;
                    }
                }
                if (exst == true)
                {
                    Console.WriteLine("Ребро уже существует");
                    Console.WriteLine();
                }
                else
                {
                    if (isDirected)
                    {
                        graph[v1].Add(v2, 0);
                    }
                    else
                    {
                        graph[v1].Add(v2, 0);
                        graph[v2].Add(v1, 0);
                    }
                }
                
            }
            else
            {
                Console.WriteLine("Неправильно введены вершины");
                Console.WriteLine();
            }
            
        }

        public void VertDel(string v)
        {
            if (graph.ContainsKey(v))
            {
                graph.Remove(v);
                foreach (var item in graph)
                {
                    graph[item.Key].Remove(v);
                }
            }
            else
            {
                Console.WriteLine("Такой вершины нет");
                Console.WriteLine();
            }
        }

        public void HalfDegree(string v)
        {
            if (graph.ContainsKey(v))
            {
                int halfdeg = graph[v].Count;
                Console.Write("Вершины, полустепень исхода которых больше: ");
                foreach (var item in graph)
                {
                    if (item.Value.Count > halfdeg && item.Key != v)
                    {
                        Console.Write("{0}, ", item.Key);
                    }
                }
            }
            else
            {
                Console.WriteLine("Такой вершины нет");
            }
            
            Console.WriteLine();
        }

        public void VertNotAdj(string v)
        {
            if (graph.ContainsKey(v))
            {
                Console.Write("Вершины графа, не смежные с {0}: ", v);
                foreach (var item in graph)
                {
                    if (!item.Value.ContainsKey(v) && item.Key != v)
                    {
                        Console.Write("{0}, ", item.Key);
                    }
                }
            }
            else
            {
                Console.WriteLine("Такой вершины нет");
            }
            
            Console.WriteLine();
        }
        public void EdgeDel(string v1, string v2)
        {
            if (graph.ContainsKey(v1) && graph.ContainsKey(v2))
            {
                bool exst = false;
                foreach (var item in graph[v1])
                {
                    if (item.Key == v2)
                    {
                        exst = true;
                        break;
                    }
                }
                if (exst == false)
                {
                    Console.WriteLine("Такого ребра нет");
                    Console.WriteLine();
                }
                else
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
                
            }
            else
            {
                Console.WriteLine("Неправильно введены вершины");
                Console.WriteLine();
            }
            
        }

        public Graph OrGraphComplement(Graph g)
        {
            Graph comp = new Graph();
            comp.isDirected = g.isDirected;
            comp.isWeighted = g.isWeighted;

            foreach (var vert in g.graph)
            {
                comp.VertAdd(vert.Key);
            }

            foreach (var vert in g.graph)
            {
                foreach (var adjv in g.graph.Keys)
                {
                    if (!vert.Value.ContainsKey(adjv) && vert.Key != adjv)
                        {
                            comp.EdgeAdd(vert.Key, adjv);
                        }
                    
                }
            }

            return comp;
        }

        public void Show()
        {
            Console.WriteLine("Полученный граф: ");
            foreach (var i in graph)
            {
                Console.Write("{0} -> ", i.Key);
                foreach (var j in i.Value)
                {
                    if (IsWeighted)
                    {
                        Console.Write("{0}({1}) ", j.Key, j.Value);
                    }
                    else
                        Console.Write("{0} ", j.Key);
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
