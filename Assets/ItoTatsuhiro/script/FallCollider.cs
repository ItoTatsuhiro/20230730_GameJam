using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class FallCollider : MonoBehaviour
{
    [Header("playerspawn�̃Q�[���I�u�W�F�N�g")] public GameObject _playerSpawn;

    // 
    private PlayerSpawn _ps;

    // Start is called before the first frame update
    void Start()
    {
        _ps = _playerSpawn.GetComponent<PlayerSpawn>();
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _ps.player_.transform.position = _ps.continuePoint_[_ps.continuePonitCount_].transform.position;
            Debug.Log("���X�^�[�g");

        }
    }
}
