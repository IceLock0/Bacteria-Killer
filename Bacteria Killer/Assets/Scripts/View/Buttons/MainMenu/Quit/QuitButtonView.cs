using UnityEngine;
using View.Buttons;

namespace View.MainMenuButtons.Quit
{
    public class QuitButtonView : BaseButton
    {
        protected override void OnClick()
        {
            PlayAudio();
            Application.Quit();
        }
    }
}