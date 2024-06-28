using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondPickup : MonoBehaviour, IPickable
{
    public static int diamondCount = 0;


    private void Start()
    {
        UpdateDiamondUI();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Pickup(other.gameObject);
        }
    }

    public void Pickup(GameObject player)
    {
        AudioManager.Instance.PlayDiamondPickupSound();
        diamondCount++;
        UpdateDiamondUI();
        Destroy(gameObject);
    }

    public static void SetDiamondCountToZero() => diamondCount = 0;
    public static void UpdateDiamondUI() => UIManager.Instance.GetDiamondCountText().text = diamondCount.ToString();
    public static void UpdateDiamondScoreUIEndGame() => UIManager.Instance.GetScoreText().text = "SCORE: " + diamondCount.ToString();
    public static void UpdateHighestDiamondScoreUIEndGame(int highScore) => UIManager.Instance.GetHighestScoreText().text = "HIGHEST SCORE: " + highScore;
}
