using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Completed
{
    public class OuterWall
    {
        public enum Orientations
        {
            Upper,
            Bottom,
            Right,
            Left
        }
        public int Length { get; }
        public Orientations Orientation { get; }
        public bool HasPath { get; }

        public OuterWall(int length, Orientations orientation, bool hasPath)
        {
            Length = length;
            HasPath = hasPath;
            Orientation = orientation;
        }
    }
}
