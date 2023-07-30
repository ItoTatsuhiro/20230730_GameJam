using UnityEngine;

/// <summary>
/// �v���C���[�𓮂����R���|�[�l���g
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] float _moveSpeed = 10;
    [SerializeField] float _jumpPower = 10;
    /// <summary>�󒆂ł̕����]���̃X�s�[�h</summary>
    [SerializeField] float _turnSpeed = 3;
    [SerializeField] PlaneNormalOutputer _planeNormal;
    Rigidbody _rb;
    Animator _anim;
    /// <summary>�ڒn����̋���</summary>
    //float _isGroundedLength = 1.1f;
    bool _jumped;
    float _jumpedTimer;
    bool IsGround;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
        //_isGroundedLength = GetComponent<CapsuleCollider>().height / 2 + 0.1f;
    }

    void Update()
    {
        if (_jumped) // ��莞�Ԑڒn����ɂ����Ȃ�
        {
            _jumpedTimer += Time.deltaTime;
            if (_jumpedTimer > 0.3f)
            {
                _jumped = false;
            }
            IsGround = false;
        }

        // �����ړ�����
        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");
        Vector3 dir = new Vector3(h, 0, v); // �ړ�����
        dir = Camera.main.transform.TransformDirection(dir); //�J������̃x�N�g���ɒ���
        dir.y = 0;
        dir = dir.normalized; // �P�ʉ����Ă��鐅�������̓��̓x�N�g��
        Vector3 velo = dir * _moveSpeed;
        velo.y = _rb.velocity.y;
        if (!IsGround)
        {
            // �󒆂ł����������]�����\
            velo = _rb.velocity;
            if (dir.magnitude != 0f)
            {
                // ���x�̑傫����ێ����Ȃ���������������ς���
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
            //if (dir.magnitude == 0f)
            //{
            //    velo.y = -10;
            //}
            
            if (Input.GetButton("Jump"))
            {
                RbAddPower();
                velo.y = _jumpPower;
            }

            //Vector3 onPlaneVelo = Vector3.ProjectOnPlane(velo, _planeNormal.PlaneNormalVector());
            _rb.velocity = velo; // �ڒn����velocity������������
        }

        // �i�s����������
        if (dir.magnitude != 0)
        {
            var rotation = Quaternion.LookRotation(dir, Vector3.up);
            transform.rotation = rotation;
        }

        // �A�j���[�^�[�̐ݒ�
        _anim.SetFloat("Speed", dir.magnitude);
        _anim.SetBool("Jump", !IsGround);
    }

    /// <summary>�W�����v�Ȃǂ������Ƃ���莞�Ԑڒn����ɂ����Ȃ����߂̃��\�b�h</summary>
    public void RbAddPower()
    {
        _jumpedTimer = 0;
        _jumped = true;
    }

    private void OnTriggerStay(Collider other)
    {
        IsGround = true;
    }
    private void OnTriggerExit(Collider other)
    {
        IsGround = false;
    }
}