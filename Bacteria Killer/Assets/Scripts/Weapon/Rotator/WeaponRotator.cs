using UnityEngine;

public class WeaponRotator : MonoBehaviour
{
    [SerializeField] private bool _isFlipped;
    
    void Update()
    {
        var direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (_isFlipped)
            angle -= 180;
        
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
