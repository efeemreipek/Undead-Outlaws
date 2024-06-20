using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName ="WeaponData")]
public class WeaponData : ScriptableObject
{
    [Header("INFO")]
    public string weaponName;

    [Header("SHOOTING")]
    public int minDamage;
    public int maxDamage;
    public int maxDistance;

    [Header("RELOADING")]
    public int currentAmmo;
    public int maxAmmo;
    public int fireRate;
    public float reloadTime;
    public bool isReloading;

    [Header("CAMERA SHAKE")]
    public float shakeDuration;
    public float shakeAmount;
}
