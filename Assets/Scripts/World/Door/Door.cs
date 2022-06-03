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

        private AudioSource Audio { get; set; }

        public void FitModel()
        {
            Collider = GetComponent<BoxCollider2D>();
            Audio = GetComponent<AudioSource>();

            Animator.SetBool("IsVertical", DoorModel.Direction == Orientation.Direction.Vertical);
            if (DoorModel.Direction == Orientation.Direction.Horizontal)
            {
                var colliders = GetComponents<BoxCollider2D>();
                colliders[1].enabled = true;
                colliders[2].enabled = false;
                colliders[3].enabled = false;
            }
        }

        private void OnCollisionEnter2D()
        {
            if (!DoorModel.AttachedToRoom.AllEnemiesDead)
                return;

            Collider.isTrigger = true;
            Audio.Play();
            Animator.SetBool("IsClosed", false);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (DoorModel.AttachedToRoom.AllEnemiesDead) 
                return;

            Collider.isTrigger = false;
            Animator.SetBool("IsClosed", true);
            Audio.Play();
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (!collision.CompareTag("Player") || DoorModel.Disabled)
                return;

            DoorModel.Passed = true;

            var anotherDoor = DoorModel.AttachedToPassage.GetAnotherDoor(DoorModel);
            if (!anotherDoor.Passed)
                return;

            Collider.isTrigger = false;

            if (DoorModel.AttachedToRoom.WithBoss)
                EnemySpawner(DoorModel.AttachedToRoom.GetEnemiesOfType(EnemyModel.EnemyType.Boss),
                    DoorModel.AttachedToRoom.Offset);
            else
                EnemySpawner(DoorModel.AttachedToRoom.Enemies, DoorModel.AttachedToRoom.Offset);

            DoorModel.Disabled = true;
            Animator.SetBool("IsClosed", true);
            Audio.Play();
            anotherDoor.Disabled = true;
        }
    }
}
