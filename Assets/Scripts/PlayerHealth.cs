using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;

    private bool bloodOverlayCoroutineRunning = false;
    private bool isPlayerDamaged = false;

    public static Action OnPlayerDead;

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();

        GameManager.OnGameRestarted += GameManager_OnGameRestarted;
    }

    public void TakeDamage(int amount)
    {
        currentHealth = Mathf.Max(currentHealth - amount, 0);
        CheckIfPlayerIsDamaged();
        AudioManager.Instance.PlayTakingDamageSound();
        UpdateHealthBar();
        if(!bloodOverlayCoroutineRunning) StartCoroutine(ShowBloodOverlay());
        if(currentHealth <= 0)
        {
            OnPlayerDead?.Invoke();
        }
    }

    private void UpdateHealthBar() => UIManager.Instance.GetHealthImage().fillAmount = (float)currentHealth / maxHealth;

    public void Heal(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        CheckIfPlayerIsDamaged();
        UpdateHealthBar();
    }

    private IEnumerator ShowBloodOverlay()
    {
        bloodOverlayCoroutineRunning = true;
        UIManager.Instance.GetBloodOverlayImage().gameObject.SetActive(true);

        yield return new WaitForSeconds(0.3f);

        UIManager.Instance.GetBloodOverlayImage().gameObject.SetActive(false);
        bloodOverlayCoroutineRunning = false;

    }

    private void SetCurrentHealthToMax()
    {
        currentHealth = maxHealth;
        CheckIfPlayerIsDamaged();
        UpdateHealthBar();
    }

    private void GameManager_OnGameRestarted()
    {
        SetCurrentHealthToMax();
    }

    private void CheckIfPlayerIsDamaged()
    {
        if (currentHealth != maxHealth) isPlayerDamaged = true;
        else isPlayerDamaged = false;
    }

    public bool GetIsPlayerDamaged() => isPlayerDamaged;
}
