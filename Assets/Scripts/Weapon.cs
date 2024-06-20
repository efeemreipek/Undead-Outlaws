using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponData weaponData;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePointTransform;

    private float timeSinceLastShot = 0f;

    private void Start()
    {
        weaponData.currentAmmo = weaponData.maxAmmo;
    }

    private void OnEnable()
    {
        weaponData.isReloading = false;
        UIManager.Instance.GetWeaponNameText().text = weaponData.weaponName.ToUpperInvariant();
        UpdateAmmoUI();
    }

    private void Update()
    {
        timeSinceLastShot += Time.deltaTime;

        if (InputManager.Instance.GetIsShootPressed())
        {
            Shoot();
        }

        if (InputManager.Instance.GetIsReloadKeyPressed())
        {
            StartReloading();
        }
    }

    private void StartReloading()
    {
        if (!weaponData.isReloading && weaponData.currentAmmo < weaponData.maxAmmo)
        {
            StartCoroutine(Reload());
        }
    }

    private IEnumerator Reload()
    {
        weaponData.isReloading = true;
        UIManager.Instance.GetReloadingText().gameObject.SetActive(true);
        AudioManager.Instance.PlayReloadingSound();
        yield return new WaitForSeconds(weaponData.reloadTime);
        SetCurrentAmmoToMax();
        weaponData.isReloading = false;
        UIManager.Instance.GetReloadingText().gameObject.SetActive(false);
        UpdateAmmoUI();
    }

    private bool CanShoot() => !weaponData.isReloading && timeSinceLastShot > 1f / (weaponData.fireRate / 60f);

    private void Shoot()
    {
        if(weaponData.currentAmmo > 0 && CanShoot())
        {
            GameObject bulletGO = Instantiate(bulletPrefab, firePointTransform.position, firePointTransform.rotation);
            Bullet bullet = bulletGO.GetComponent<Bullet>();

            if(weaponData.weaponName == "Thompson M1A1")
            {
                AudioManager.Instance.PlayShootingSound2();
            }
            else if(weaponData.weaponName == "Revolver")
            {
                AudioManager.Instance.PlayShootingSound3();
            }
            else
            {
                AudioManager.Instance.PlayShootingSound();
            }

            int damage = Random.Range(weaponData.minDamage, weaponData.maxDamage + 1);
            bool isCritical = damage == weaponData.maxDamage;
            bullet.SetIsCriticalHit(isCritical);
            bullet.SetDamage(damage);
            bullet.FireBullet(firePointTransform.forward);

            CameraShake.Instance.Shake(weaponData.shakeDuration, weaponData.shakeAmount);

            weaponData.currentAmmo--;
            UpdateAmmoUI();

            timeSinceLastShot = 0f;
        }

        if(weaponData.currentAmmo <= 0 && CanShoot())
        {
            AudioManager.Instance.PlayEmptyClipSound();
            timeSinceLastShot = 0f;
        }
    }

    private void UpdateAmmoUI() => UIManager.Instance.GetAmmoCountText().text = weaponData.currentAmmo.ToString() + "/" + weaponData.maxAmmo.ToString();

    public void SetCurrentAmmoToMax()
    {
        weaponData.currentAmmo = weaponData.maxAmmo;
    }
    
}
