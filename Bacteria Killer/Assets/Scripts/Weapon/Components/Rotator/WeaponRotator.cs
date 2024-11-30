using Entities.Player;
using UnityEngine;

public class WeaponRotator
{
    private readonly PlayerClosestEnemyDetector _enemyDetector;
    
    private readonly bool _isFlipped;
    private readonly Transform _weaponTransform;
    
    private Vector2 _target;
    
    public WeaponRotator(PlayerClosestEnemyDetector enemyDetector, bool isFlipped, Transform weaponTransform)
    {
        _enemyDetector = enemyDetector;
        
        _isFlipped = isFlipped;
        _weaponTransform = weaponTransform;
    }
    
    public void Rotate()
    {
        var closestEnemy = _enemyDetector.ClosestEnemy;

        if (closestEnemy == null)
            return;
        
        _target = closestEnemy.transform.position;

        var direction = _target - (Vector2)_weaponTransform.position;

        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (_isFlipped)
            angle -= 180;
        
        _weaponTransform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
