using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankBall : BattleBall
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
