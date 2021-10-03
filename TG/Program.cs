using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TG
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = "input.txt";
            string fout = "output.txt";
            Graph g = new Graph(path);
            Interface.Menu(g, fout);
            //g.Show();

            //g.VertDel("5");
            //g.Show();

            //g.EdgeDel("1", "2");
            //g.Show();

            //g.IsDirected = false;
            //g.EdgeDel("3", "4");
            //g.Show();

            //string fout = "output.txt";
            //g.Print(fout);
        }
    }
}
