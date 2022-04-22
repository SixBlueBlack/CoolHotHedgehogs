using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Completed
{
    class RimmsGeneration
    {
        private static Vertex FindMinWeightVertex(HashSet<Vertex> vertices)
        {
            var minWeight = vertices.Min(vertice => vertice.Weight);
            return vertices.Where(vertice => vertice.Weight == minWeight).First();
        }

        public static Dictionary<(int, int), Vertex>  InitSquareGraph(int rows, int columns)
        {
            var notAdded = new HashSet<Vertex>();
            var vertices = new Dictionary<(int, int), Vertex>();
            for (var i = 0; i < rows; i++)
                for (var j = 0; j < columns; j++)
                {
                    var up = new Edge();
                    var right = new Edge();
                    var v = new Vertex(
                        i == rows - 1 ? null : up,
                        i == 0 ? null : vertices[(i - 1, j)].UpperEdge,
                        j == columns - 1 ? null : right,
                        j == 0 ? null : vertices[(i, j - 1)].RightEdge);
                    up.Vertex1 = v;
                    right.Vertex1 = v;
                    if (i != 0)
                        vertices[(i - 1, j)].UpperEdge.Vertex2 = v;
                    if (j != 0)
                        vertices[(i, j - 1)].RightEdge.Vertex2 = v;

                    vertices[(i, j)] = v;
                    notAdded.Add(v);
                }
            vertices[(0, 0)].Weight = 0;

            return vertices;
        }

        public static void ConnectRandomSpanningTree(Vertex[] vertices)
        {
            var notAdded = new HashSet<Vertex>(vertices);
            while (notAdded.Count != 0)
            {
                var vertex = FindMinWeightVertex(notAdded);
                notAdded.Remove(vertex);
                if (vertex.CheapestConnection != null)
                    vertex.CheapestConnection.Actual = true;

                foreach (var edge in vertex.Edges)
                {
                    if (edge == null)
                        continue;
                    var vertex2 = edge.GetIncident(vertex);
                    if (notAdded.Contains(vertex2) && edge.Cost < vertex2.Weight)
                    {
                        vertex2.Weight = edge.Cost;
                        vertex2.CheapestConnection = edge;
                    }
                }
            }
        }
    }
}
