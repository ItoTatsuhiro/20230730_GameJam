using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    [Header("playerのゲームオブジェクト")] public GameObject player_;

    [Header("中間地点")] public GameObject[] continuePoint_;

    [Header("落下判定")] public GameObject fall_;

    // 通過した中間地点の数
    public int continuePonitCount_ = 0;

    void Start()
    {
        if (player_ != null && continuePoint_ != null && continuePoint_.Length > 0)
        {
            player_.transform.position = continuePoint_[0].transform.position;
        }
        else
        {
            Debug.Log("オブジェクトの設定漏れ");
        }

    }

    void Update()
    {

    }
}
