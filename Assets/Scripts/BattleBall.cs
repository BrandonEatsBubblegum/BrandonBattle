using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleBall : MonoBehaviour
{
    public float minBouncePower = 10f;
    public float maxBouncePower = 10f;
    public float moveSpeed = 10f;
    public float health = 100f;
    public float pointReward = 5;
    public float minDamage = 10f;
    public float maxDamage = 20f;
    public float deathTime = -1f;
    public string team;
    public LayerMask sightLM;
    public float range = 10;
    protected Transform currentTarget;
    public Transform enemyBase;
    Rigidbody rb;
    float nextCheckTime = 0f;
    float checkInterval = .5f;
    Color originalColor;
    public bool faceDir;
    public bool invisible = false;
    public GameObject particleEffect;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        if(deathTime >= 0)
        {
            Destroy(gameObject, deathTime);
        }
        //originalColor = GetComponentInChildren<MeshRenderer>().material.color;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if(Time.time > nextCheckTime)
        {
            nextCheckTime = Time.time + checkInterval;
            SetTarget();
        }
    }
    protected virtual void FixedUpdate()
    {
        if(currentTarget != null)
        {
            Vector3 targetPos = currentTarget.position;
            Vector3 targetDir = (targetPos - transform.position).normalized;
            rb.AddForce(targetDir * moveSpeed * rb.mass);
            if(faceDir)
            {
                Vector3 horizontalVel = rb.velocity;
                horizontalVel.y = 0;
                Vector3 moveDir = horizontalVel.normalized;
                Quaternion moveDirRot = Quaternion.LookRotation(moveDir);
                rb.MoveRotation(moveDirRot);
            }
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.CompareTag("bouncewall"))
        {
            Vector3 rand = Random.insideUnitSphere.normalized;
            rb.AddForce(rand * 5f, ForceMode.Impulse);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        BattleBall battleBall = collision.gameObject.GetComponent<BattleBall>();
        if (battleBall != null)
        {
            if(battleBall.team == this.team)
            {
                return;
            }
            float bouncePower = Random.Range(minBouncePower, maxBouncePower);
            Rigidbody otherRB = collision.rigidbody;
            Vector3 pushVector = (collision.transform.position - transform.position).normalized;
            otherRB.AddForce(pushVector * bouncePower, ForceMode.Impulse);
            otherRB.AddForce(Vector3.up * (bouncePower / 2), ForceMode.Impulse);
            float reward = battleBall.TakeDamage(Random.Range(minDamage,maxDamage));
            Instantiate(particleEffect, collision.contacts[0].point, transform.rotation);
            if(reward != 0)
            {
                PointGiver.main.GivePoints(team, reward);
            }
        }
        Spawner spawn = collision.gameObject.GetComponent<Spawner>();
        if(spawn != null)
        {
            if (spawn.team != this.team)
            {
                spawn.TakeDamage(Random.Range(minDamage, maxDamage));
            }
        }
    }
    public float TakeDamage(float damage)
    {
        health -= damage;
        float interp = health / 100f;
        //GetComponent<MeshRenderer>().material.color = Color.Lerp(Color.black, originalColor, interp);
        if(health <= 0)
        {
            Destroy(gameObject);
            return pointReward;
        }
        return 0;
    }
    public virtual void SetTarget()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, range, sightLM);
        bool foundTarget = false;
        if (colliders.Length > 0)
        {
            foreach(Collider col in colliders)
            {
                if(col.GetComponent<BattleBall>()!= null)
                {
                    if (col.GetComponent<BattleBall>().invisible == false)
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
        }
        if(!foundTarget)
        {
            currentTarget = enemyBase;
        }
    }
}
