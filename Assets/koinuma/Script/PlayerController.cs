using UnityEngine;

/// <summary>
/// プレイヤーを動かすコンポーネント
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] float _moveSpeed = 10;
    [SerializeField] float _jumpPower = 10;
    /// <summary>空中での方向転換のスピード</summary>
    [SerializeField] float _turnSpeed = 3;
    Rigidbody _rb;
    Animator _anim;
    bool _jumped;
    float _jumpedTimer;
    bool _isGround;
    Vector3 _planeNormalVector;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (_jumped) // 一定時間接地判定にさせない
        {
            _jumpedTimer += Time.deltaTime;
            if (_jumpedTimer > 0.3f)
            {
                _jumped = false;
            }
            _isGround = false;
        }

        // 水平移動処理
        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");
        Vector3 dir = new Vector3(h, 0, v); // 移動方向
        dir = Camera.main.transform.TransformDirection(dir); //カメラ基準のベクトルに直す
        dir.y = 0;
        dir = dir.normalized; // 単位化してある水平方向の入力ベクトル
        Vector3 velo = dir * _moveSpeed;
        velo.y = _rb.velocity.y;
        if (!_isGround)
        {
            // 空中でゆっくり方向転換が可能
            velo = _rb.velocity;
            if (dir.magnitude != 0f)
            {
                // 速度の大きさを保持しながら向きを少しずつ変える
                Vector2 startHoriVelo = new Vector2(_rb.velocity.x, _rb.velocity.z);
                float horiMag = startHoriVelo.magnitude;
                if (horiMag < 10f)
                {
                    horiMag = 10;
                }
                Vector2 endHoriVelo = new Vector2(dir.x * horiMag, dir.z * horiMag);
                float turnSpeed = _turnSpeed * Time.deltaTime;
                Vector2 airHoriVelo = endHoriVelo * turnSpeed + startHoriVelo * (1 - turnSpeed);
                velo = new Vector3(airHoriVelo.x, _rb.velocity.y, airHoriVelo.y);
            }
            _rb.velocity = velo;
        }
        else
        {
            Vector3 onPlaneVelo = Vector3.ProjectOnPlane(velo, _planeNormalVector);
            if (Input.GetButton("Jump"))
            {
                RbAddPower();
                onPlaneVelo.y = _jumpPower;
                _anim.SetTrigger("Jump");
            }

            _rb.velocity = onPlaneVelo; // 接地中はvelocityを書き換える
        }

        // 進行方向を向く
        if (dir.magnitude != 0)
        {
            var rotation = Quaternion.LookRotation(dir, Vector3.up);
            transform.rotation = rotation;
        }

        // アニメーターの設定
        _anim.SetFloat("Speed", dir.magnitude);
        _anim.SetBool("IsGround", _isGround);
    }

    /// <summary>ジャンプなどをしたとき一定時間接地判定にさせないためのメソッド</summary>
    public void RbAddPower()
    {
        _jumpedTimer = 0;
        _jumped = true;
    }

    private void OnTriggerStay(Collider other)
    {
        _isGround = true;
    }
    private void OnTriggerExit(Collider other)
    {
        _isGround = false;
    }

    private void OnCollisionStay(Collision collision)
    {
        float angle = Vector3.Angle(Vector3.up, collision.contacts[0].normal);
        if (angle < 45)
        {
            _planeNormalVector = collision.contacts[0].normal;
        }
        if (collision.gameObject.tag == "Hurdle")
        {
            RbAddPower();
            Debug.Log("atatta");
        }
    }
}
