using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Random = UnityEngine.Random;

namespace Assets.Scripts
{
    public class Vertex
    {
        public int X { get; }
        public int Y { get; }

        public float Weight { get; set; } = int.MaxValue;

        public Edge UpperEdge { get; set; }
        public Edge BottomEdge { get; set; }
        public Edge RightEdge { get; set; }
        public Edge LeftEdge { get; set; }
        public Edge[] Edges
        {
            get { return new Edge[4] { UpperEdge, BottomEdge, RightEdge, LeftEdge }; }
        }

        public Edge CheapestConnection { get; set; } = null;

        public Vertex(Edge upperEdge, Edge bottomEdge, Edge rightEdge, Edge leftEdge)
        {
            UpperEdge = upperEdge;
            BottomEdge = bottomEdge;
            RightEdge = rightEdge;
            LeftEdge = leftEdge;
        }
    }

    public class Edge
    {
        public float Cost { get; set; } = Random.value;
        public bool Actual { get; set; } = false;
        public Vertex Vertex1 { get; set; }
        public Vertex Vertex2 { get; set; }

        public Vertex GetIncident(Vertex v)
        {
            return v == Vertex1 ? Vertex2 : Vertex1;
        }
    }
}
