using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class Decoration
    {
        public Vector3 Coordinate { get; }
        public DecorationType Type { get; }
        public bool AbovePlayer { get; }
        public enum DecorationType
        {
            Plant,
            VendingMachine,
            TennisTable,
            Other
        }

        public Decoration(Vector3 coordinate, DecorationType type, bool abovePlayer=false)
        {
            Coordinate = coordinate;
            Type = type;
            AbovePlayer = abovePlayer;
        }
    }
}
