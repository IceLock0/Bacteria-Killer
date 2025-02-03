using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace View.Sound
{
    public class SoundVolumeView : MonoBehaviour
    {
        [SerializeField] private AudioMixer _mixer;
        [SerializeField] private Slider _slider;

        private void ChangeSoundVolume(float value)
        {
            _mixer.SetFloat("Sound", value);
        }
        
        private void OnEnable()
        {
            _slider.onValueChanged.AddListener(ChangeSoundVolume);
        }
        
        private void OnDisable()
        {
            _slider.onValueChanged.RemoveListener(ChangeSoundVolume);
        }
    }
}