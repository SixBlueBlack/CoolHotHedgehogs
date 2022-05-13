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
        public DoorModel DoorModel { get; set; }

        private void OnTriggerEnter2D(Collider2D hitInfo)
        {
            if (hitInfo.tag == "Player")
            {
                var anotherDoor = DoorModel.AttachedTo.GetAnotherDoor(DoorModel);
                if (!anotherDoor.Open)
                    anotherDoor.Open = true;
                else 
                {
                    // this.GetComponent<BoardManager>().Spa
                }
            }
        }
    }
}
