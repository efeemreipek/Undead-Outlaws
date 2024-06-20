using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    public static GameAssets Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    public Transform damagePopupPrefab;
    public GameObject zombiePrefab;
    public GameObject healthPickupPrefab;
    public GameObject diamondPickupPrefab;
    public GameObject bloodSplatFXPrefab;
}
