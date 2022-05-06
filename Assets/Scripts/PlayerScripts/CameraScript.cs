using UnityEngine;

namespace Assets.Scripts
{
    public class CameraScript : MonoBehaviour
    {

        private Transform player;
        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        // Update is called once per frame
        void Update()
        {
            MoveCamera();
        }

        private void MoveCamera()
        {
            var temp = transform.position;
            temp.x = player.position.x;
            temp.y = player.position.y;
            transform.position = temp;
        }
    }
}