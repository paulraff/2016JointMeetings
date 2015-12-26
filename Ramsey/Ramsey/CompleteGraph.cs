using System;
using System.Collections;

namespace Ramsey
{
    public class CompleteGraph
    {
        private int nodes;
        private BitArray edges;

        public CompleteGraph(int n)
        {
            if (n <= 0)
                throw new ArgumentOutOfRangeException("[CompleteGraph Constructor] Input size of CompleteGraph must be positive");
            this.nodes = n;
            this.edges = new BitArray(n * (n - 1) / 2);
        }

        /// <summary>
        /// Given two nodes, colors the edge between them. 
        /// Only colors - if the edge is already colored, then it's a no-op.
        /// </summary>
        /// <param name="e"></param>
        public void ColorEdge(int v1, int v2)
        {
            if (v1 < 0 || v2 < 0 || v1 == v2)
                throw new ArgumentOutOfRangeException("[CompleteGraph.ColorEdge] Input vertices must be positive, and not equal to each other.");
            if (v1 > v2)
                Flip(ref v1, ref v2);
            var index = GetEdgeFromNodes(v1, v2);
            edges.Set(index, true);
        }

        public void UncolorEdge(int v1, int v2)
        {
            if (v1 < 0 || v2 < 0 || v1 == v2)
                throw new ArgumentOutOfRangeException("[CompleteGraph.UncolorEdge] Input vertices must be positive, and not equal to each other.");
            if (v1 > v2)
                Flip(ref v1, ref v2);
            var index = GetEdgeFromNodes(v1, v2);
            edges.Set(index, false);
        }

        /// <summary>
        /// Starts from scratch, and randomizes all edges.
        /// </summary>
        public void Randomize(Random rand)
        {
            
            var len = edges.Count;
            for (var i=0; i<len; i++)
            {
                var flip = rand.NextDouble();
                if (flip < 0.5)
                    edges[i] = true;
                else
                    edges[i] = false;
            }
        }

        /// <summary>
        /// Only checks for subgraph of size 3.
        /// </summary>
        /// <returns></returns>
        public bool HasCompleteSubgraphOfSize3()
        {
            if (nodes < 3)
                return false;
            for (var i=0; i < nodes; i++)
            {
                for (var j=1; j < nodes; j++)
                {
                    for (var k=2; k < nodes; k++)
                    {
                        // We need to ensure that i < j < k
                        if (i >= j || j >= k)
                            continue;
                        var edge1 = GetEdgeFromNodes(i, j);
                        var edge2 = GetEdgeFromNodes(j, k);
                        var edge3 = GetEdgeFromNodes(i, k);
                        var isCompleteTrue = edges[edge1] && edges[edge2] && edges[edge3];
                        if (isCompleteTrue)
                            return true;
                        var isCompleteFalse = !edges[edge1] && !edges[edge2] && !edges[edge3];
                        if (isCompleteFalse)
                            return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// We assume inputs have been validated.
        /// Given two edges, returns the correct index from the bit array.
        /// Here's the enumeration example for 5 nodes:
        /// 00 01 02 03 04
        /// 05 06 07 08
        /// 09 10 11
        /// 12 13
        /// 14
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        private int GetEdgeFromNodes(int v1, int v2)
        {
            var step = this.nodes-1;
            var result = 0;
            for (var i = 0; i < v1; i++)
            {
                result += step;
                step--;
            }
            result += (v2 - v1) - 1;
            return result;
        }

        private static void Flip(ref int a, ref int b)
        {
            int temp = b;
            b = a;
            a = temp;            
        }
    }
}
