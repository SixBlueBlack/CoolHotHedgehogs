using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    public class DoorModel
    {
        public bool Open { get; set; } = false;
        public Passage AttachedTo { get; set; }

        public DoorModel(Passage attachedTo)
        {
            AttachedTo = attachedTo;
        }
    }
}
