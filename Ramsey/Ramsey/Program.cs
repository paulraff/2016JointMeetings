using System;
using System.IO;

namespace Ramsey
{
    public class Program
    {
        static void Main(string[] args)
        {
            var rand = new Random();
            var sw = new StreamWriter(@"C:\tmp\out-common.txt");
            for (var i=0; i<1000000; i++)
            {
                if (i % 10000 == 0)
                    System.Console.WriteLine(i.ToString());
                var graph = new CompleteGraph(5);
                graph.Randomize(rand);
                sw.WriteLine(string.Format("{0}\t{1}\t{2}", i, graph.GetHashCode(), graph.HasCompleteSubgraphOfSize3()));
            }
            sw.Close();
        }
    }
}
