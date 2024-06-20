using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour, IPickable
{
    [SerializeField] private int healAmount = 30;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Pickup(other.gameObject);
        }
    }


    public void Pickup(GameObject player)
    {
        AudioManager.Instance.PlayHealthPickupSound();
        player.GetComponent<PlayerHealth>().Heal(healAmount);
        Destroy(gameObject);
    }
}
