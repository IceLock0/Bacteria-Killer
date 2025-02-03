using UnityEngine;
using UnityEngine.UI;
using Utils.Extensions;

namespace View.MainMenuButtons
{
    public class PlayButtonView : MonoBehaviour
    {
        [SerializeField] private Button _playButton;

        private void StartGame()
        {
            Debug.Log($"Game started");
        }
        
        private void OnEnable()
        {
            _playButton.AddListener(StartGame);
        }

        private void OnDisable()
        {
            _playButton.RemoveListener(StartGame);
        }
    }
}