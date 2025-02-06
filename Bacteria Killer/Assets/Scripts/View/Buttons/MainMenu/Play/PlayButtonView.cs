using System;
using View.Buttons;

namespace View.MainMenuButtons
{
    public class PlayButtonView : BaseButton
    {
        public event Action Clicked;

        protected override void OnClick()
        {
            PlayAudio();
            Clicked?.Invoke();
        }
    }
}