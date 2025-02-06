using System;

namespace View.Buttons.Gameplay
{
    public class HomeButtonView : BaseButton
    {
        public event Action Clicked;

        protected override void OnClick()
        {
            PlayAudio();
            Clicked?.Invoke();
        }
    }
}