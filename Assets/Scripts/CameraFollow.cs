using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _offset = new Vector3(0f, 3f, -6f);
    [SerializeField] private float _followSpeed = 8f;
    [SerializeField] private float _lookHeight = 1f;

    private void LateUpdate()
    {
        if (!_target)
            return;

        Vector3 desiredPosition = _target.position + _target.rotation * _offset;
        float blend = 1f - Mathf.Exp(-_followSpeed * Time.deltaTime);

        transform.position = Vector3.Lerp(
            transform.position,
            desiredPosition,
            blend
        );

        transform.LookAt(_target.position + Vector3.up * _lookHeight);
    }
}
