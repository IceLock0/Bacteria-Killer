using System;
using UnityEngine;

namespace Components.Collectable
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class CollectableComponent : MonoBehaviour, ICollectable
    {
        public event Action<Collider2D> Collected;

        private void Awake()
        {
            var collider = GetComponent<BoxCollider2D>();
            collider.isTrigger = true;
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            Collected?.Invoke(collider);
        }
    }
}