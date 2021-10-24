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
            string path5 = "DirForComp.txt";
            string fout = "output.txt";
            string s = AddG(path1, path2, path3, path4, path5);
            Graph g = new Graph(s);
            Interface.Menu(g, fout);
        }

        static string AddG(string s1, string s2, string s3, string s4, string s5)
        {
            Console.WriteLine("Выберите файл:");
            Console.WriteLine("1 - Directed.txt");
            Console.WriteLine("2 - DirectedWeighted.txt");
            Console.WriteLine("3 - NoDirected.txt");
            Console.WriteLine("4 - NoDirectedWeighted.txt");
            Console.WriteLine("5 - DirForComp.txt");
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
                default:
                    Console.WriteLine("Попробуйте ещё рaз");
                    AddG(s1, s2, s3, s4, s5);
                    break;
            }
            return "";
        }
    }
}
