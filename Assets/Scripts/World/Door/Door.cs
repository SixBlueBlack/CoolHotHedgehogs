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
        private BoxCollider2D Collider;

        private void Start()
        {
            Collider = this.GetComponent<BoxCollider2D>();
        }

        private void OnCollisionEnter2D()
        {
            if (DoorModel.AttachedToRoom.AllEnemiesDead)
                Collider.isTrigger = true;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!DoorModel.AttachedToRoom.AllEnemiesDead)
                Collider.isTrigger = false;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player") && !DoorModel.Disabled)
            {
                DoorModel.Passed = true;
                Collider.isTrigger = false;

                var anotherDoor = DoorModel.AttachedToPassage.GetAnotherDoor(DoorModel);
                if (anotherDoor.Passed)
                {
                    EnemySpawner(DoorModel.AttachedToRoom.Enemies, DoorModel.AttachedToRoom.Offset);
                    DoorModel.Disabled = true;
                    anotherDoor.Disabled = true;
                }
            }
        }
    }
}
