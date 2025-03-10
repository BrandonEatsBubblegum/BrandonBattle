using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    public float magnetRange = 10f;
    public float magnetForce = 10f;
    public AnimationCurve magnetCurve;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        HandleMagnetForce();
    }

    public void HandleMagnetForce()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, magnetRange);
        foreach(Collider collider in colliders)
        {
            MagnetBall mb = collider.GetComponent<MagnetBall>();
            if (mb != null)
            {
                Rigidbody rb = mb.gameObject.GetComponent<Rigidbody>();
                float distance = Vector3.Distance(transform.position, rb.transform.position);
                float interpolator = distance / magnetRange;
                float force = magnetCurve.Evaluate(interpolator);
                Vector3 direction = (transform.position - rb.transform.position).normalized;
                rb.AddForce(direction * force * magnetForce);
            }
        }
    }
}
