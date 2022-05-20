using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class DoorModel
    {
        public bool Open { get; set; } = false;
        public bool Disabled { get; set; } = false;
        public Passage AttachedToPassage { get; set; }
        public Room AttachedToRoom { get; set; }

        public DoorModel(Passage attachedToPassage, Room attachedToRoom)
        {
            AttachedToPassage = attachedToPassage;
            AttachedToRoom = attachedToRoom;
        }
    }
}
