using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private float range = 10f;
    [SerializeField] private float countdown = 5f;
    [SerializeField] private PlayerHealth playerHealth;

    private int healthPickupCountPerWave = 1;
    private int diamondPickupCountPerWave = 2;
    private int currentEnemyCount = 0;
    private int waveEnemyCount = 5;
    private List<Zombie> zombieList = new List<Zombie>();
    private List<GameObject> pickupList = new List<GameObject>();
    private int waveNumber = 0;

    private enum WaveState
    {
        WaveReady,
        Waiting
    }
    private WaveState waveState;

    private void Start()
    {
        DiamondPickup.UpdateDiamondUI();

        waveState = WaveState.WaveReady;

        Zombie.OnDead += Zombie_OnDead;
        GameManager.OnGameRestarted += GameManager_OnGameRestarted;
    }

    private void Update()
    {
        if (waveState == WaveState.WaveReady && currentEnemyCount == 0 && GameManager.Instance.GetIsGameStarted())
        {
            waveState = WaveState.Waiting;
            StartCoroutine(WaveWaitingCountdown());
        }
    }

    private void WaveIsOn()
    {
        waveEnemyCount += Random.Range(0, 2);

        if (playerHealth.GetIsPlayerDamaged())
        {
            CreatePickupsAtRandomPoint(healthPickupCountPerWave, GameAssets.Instance.healthPickupPrefab);
        }
        CreatePickupsAtRandomPoint(diamondPickupCountPerWave, GameAssets.Instance.diamondPickupPrefab);
        CreateEnemiesAtRandomPoint(waveEnemyCount);
        waveState = WaveState.WaveReady;

        waveNumber++;

        UpdateWaveNumberUI();
        UpdateEnemiesLeftBar();
    }

    IEnumerator WaveWaitingCountdown()
    {
        float timer = countdown;
        UIManager.Instance.GetWaveCountdownText().gameObject.SetActive(true);

        AudioManager.Instance.PlayClockTickingSound();

        while (timer > 0)
        {
            timer -= Time.deltaTime;

            UIManager.Instance.GetWaveCountdownText().text = ((int)timer + 1).ToString();

            yield return null;
        }

        
        UIManager.Instance.GetWaveCountdownText().gameObject.SetActive(false);

        WaveIsOn();
    }

    private void CreateEnemiesAtRandomPoint(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 point;

            bool isInNavMesh = RandomPointOnNavMesh.RandomPoint(Vector3.zero, range, out point);
            while (!isInNavMesh)
            {
                isInNavMesh = RandomPointOnNavMesh.RandomPoint(Vector3.zero, range, out point);
            }

            if (isInNavMesh)
            {
                GameObject zombieGO = Instantiate(GameAssets.Instance.zombiePrefab, point, Quaternion.identity);
                Zombie zombie = zombieGO.GetComponent<Zombie>();

                zombieList.Add(zombie);
                currentEnemyCount = zombieList.Count;
            }
        }

        waveState = WaveState.WaveReady;
    }

    private void CreatePickupsAtRandomPoint(int count, GameObject go)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 point;

            bool isInNavMesh = RandomPointOnNavMesh.RandomPoint(Vector3.zero, range, out point);
            while (!isInNavMesh)
            {
                isInNavMesh = RandomPointOnNavMesh.RandomPoint(Vector3.zero, range, out point);
            }

            if (isInNavMesh)
            {
                GameObject pickup = Instantiate(go, point + Vector3.up * 0.5f, Quaternion.identity);
                pickupList.Add(pickup);
            }
        }

    }

    private void Zombie_OnDead(Zombie zombie)
    {
        zombieList.Remove(zombie);
        currentEnemyCount = zombieList.Count;

        UpdateEnemiesLeftBar();
    }

    private void GameManager_OnGameRestarted()
    {
        for (int i = zombieList.Count - 1; i >= 0; i--)
        {
            Destroy(zombieList[i].gameObject);
            zombieList.RemoveAt(i);
        }
        for (int i = pickupList.Count - 1; i >= 0; i--)
        {
            Destroy(pickupList[i]);
            pickupList.RemoveAt(i);
        }

        DiamondPickup.SetDiamondCountToZero();
        DiamondPickup.UpdateDiamondUI();

        currentEnemyCount = 0;
        waveNumber = 0;
        waveState = WaveState.WaveReady;

        UpdateWaveNumberUI();
        UpdateEnemiesLeftBar();

    }

    private void UpdateWaveNumberUI()
    {
        UIManager.Instance.GetWaveNumberText().text = "WAVE " + waveNumber;
    }

    private void UpdateEnemiesLeftBar()
    {
        UIManager.Instance.GetEnemiesLeftImage().fillAmount = (float)currentEnemyCount / waveEnemyCount;
    }
}
