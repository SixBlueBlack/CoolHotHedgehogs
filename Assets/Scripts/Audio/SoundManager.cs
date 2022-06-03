using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    class SoundManager : MonoBehaviour
    {
        public Slider volumeSlider;
        private AudioSource TestAudio { get; set; }

        private void Start()
        {
            TestAudio = GetComponent<AudioSource>();
        }

        public void ChangeVolume()
        {
            AudioListener.volume = volumeSlider.value;

            if (TestAudio.time >= 0.1)
                TestAudio.Play();
            else if (!TestAudio.isPlaying)
                TestAudio.Play();
        }
    }
}
