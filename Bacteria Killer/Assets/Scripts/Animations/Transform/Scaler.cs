using DG.Tweening;
using UnityEngine;

namespace Animations.Transform
{
    public class Scaler : MonoBehaviour
    {
        [SerializeField] private Vector2 _targetScale;
        [SerializeField] private float _duration;

        private void Awake()
        {
            Scale();
        }

        private void Scale()
        {
            transform.DOScale(_targetScale, _duration)
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.InOutSine);
        }
    }
}