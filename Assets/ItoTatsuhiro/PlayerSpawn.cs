using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    [Header("player�̃Q�[���I�u�W�F�N�g")] public GameObject player_;

    [Header("���Ԓn�_")] public GameObject[] continuePoint_;

    [Header("��������")] public GameObject fall_;

    // �ʉ߂������Ԓn�_�̐�
    public int continuePonitCount_ = 0;

    void Start()
    {
        if (player_ != null && continuePoint_ != null && continuePoint_.Length > 0)
        {
            player_.transform.position = continuePoint_[0].transform.position;
        }
        else
        {
            Debug.Log("�I�u�W�F�N�g�̐ݒ�R��");
        }

    }

    void Update()
    {

    }
}
