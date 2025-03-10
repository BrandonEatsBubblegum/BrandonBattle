using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SniperBall : BattleBall
{
    public GameObject projectile;
    public float nextShootTime;
    public float shootInterval;
    public float shootPower;
    public Transform shootSpot;

    protected override void Start()
    {
        base.Start();
        nextShootTime = Time.time + shootInterval;
    }
    protected override void Update()
    {
        base.Update();
        if(currentTarget != null)
        {
            Vector3 targetPos = currentTarget.position;
            Vector3 targetDir = (targetPos - transform.position).normalized;
            transform.forward = targetDir;
        }
        if(Time.time > nextShootTime)
        {
            nextShootTime = nextShootTime + shootInterval;
            Shoot();
        }
    }
    void Shoot()
    {
        GameObject instance = Instantiate(projectile, shootSpot.position, shootSpot.rotation);
        Rigidbody RB = instance.GetComponent<Rigidbody>();
        RB.AddForce(shootSpot.forward * shootPower, ForceMode.VelocityChange);
        instance.GetComponent<BattleBall>().enemyBase = enemyBase;
    }
}
