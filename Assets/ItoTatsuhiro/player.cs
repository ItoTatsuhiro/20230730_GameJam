using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    private Rigidbody _rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        var h = Input.GetAxis("Horizontal"); // �������������̉��z��
        var v = Input.GetAxis("Vertical"); // �������������̉��z��
        _rigidbody.AddForce(h, 0, v);

        //if (transform.position.y < -3)
        //{
        //    _rigidbody.velocity = Vector3.zero;
        //    _rigidbody.angularVelocity = Vector3.zero;

        //    transform.position = new Vector3(0, 3, 0);
        //}
    }
}
