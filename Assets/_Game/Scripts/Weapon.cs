using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private Transform _fpCamera;
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private ParticleSystem bulletHit;
    [SerializeField] private float maxDistance = 100f;
    [SerializeField] private int inflictedDamage = 10;

    private void Start()
    {
        _fpCamera = Camera.main.transform;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            PlayMuzzleFlash();
            Shoot();
        }
    }

    private void PlayMuzzleFlash()
    {
        muzzleFlash.Play(); 
    }

    private void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(_fpCamera.position, _fpCamera.forward, out hit, maxDistance))
        {
            var bulletHitInstance = Instantiate(bulletHit, hit.point,Quaternion.LookRotation(hit.normal));
            Destroy(bulletHitInstance.gameObject, 0.05f);
            var enemyHealth = hit.transform.GetComponent<EnemyHealth>();
            var enemyAI = hit.transform.GetComponent<EnemyAI>();
            if (enemyHealth)
                enemyHealth.DoDamage(inflictedDamage);

            if (enemyAI)
                enemyAI.GotHitByBullet();
        }
    }
}
