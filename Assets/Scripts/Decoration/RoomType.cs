using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RandomGenerator = UnityEngine.Random;

namespace Assets.Scripts
{
    public class RoomType
    {
        public List<Decoration> Decorations { get; set; } = new List<Decoration>();

        public enum TypeName
        {
            Tennis,
            Classroom
        }

        public static TypeName GetRandomRoomType()
        {
            var values = Enum.GetValues(typeof(TypeName));
            return (TypeName)values.GetValue(RandomGenerator.Range(0, values.Length));
        }
    }
}
