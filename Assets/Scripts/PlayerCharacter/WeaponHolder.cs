using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    [Header("WeaponToSpawn"), SerializeField]
    private GameObject weaponToSpawn;

    public Sprite crossHairImage;

    [SerializeField]
    private GameObject weaponSocketLocation;

    void Start()
    {
        GameObject spawnedWeapon = Instantiate(weaponToSpawn, weaponSocketLocation.transform.position, weaponSocketLocation.transform.rotation, weaponSocketLocation.transform);
        //GameObject spawnedWeapon = Instantiate(weaponToSpawn, weaponSocketLocation.transform, false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
