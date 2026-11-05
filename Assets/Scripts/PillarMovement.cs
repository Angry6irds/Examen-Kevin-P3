using UnityEngine;

public class PillarMovement : MonoBehaviour
{
    [SerializeField] private Transform destination;
    [SerializeField] private float speed = 3f;

    private void Awake()
    {
        enabled = false;
    }

    private void Update()
    {
        if (!destination)
        {
            enabled = false;
            return;
        }

        transform.position = Vector3.MoveTowards(
            transform.position, destination.position, speed * Time.deltaTime);

        if (transform.position == destination.position)
            enabled = false;
    }

    [ContextMenu("Activate Pillar")]
    public void Activate()
    {
        enabled = destination;
    }
}
