using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AK47Component : WeaponComponent
{
    private Vector3 hitLocation;

    protected override void Fire()
    {
        if (stats.bulletsInClip > 0 && !isReloading && (stats.fireWhileMoving || !weaponHolder.controller.isRunning))
        {
            --stats.bulletsInClip;
            base.Fire();

            if (firingEffect)
                firingEffect.Play();

            Ray screenRay = mainCamera.ViewportPointToRay(new Vector2(0.5f, 0.5f));
            if (Physics.Raycast(screenRay, out RaycastHit hit, stats.range, stats.weaponHitLayers))
            {
                hitLocation = hit.point;
                DealDamage(hit);
                Vector3 hitDirection = hit.point - mainCamera.transform.position;
                Debug.DrawRay(mainCamera.transform.position, hitDirection * stats.range, Color.red, 5.0f);
            }
        }
        else if (stats.bulletsInClip <= 0)
        {
            weaponHolder.StartReloading();
        }
        else
        {
            StopFiring();
        }
    }

    void DealDamage(RaycastHit hitInfo)
    {
        Debug.Log(Time.time + " Layer: " + LayerMask.LayerToName(hitInfo.collider.gameObject.layer) + " Name: " + hitInfo.collider.gameObject.name);
        hitInfo.collider.GetComponent<IDamageable>()?.TakeDamage(stats.damage);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(hitLocation, 0.1f);
    }
}
