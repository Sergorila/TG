using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TG
{
    class Program
    {
        static void Main(string[] args)
        {
            string path1 = "Directed.txt";
            string path2 = "DirectedWeighted.txt";
            string path3 = "NoDirected.txt";
            string path4 = "NoDirectedWeighted.txt";
            string path5 = "DirForComp1.txt";
            string path6 = "DirForComp2.txt";
            string path7 = "DirForComp3.txt";
            string path8 = "Floyd.txt";
            string fout = "output.txt";
            string s = AddG(path1, path2, path3, path4, path5, path6, path7, path8);
            Graph g = new Graph(s);
            Interface.Menu(g, fout);
        }

        static string AddG(string s1, string s2, string s3, string s4, string s5, string s6, string s7, string s8)
        {
            Console.WriteLine("Выберите файл:");
            Console.WriteLine("1 - Directed.txt");
            Console.WriteLine("2 - DirectedWeighted.txt");
            Console.WriteLine("3 - NoDirected.txt");
            Console.WriteLine("4 - NoDirectedWeighted.txt");
            Console.WriteLine("5 - DirForComp1.txt");
            Console.WriteLine("6 - DirForComp2.txt");
            Console.WriteLine("7 - DirForComp3.txt");
            Console.WriteLine("8 - WeightedGraph.txt");
            int n = int.Parse(Console.ReadLine());
            switch (n)
            {
                case 1:
                    return s1;
                    break;
                case 2:
                    return s2;
                    break;
                case 3:
                    return s3;
                    break;
                case 4:
                    return s4;
                    break;
                case 5:
                    return s5;
                    break;
                case 6:
                    return s6;
                    break;
                case 7:
                    return s7;
                    break;
                case 8:
                    return s8;
                    break;
                default:
                    Console.WriteLine("Попробуйте ещё рaз");
                    AddG(s1, s2, s3, s4, s5, s6, s7, s8);
                    break;
            }
            return "";
        }
    }
}
