using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_ground : MonoBehaviour
{
    [SerializeField]
    Rigidbody rigidBody = null;

    [SerializeField]
    Vector3 speed = Vector3.zero;

    List<Rigidbody> rigidBodies = new();

    void FixedUpdate()
    {
        Move_Ground();
        AddVelocity();
    }

    void OnTriggerEnter(Collider other)
    {
        rigidBodies.Add(other.gameObject.GetComponent<Rigidbody>());
    }

    void OnTriggerExit(Collider other)
    {
        rigidBodies.Remove(other.gameObject.GetComponent<Rigidbody>());
    }

    void Move_Ground()
    {
        rigidBody.MovePosition(transform.position + Time.fixedDeltaTime * speed);
    }

    void AddVelocity()
    {
        if (rigidBody.velocity.sqrMagnitude <= 0.01f)
        {
            return;
        }

        for (int i = 0; i < rigidBodies.Count; i++)
        {
            rigidBodies[i].AddForce(rigidBody.velocity, ForceMode.VelocityChange);
        }
    }

}