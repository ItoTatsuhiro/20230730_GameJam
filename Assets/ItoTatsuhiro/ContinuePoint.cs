using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class ContinuePoint : MonoBehaviour
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
            if (_ps.continuePonitCount_ < _ps.continuePoint_.Length - 1)
            {
                for (int i = 0; i <= _ps.continuePonitCount_; ++i)
                {
                    if (_ps.continuePoint_[i].transform.position == other.transform.position)
                    {
                        Debug.Log("�ǉ��ς̒��Ԓn�_");
                        return;
                    }

                    else
                    {
                        _ps.continuePonitCount_++;
                        _ps.continuePoint_[_ps.continuePonitCount_].transform.position = other.transform.position;
                        Debug.Log("���Ԓn�_�ǉ��I");
                    }
                }
            }
            else
            {
                Debug.Log("���Ԓn�_�̔z�񂪕s�����Ă��܂��B");
            }
        }
    }

}
