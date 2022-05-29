using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class Door : MonoBehaviour
    {
        public Action<IEnumerable<EnemyModel>, Vector3> EnemySpawner;
        public DoorModel DoorModel { get; set; }
        private BoxCollider2D Collider;
        public Animator Animator;

        private void Start()
        {
            Collider = this.GetComponent<BoxCollider2D>();
            Animator.SetBool("IsVertical", DoorModel.IsVertical);
        }

        private void OnCollisionEnter2D()
        {
            if (!DoorModel.AttachedToRoom.AllEnemiesDead) return;
            Collider.isTrigger = true;
            Animator.SetBool("IsClosed", false);

        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (DoorModel.AttachedToRoom.AllEnemiesDead) return;
            Collider.isTrigger = false;
            Animator.SetBool("IsClosed", true);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (!collision.CompareTag("Player") || DoorModel.Disabled) return;
            DoorModel.Passed = true;
            Collider.isTrigger = false;

            var anotherDoor = DoorModel.AttachedToPassage.GetAnotherDoor(DoorModel);
            if (!anotherDoor.Passed) return;
            EnemySpawner(DoorModel.AttachedToRoom.Enemies, DoorModel.AttachedToRoom.Offset);
            DoorModel.Disabled = true;
            Animator.SetBool("IsClosed", true);
            anotherDoor.Disabled = true;
        }
    }
}
