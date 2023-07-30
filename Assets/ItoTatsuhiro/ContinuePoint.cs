using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class ContinuePoint : MonoBehaviour
{
    [Header("playerspawnのゲームオブジェクト")] public GameObject _playerSpawn;

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
                        Debug.Log("追加済の中間地点");
                        return;
                    }

                    else
                    {
                        _ps.continuePonitCount_++;
                        _ps.continuePoint_[_ps.continuePonitCount_].transform.position = other.transform.position;
                        Debug.Log("中間地点追加！");
                    }
                }
            }
            else
            {
                Debug.Log("中間地点の配列が不足しています。");
            }
        }
    }

}
