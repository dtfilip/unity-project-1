using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsHandler : MonoBehaviour
{
    [SerializeField] float velocity = 5;
    void jump(Vector3 normalized)
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(normalized * velocity, ForceMode.VelocityChange);
    }

    private void OnTriggerEnter(Collider other)
    {
        Vector3 dif = other.transform.position - transform.position;
        jump(-dif.normalized);
    }
}
