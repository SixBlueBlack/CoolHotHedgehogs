using UnityEngine;

namespace Assets.Scripts
{
    class BackgroudAudio : MonoBehaviour
    {
        public Player Player;

        private void Update()
        {
            if (Player.IsDead)
                Destroy(gameObject);
        }
    }
}
