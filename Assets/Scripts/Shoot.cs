using UnityEngine;
using UnityEngine.InputSystem;

public class Shoot : MonoBehaviour
{
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private Projectile _projectile;

    private void Update()
    {
        if (Keyboard.current != null && Keyboard.current.fKey.wasPressedThisFrame)
            Fire();
    }

    public void Fire()
    {
        if (!_shootPoint || !_projectile)
            return;

        Instantiate(_projectile, _shootPoint.position, _shootPoint.rotation);
    }
}
