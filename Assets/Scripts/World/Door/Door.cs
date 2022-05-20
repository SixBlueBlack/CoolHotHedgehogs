using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class Door : MonoBehaviour
    {
        public Action<IEnumerable<EnemyModel>, Vector3> EnemySpawner;
        public DoorModel DoorModel { get; set; }

        private void OnTriggerExit2D(Collider2D hitInfo)
        {
            if (hitInfo.CompareTag("Player") && !DoorModel.Disabled)
            {
                DoorModel.Open = true;
                var anotherDoor = DoorModel.AttachedToPassage.GetAnotherDoor(DoorModel);
                if (anotherDoor.Open)
                {
                    EnemySpawner(DoorModel.AttachedToRoom.Enemies, DoorModel.AttachedToRoom.Offset);
                    DoorModel.Disabled = true;
                    anotherDoor.Disabled = true;
                }
            }
        }
    }
}
