using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBall : BattleBall
{
    public float spread = 0.1f;
    public GameObject projectile;
    public float nextShootTime;
    public float shootInterval;
    public float shootPower;
    public Transform shootSpot;
    public Transform turret;

    protected override void Start()
    {
        base.Start();
        nextShootTime = Time.time + shootInterval;
    }
    protected override void Update()
    {
        base.Update();
        if (currentTarget != null && Time.time > nextShootTime)
        {
            nextShootTime = Time.time + shootInterval;
            Shoot();
        }
    }
    protected override void FixedUpdate()
    {

        if (currentTarget != null)
        {
            Vector3 targetPos = currentTarget.position;
            Vector3 targetDir = (targetPos - turret.position).normalized;
            turret.forward = targetDir;
        }
    }
    void Shoot()
    {
        GameObject instance = Instantiate(projectile, shootSpot.position, shootSpot.rotation);
        Rigidbody RB = instance.GetComponent<Rigidbody>();
        Vector3 dir = shootSpot.forward + (Random.insideUnitSphere * spread);
        dir = dir.normalized;
        RB.AddForce(dir * shootPower, ForceMode.VelocityChange);
        instance.GetComponent<BattleBall>().enemyBase = enemyBase;
    }

    public override void SetTarget()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, range, sightLM);
        bool foundTarget = false;
        if (colliders.Length > 0)
        {
            foreach (Collider col in colliders)
            {
                if (col.GetComponent<BattleBall>() != null)
                {
                    if (col.GetComponent<BattleBall>().team != team)
                    {
                        currentTarget = col.transform;
                        foundTarget = true;
                        break;
                    }
                }
            }
        }
        if (!foundTarget)
        {
            currentTarget = null;
        }
    }
}
