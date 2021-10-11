using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TG
{
    class Interface
    {
        public static int Menu(Graph g, string fout)
        {
            Console.WriteLine("Выберите действие:");
            Console.WriteLine("1 - посмотреть граф");
            Console.WriteLine("2 - добавить вершину");
            Console.WriteLine("3 - удалить вершину");
            Console.WriteLine("4 - добавить ребро");
            Console.WriteLine("5 - удалить ребро");
            Console.WriteLine("6 - выход");
            int n = int.Parse(Console.ReadLine());
            if (n == 1)
            {
                Console.WriteLine("Список смежности графа:");
                g.Show();
                Console.WriteLine();
                Menu(g, fout);
            }
            if (n == 2)
            {
                Console.Write("Введите вершину которую хотите добавить: ");
                string v = Console.ReadLine();
                Console.WriteLine();
                g.VertAdd(v);
                g.Show();
                Console.WriteLine();
                Menu(g, fout);
            }
            if (n == 3)
            {
                Console.Write("Введите вершину которую хотите удалить: ");
                string v = Console.ReadLine();
                Console.WriteLine();
                g.VertDel(v);
                g.Show();
                Console.WriteLine();
                Menu(g, fout);
            }
            if (n == 4)
            {
                Console.WriteLine("Введите ребро которое хотите добавить:");
                Console.Write("Начальная вершина: ");
                string v1 = Console.ReadLine();
                Console.Write("Конечная вершина: ");
                string v2 = Console.ReadLine();
                if (g.IsWeighted)
                {
                    Console.Write("Вес ребра: ");
                    int weight = int.Parse(Console.ReadLine());
                    g.EdgeAdd(v1, v2, weight);
                }
                else
                    g.EdgeAdd(v1, v2);
                Console.WriteLine();
                g.Show();
                Console.WriteLine();
                Menu(g, fout);
            }
            if (n == 5)
            {
                Console.WriteLine("Введите ребро которое хотите удалить:");
                Console.Write("Начальная вершина: ");
                string v1 = Console.ReadLine();
                Console.Write("Конечная вершина: ");
                string v2 = Console.ReadLine();
                Console.WriteLine();
                g.EdgeDel(v1, v2);
                g.Show();
                Console.WriteLine();
                Menu(g, fout);
            }
            if (n == 6)
            {
                Console.WriteLine("Работа завершна, граф записан в файл");
                g.Print(fout);
                return 0;
            }

            if (n > 6 || n < 1)
            {
                Console.WriteLine("Попробуйте заново и введите данные корректно");
                Menu(g, fout);
            }

            return 0;
        }

    }
}
