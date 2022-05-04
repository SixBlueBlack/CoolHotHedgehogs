using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Completed
{
    public class Passage
    {
        public Orientation.Direction Direction { get; }

        public OuterWall StartWall { get; }
        public OuterWall EndWall { get; set; }

        public int Length { get; }

        public Passage(OuterWall startWall, Orientation.Direction direction, Range passageLength)
        {
            StartWall = startWall;
            Direction = direction;
            Length = passageLength.Random;
        }

        public OuterWall GetAnotherWall(OuterWall wall)
        {
            if (wall == StartWall)
                return EndWall;
            return StartWall;
        }
    }
}
