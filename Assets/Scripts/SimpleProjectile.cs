using UnityEngine;

public class SimpleProjectile : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float velocity;
    [SerializeField] private float damage;
    [SerializeField] private float selfdestructTime = 10;

    [SerializeField] private GameObject ElectricHitEffect;

    private void Start()
    {
        _rigidbody.velocity = transform.forward * velocity;
        Destroy(gameObject, selfdestructTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamagable damagable))
        {
            if (ElectricHitEffect != null)
            {
                Instantiate(ElectricHitEffect, transform.position, transform.rotation);
            }
            damagable.TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
