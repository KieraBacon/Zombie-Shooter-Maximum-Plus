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
    [SerializeField]
    private WeaponStats stats;
}
