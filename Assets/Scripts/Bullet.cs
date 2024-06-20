using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    private Rigidbody rb;
    private int damage;
    private bool isCriticalHit;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void FireBullet(Vector3 direction)
    {
        rb.AddForce(direction * speed, ForceMode.Impulse);
        Destroy(gameObject, 5f);
    }

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamagable damagable))
        {
            if(damagable.GetCurrentHealth() > 0)
            {
                damagable.TakeDamage(damage);
                DamagePopUp.Create(other.transform.position + new Vector3(Random.Range(-1f, 1f), 0f), damage, isCriticalHit);
                Instantiate(GameAssets.Instance.bloodSplatFXPrefab, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }

        }

        if (other.gameObject.CompareTag("Static"))
        {
            Destroy(gameObject);
        }
    }

    public void SetDamage(int damage) => this.damage = damage;
    public void SetIsCriticalHit(bool isCritical) => this.isCriticalHit = isCritical;
}
