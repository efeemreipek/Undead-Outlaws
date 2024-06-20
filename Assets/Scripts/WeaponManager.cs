using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private List<Weapon> weaponList = new List<Weapon>();

    private int currentWeaponIndex = 0;
    private Weapon currentWeapon;

    private void Start()
    {
        currentWeapon = weaponList[currentWeaponIndex];
        ReloadAllWeapons();

        GameManager.OnGameRestarted += GameManager_OnGameRestarted;
    }

    private void Update()
    {
        if (InputManager.Instance.GetIsWeaponOnePressed())
        {
            SetWeaponActive(0);
        }

        if (InputManager.Instance.GetIsWeaponTwoPressed())
        {
            SetWeaponActive(1);
        }

        if (InputManager.Instance.GetIsWeaponThreePressed())
        {
            SetWeaponActive(2);
        }

        if (InputManager.Instance.GetIsWeaponUpPressed())
        {
            currentWeaponIndex = (currentWeaponIndex + 1) % weaponList.Count;
            SetWeaponActive(currentWeaponIndex);
        }

        if (InputManager.Instance.GetIsWeaponDownPressed())
        {
            currentWeaponIndex = (currentWeaponIndex - 1 + weaponList.Count) % weaponList.Count;
            SetWeaponActive(currentWeaponIndex);
        }
    }

    private void SetWeaponActive(int index)
    {
        foreach (var weapon in weaponList)
        {
            if (weapon.Equals(weaponList[index]))
            {
                weapon.gameObject.SetActive(true);
                currentWeaponIndex = index;
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }

        }
    }

    private void ReloadAllWeapons()
    {
        foreach(var weapon in weaponList)
        {
            weapon.SetCurrentAmmoToMax();
        }
    }

    private void GameManager_OnGameRestarted()
    {
        ReloadAllWeapons();
    }
}
