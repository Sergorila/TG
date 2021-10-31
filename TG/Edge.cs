using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TG
{
    class Edge
    {
        public string v1, v2;

        public int weight;

        public Edge(string v1, string v2, int weight)
        {
            this.v1 = v1;
            this.v2 = v2;
            this.weight = weight;
        }
    }
}
