using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public float range = 10;
    public LayerMask sightLM;
    private Transform currentTarget;
    public float moveSpeed = 10f;
    public float fleeSpeed = 10f;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void FixedUpdate()
    {
        if (currentTarget == null)
        {
            SetTarget();
        }
        else
        {
            Vector3 targetDir = (currentTarget.position - transform.position).normalized;
            rb.AddForce(targetDir * moveSpeed * rb.mass);

            float curDist = Vector3.Distance(transform.position, currentTarget.position);
            if (curDist > range)
            {
                SetTarget();
            }

        }
        PushAway();
    }
    public void SetTarget()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, range, sightLM);
        if (colliders.Length > 0)
        {
            currentTarget = colliders[0].transform;
        }
    }
    Collider[] nearbyBalls = new Collider[20];
    public void PushAway()
    {
        nearbyBalls = new Collider[20];
        Physics.OverlapSphereNonAlloc(transform.position, 10, nearbyBalls, sightLM);
        foreach (Collider collider in nearbyBalls)
        {
            if(collider!=null)
            {
                Vector3 runDir = (collider.transform.position - transform.position).normalized;
                collider.attachedRigidbody.AddForce(runDir * fleeSpeed * collider.attachedRigidbody.mass);
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<BattleBall>() != null)
        {
            Destroy(collision.gameObject);
        }
    }
}
