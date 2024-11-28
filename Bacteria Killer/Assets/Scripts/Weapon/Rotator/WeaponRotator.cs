using Entities.Player;
using UnityEngine;
using Zenject;

public class WeaponRotator : MonoBehaviour
{
    [SerializeField] private bool _isFlipped;

    private PlayerClosestEnemyDetector _enemyDetector;
    
    private Vector2 _target;
    
    [Inject]
    public void Initialize(PlayerClosestEnemyDetector enemyDetector)
    {
        _enemyDetector = enemyDetector;
    }
    
    private void Update()
    {
        var closestEnemy = _enemyDetector.ClosestEnemy;

        if (closestEnemy == null)
            return;
        
        _target = closestEnemy.transform.position;

        var direction = _target - (Vector2)transform.position;

        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (_isFlipped)
            angle -= 180;
        
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
