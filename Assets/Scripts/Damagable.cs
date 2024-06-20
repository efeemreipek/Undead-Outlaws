using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Damagable : MonoBehaviour, IDamagable
{
    [SerializeField] protected int maxHealth;
    [SerializeField] protected int currentHealth;

    protected bool isDamageTaken = false;

    private void Start()
    {
        SetCurrentHealthToMax();
    }

    public void TakeDamage(int damage)
    {
        currentHealth = Mathf.Max(currentHealth - damage, 0);
        isDamageTaken = true;
    }

    protected void SetCurrentHealthToMax() => currentHealth = maxHealth;

    protected bool CheckIfDead() => currentHealth == 0;

    protected void DestroyObject() => Destroy(gameObject);

    protected bool CheckIfDamageTaken()
    {
        bool wasDamageTaken = isDamageTaken;
        isDamageTaken = false;
        return wasDamageTaken;
    }

    public int GetCurrentHealth() => currentHealth;
}
