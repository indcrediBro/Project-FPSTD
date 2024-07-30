using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeaponBase : MonoBehaviour
{

    public Camera pov;
    public TextMeshProUGUI ammoDisplay;
    public GameObject bullet;
    public float range = 100f;
    public float fireRate = 0.1f;
    public float reloadTime = 1f;
    public float bulletLaunchVelocity = 10f;
    public int ammoSize = 20;
    public int reserveSize = 60;

    public Boolean charger = false;
    public Boolean partialCharger = false;
    public float chargeTime = 0f;

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

        var attemptReload = Input.GetKeyDown(KeyCode.R);
        var attemptFire = Input.GetKey(KeyCode.Mouse0);

        if (isFiring == false && isReloading == false)
        {

            if (attemptReload) { Reload(); }

            else
            {
                if (currentAmmo > 0)
                {
                    if (charger == true)
                    {
                        Charge();
                    }
                    else if (attemptFire)
                    {
                        Fire();
                    };
                }
                else { Reload(); }
            }
        }
    }

    private void Charge()
    {
        var attemptFire = Input.GetKey(KeyCode.Mouse0);
        if (attemptFire == true)
        {
            currentCharge = Math.Min(currentCharge + Time.deltaTime, chargeTime);
            UpdateText();
        }
        else if (currentCharge >= chargeTime) { Fire(); }
        else if (currentCharge > 0 && partialCharger == true) { Fire(); }
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
        Ray ray = pov.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        Vector3 aim;
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, range))
            aim = hit.point;
        else
            aim = ray.GetPoint(range);
        Vector3 direction = aim - _bulletSpawnPoint.position;

        // fire bullet
        GameObject newBullet = Instantiate(bullet, _bulletSpawnPoint.position, Quaternion.LookRotation(direction));
        newBullet.transform.forward = direction;

        float variableBulletLaunchVelocity;

        if (charger || partialCharger) variableBulletLaunchVelocity = bulletLaunchVelocity * currentCharge;
        else variableBulletLaunchVelocity = bulletLaunchVelocity;
        Debug.Log("variableBulletLaunchVelocity: " + variableBulletLaunchVelocity);
        newBullet.GetComponent<Rigidbody>().AddForce(direction.normalized * variableBulletLaunchVelocity, ForceMode.Impulse);

        // done
        currentAmmo--;
        currentCharge = 0;
        UpdateText();
        Invoke("DoneFire", fireRate);
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

        // done
        currentAmmo += toRefill;
        Invoke("DoneReload", reloadTime);
    }

    private void DoneReload()
    {
        isReloading = false;
        UpdateText();
    }

    private void UpdateText()
    {
        if (ammoDisplay != null)
        {
            var maybeCharge = "";
            if (charger == true)
            {
                maybeCharge = "\n" + "(" + Math.Round(currentCharge, 2) + "/" + Math.Round(chargeTime, 2) + ")";
            }
            ammoDisplay.SetText(currentAmmo + " / " + currentReserve + maybeCharge);
        }
    }
}
