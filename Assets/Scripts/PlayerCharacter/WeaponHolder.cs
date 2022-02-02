using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponHolder : MonoBehaviour
{
    [Header("WeaponToSpawn"), SerializeField]
    private GameObject weaponToSpawn;

    public Sprite crossHairImage;

    [SerializeField]
    private GameObject weaponSocket;
    [SerializeField]
    private Transform gripIKSocket;
    [SerializeField]
    private WeaponComponent equippedWeapon;

    #region Component Reference Variables
    private PlayerController playerController;
    private Animator animator;
    #endregion

    public readonly int isFiringHash = Animator.StringToHash("isFiring");
    public readonly int isReloadingHash = Animator.StringToHash("isReloading");

    void Start()
    {
        GameObject spawnedWeaponGO = Instantiate(weaponToSpawn, weaponSocket.transform.position, weaponSocket.transform.rotation, weaponSocket.transform);
        WeaponComponent spawnedWeapon = spawnedWeaponGO.GetComponent<WeaponComponent>();
        animator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
        equippedWeapon = spawnedWeapon;
        gripIKSocket = equippedWeapon.gripLocation;
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (equippedWeapon && equippedWeapon.gripLocation)
        {
            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, playerController.isReloading ? 0.0f : 1.0f);
            animator.SetIKPosition(AvatarIKGoal.LeftHand, gripIKSocket.transform.position);
        }
    }

    public void OnReload(InputValue value)
    {
        playerController.isReloading = value.isPressed;
        animator.SetBool(isReloadingHash, playerController.isReloading);
    }

    public void OnFire(InputValue value)
    {
        playerController.isFiring = value.isPressed;
        animator.SetBool(isFiringHash, playerController.isFiring);
    }
}
