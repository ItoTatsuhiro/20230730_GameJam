using UnityEngine;

/// <summary>�ڒn�����ʂ̖@���x�N�g���������Ă���</summary>
[RequireComponent (typeof(Collider))]
public class PlaneNormalOutputer : MonoBehaviour
{
    Vector3 _planeNormalVector = Vector3.up;

    private void OnCollisionEnter(Collision collision)
    {
        _planeNormalVector = collision.contacts[0].normal;
        Debug.Log(collision.gameObject.name);
    }

    public Vector3 PlaneNormalVector()
    {
        return _planeNormalVector;
    }
}
