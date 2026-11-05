using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 18f;
    [SerializeField] private float lifetime = 3f;
    [SerializeField] private float radius = 0.12f;
    [SerializeField] private LayerMask hitLayers = Physics.DefaultRaycastLayers;

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        float distance = speed * Time.deltaTime;

        if (!Physics.SphereCast(transform.position, radius, transform.forward,
            out RaycastHit hit, distance, hitLayers, QueryTriggerInteraction.Ignore))
        {
            transform.position += transform.forward * distance;
            return;
        }

        PillarMovement pillar = hit.collider.GetComponentInParent<PillarMovement>();

        if (pillar)
            pillar.Activate();

        Destroy(gameObject);
    }
}
