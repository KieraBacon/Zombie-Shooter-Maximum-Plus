using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    None, Pistol, MachineGun,
}

public enum WeaponFiringPattern
{
    SemiAuto, FullAuto, ThreeShotBurst, FiveShotBurst,
}

[System.Serializable]
public struct WeaponStats
{
    public WeaponType type;
    public WeaponFiringPattern firingPattern;
    public string weaponName;
    public float damage;
    public int bulletsInClip;
    public int clipSize;
    public float fireStartDelay;
    public float refireRate;
    public float range;
    public bool repeating;
    public LayerMask weaponHitLayers;
}

public class WeaponComponent : MonoBehaviour
{
    [SerializeField]
    private Transform _gripLocation;
    public Transform gripLocation => _gripLocation;
    public WeaponStats stats;
    public WeaponHolder weaponHolder;
    protected bool isFiring;
    protected bool isReloading;
    
    protected Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    public virtual void StartFiring()
    {
        isFiring = true;
        if (stats.repeating)
        {
            InvokeRepeating(nameof(Fire), stats.fireStartDelay, stats.refireRate);
        }
        else
        {
            Fire();
        }
    }

    public virtual void StopFiring()
    {
        isFiring = false;
        CancelInvoke(nameof(Fire));
    }

    protected virtual void Fire()
    {
        --stats.bulletsInClip;
        Debug.Log("Firing weapon! " + stats.bulletsInClip + " bullets left in clip.");
    }
}
