using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class Wall
    {
        public int Length { get; }
        public Orientation.Position Position { get; }
        public bool HasPath { get; }
        public Passage Corridor { get; }
        public Room AttachedTo { get; set; }

        public Wall(int length, Orientation.Position position, bool hasPath, Range passageLength)
        {
            Length = length;
            HasPath = hasPath;
            Position = position;

            if (hasPath)
                Corridor = new Passage(this, Orientation.ReverseDirection(Orientation.PositionToDirection(position)),
                    passageLength);
        }

        public Wall(int length, Orientation.Position position, Passage corridor)
        {
            Length = length;
            HasPath = true;
            Position = position;

            Corridor = corridor;
        }

        public void AttachRoom(Room room)
        {
            AttachedTo = room;
            if (Corridor != null && Corridor.StartWall != this)
                Corridor.AttachEndWall(this);
        }

        public Vector3 GetPassageStart() // Get it out of here!
        {
            var other = Corridor.GetAnotherWall(this);
            if (Corridor.Direction == Orientation.Direction.Horizontal)
            {
                var offset1 = AttachedTo.Offset.y;
                var offset2 = other.AttachedTo.Offset.y;
                var y = (int)(Math.Max(offset1, offset2) + 
                    Math.Min(AttachedTo.Rows + offset1, other.AttachedTo.Rows + offset2)) / 2;
                return new Vector3(0, y - AttachedTo.Offset.y, 0);
            }
            else
            {
                var offset1 = AttachedTo.Offset.x;
                var offset2 = other.AttachedTo.Offset.x;
                var x = (int)(Math.Max(offset1, offset2) + 
                    Math.Min(AttachedTo.Columns + offset1, other.AttachedTo.Columns + offset2)) / 2; // Method
                return new Vector3(x - AttachedTo.Offset.x, 0, 0);
            }
        }
    }
}
