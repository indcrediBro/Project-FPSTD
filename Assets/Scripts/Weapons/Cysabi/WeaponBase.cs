using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum WeaponType
{
    AUTO,
    CHARGE,
    PARTIAL_CHARGE,
};


// Should be separated in multiple weapon type categories
public class WeaponBase : MonoBehaviour
{
    public Camera m_playerPov;
    public TextMeshProUGUI ammoDisplay;

    // If it does not need to be public, use [SerializeField] private instead
    public GameObject bullet;
    public WeaponType type;
    public float range = 100f;
    public float fireRate = 0.1f;
    public float reloadTime = 1f;
    public float bulletLaunchVelocity = 10f;
    public int ammoSize = 20;
    public int reserveSize = 60;
    public float chargeTime = 1f;

    [HideInInspector]
    public Transform _bulletSpawnPoint;
    private float currentCharge;
    private int currentAmmo;
    private int currentReserve;
    private bool isReloading = false;
    private bool isFiring = false;

    private void Awake()
    {
        currentAmmo = ammoSize;
        currentReserve = reserveSize;
        UpdateText();
    }

    private void Update()
    {
        // Should be using new input system
        bool attemptReload = Input.GetKeyDown(KeyCode.R);
        bool attemptFire = Input.GetKey(KeyCode.Mouse0);

        if (isFiring == false && isReloading == false)
        {
            if (attemptReload)
            {
                Reload();
            }

            else
            {
                if (currentAmmo > 0)
                {
                    if (type == WeaponType.CHARGE || type == WeaponType.PARTIAL_CHARGE)
                    {
                        Charge();
                    }
                    else if (attemptFire)
                    {
                        Fire();
                    }
                    ;
                }
                else
                {
                    Reload();
                }
            }
        }
    }

    private void Charge()
    {
        // Use explicit types instead
        var attemptFire = Input.GetKey(KeyCode.Mouse0);
        if (attemptFire == true)
        {
            currentCharge = Math.Min(currentCharge + Time.deltaTime, chargeTime);
            UpdateText();
        }
        else if (currentCharge >= chargeTime)
        {
            Fire();
        }
        else if (currentCharge > 0 && type == WeaponType.PARTIAL_CHARGE)
        {
            Fire();
        }
        else
        {
            currentCharge = 0;
            UpdateText();
        }
    }

    private void Fire()
    {
        // lock fire
        isFiring = true;

        // calculate direction to aim
        Ray ray;
        if (m_playerPov)
        {
            ray = m_playerPov.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        }
        // Camera.main is inefficient
        else
        {
            ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        }

        Vector3 aim;
        if (Physics.Raycast(ray, out RaycastHit hit, range))
        {
            aim = hit.point;
        }
        else
        {
            aim = ray.GetPoint(range);
        }
        Vector3 direction = aim - _bulletSpawnPoint.position;

        // Extract all described functionality into own methods. CalculatePower(), FireBullet()

        // calculate power
        float power = 1;
        if (type == WeaponType.PARTIAL_CHARGE)
        {
            power = currentCharge / chargeTime;
        }

        // fire bullet
        GameObject newBullet = Instantiate(bullet, _bulletSpawnPoint.position, Quaternion.LookRotation(direction));
        newBullet.transform.forward = direction;
        // Make sure you actually got that component
        newBullet.GetComponent<Rigidbody>().AddForce(direction.normalized * (bulletLaunchVelocity * currentCharge), ForceMode.Impulse);
        newBullet.GetComponent<WeaponBaseProjectile>().power = power;

        currentAmmo--;
        currentCharge = 0;
        UpdateText();
        Invoke(nameof(DoneFire), fireRate);
    }

    private void DoneFire()
    {
        isFiring = false;
    }

    private void Reload()
    {
        // can reload?
        var toRefill = ammoSize - currentAmmo;
        if (toRefill <= 0 || currentReserve <= 0)
        {
            return;
        }

        // reloading
        isReloading = true;
        if (currentReserve < toRefill)
        {
            toRefill = currentReserve;
            currentReserve = 0;
        }
        else
        {
            currentReserve -= toRefill;
        }

        currentAmmo += toRefill;
        Invoke(nameof(DoneReload), reloadTime);
    }

    private void DoneReload()
    {
        isReloading = false;
        UpdateText();
    }

    // This logic should be separated into its own UI script
    private void UpdateText()
    {
        if (ammoDisplay != null)
        {
            var maybeCharge = "";
            if (type == WeaponType.PARTIAL_CHARGE)
            {
                // This should be simplified
                maybeCharge = "\n" + "(" + Math.Round(currentCharge, 2) + "/" + Math.Round(chargeTime, 2) + ")";

            }
            else if (type == WeaponType.CHARGE && currentCharge >= chargeTime)
            {
                maybeCharge = "CHARGED";

            }

            ammoDisplay.SetText(currentAmmo + " / " + currentReserve + maybeCharge);
        }
    }
}