using Cysharp.Threading.Tasks;
using UnityEngine;

namespace View.Pill
{
    public class PillEffectView : MonoBehaviour
    {
        private Animator _animator;
        
        public async UniTask PlayAnimation()
        {
            var info = _animator.GetCurrentAnimatorStateInfo(0);
            await UniTask.Delay((int)(info.length * 1000));
            
            Destroy(gameObject);
        }
        
        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }
    }
}