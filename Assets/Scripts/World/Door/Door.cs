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

        private void Start()
        {
            Collider = GetComponent<BoxCollider2D>();
            Animator.SetBool("IsVertical", DoorModel.IsVertical);
            Audio = GetComponent<AudioSource>();
        }

        private void OnCollisionEnter2D()
        {
            if (!DoorModel.AttachedToRoom.AllEnemiesDead) return;
            Collider.isTrigger = true;
            Animator.SetBool("IsClosed", false);
            Audio.Play();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (DoorModel.AttachedToRoom.AllEnemiesDead) return;
            Collider.isTrigger = false;
            Animator.SetBool("IsClosed", true);
            Audio.Play();
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (!collision.CompareTag("Player") || DoorModel.Disabled) return;
            DoorModel.Passed = true;
            Collider.isTrigger = false;

            var anotherDoor = DoorModel.AttachedToPassage.GetAnotherDoor(DoorModel);
            if (!anotherDoor.Passed) return;

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
