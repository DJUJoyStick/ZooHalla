using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject BulletPre;

    public Transform GunParentTr;
    public Transform GunTr;

    SpriteRenderer PlayerSr;

    Rigidbody2D PlayerRig;

    Vector2 _PlayerMoveVec;
    Vector3 SaveMoveVec;
    public Vector3 _MoveVec;                               // 모바일
    Vector3 _RotVec;                                // 모바일

    const float correction = 90f * Mathf.Deg2Rad;   // 모바일
    public float fBulletDirect;
    float fMoveSpeed;
    float _fRotateDegree;
    float fBulletDelay;
    float fGunRot;

    public int nPlayerHp;

    PLAYERDIRECT direction;

    public bool bDmgAccess = false;
    public bool bBulletDirect = false;
    public bool bPlayerDie = false;
    bool bMoveAccess = false;
    public bool bRollin = false;
    [SerializeField]
    bool bBulletShooting = false;
    public bool bAttackAccess = false;

    //public UnityEngine.UI.Text test;


    // Start is called before the first frame update
    void Start()
    {
        direction = PLAYERDIRECT.DONTMOVE;
        PlayerSr = GetComponent<SpriteRenderer>();
        PlayerRig = GetComponent<Rigidbody2D>();
        fMoveSpeed = 5.0f;
        nPlayerHp = 100;
        bMoveAccess = true;
        bDmgAccess = true;
    }

    // Update is called once per frame
    void Update()
    {
        //test.text = SGameMng.I.FindMobList[0].ToString();
        //test1.text = SGameMng.I.FindMobList[1].ToString();
        PlayerState();
        if (!SGameMng.I.bMobileOn)
        {
            Move();
            PlayerSkill();
        }
        else
        {
            getKey();
            movement();
            rotation();
            PlayerSkill();
            MobileRollinSkill();

            if (bAttackAccess)
            {
                Attack();
            }
        }
        if (Time.time > fBulletDelay + 0.1f)                                                                                // 0.1f부분을 변수화해서 총알 딜레이 설정
        {
            bBulletShooting = false;
        }
    }


    void PlayerState()
    {
        if (nPlayerHp > 0)
        {
            if (!SGameMng.I.bMobileOn)
                WeaponRot();
        }
        else
        {
            bPlayerDie = true;
            Debug.Log("플레이어 사망");
        }

        if (bPlayerDie)
        {
            bMoveAccess = false;
            bDmgAccess = false;
        }
    }

    void getKey()
    {
        _MoveVec = new Vector3(CnControls.CnInputManager.GetAxis("Horizontal"), CnControls.CnInputManager.GetAxis("Vertical"));
        _RotVec = new Vector3(CnControls.CnInputManager.GetAxis("RotateX"), CnControls.CnInputManager.GetAxis("RotateY"));
    }

    void movement()
    {
        if (bMoveAccess)
            PlayerRig.velocity = _MoveVec * fMoveSpeed;
    }

    void rotation()
    {
        if (_MoveVec.Equals(Vector3.zero))
            return;

        fGunRot = (Mathf.Atan2(_MoveVec.y, _MoveVec.x) - correction) * Mathf.Rad2Deg;
        if (bMoveAccess)
            GunParentTr.rotation = Quaternion.Euler(0f, 0f, fGunRot - 90f);
    }

    void Move()
    {
        if (bMoveAccess)
        {
            _PlayerMoveVec = Vector2.zero;
            if (Input.GetKey(KeyCode.W))
            {
                direction = PLAYERDIRECT.UP;
                _PlayerMoveVec.y += fMoveSpeed;
            }
            if (Input.GetKey(KeyCode.S))
            {
                direction = PLAYERDIRECT.DOWN;
                _PlayerMoveVec.y -= fMoveSpeed;
            }
            if (Input.GetKey(KeyCode.A))
            {
                direction = PLAYERDIRECT.LEFT;
                PlayerSr.flipX = false;
                _PlayerMoveVec.x -= fMoveSpeed;
            }
            if (Input.GetKey(KeyCode.D))
            {
                direction = PLAYERDIRECT.RIGHT;
                PlayerSr.flipX = true;
                _PlayerMoveVec.x += fMoveSpeed;
            }
            PlayerRig.velocity = _PlayerMoveVec;
        }
        if (Input.GetMouseButton(0) && !bPlayerDie)
        {
            Attack();
        }
    }
    void WeaponRot()
    {
        Vector3 mPosition = Input.mousePosition;
        Vector3 oPosition = transform.position;

        mPosition.z = oPosition.z - Camera.main.transform.position.z;

        Vector3 target = Camera.main.ScreenToWorldPoint(mPosition);

        float dy = target.y - oPosition.y;
        float dx = target.x - oPosition.x;
        _fRotateDegree = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;

        GunParentTr.rotation = Quaternion.Euler(0f, 0f, _fRotateDegree - 180f);                                     // 총 위치 회전(마우스 방향) 추후에 고정 몬스터 방향으로 변환

        //if (!bBulletDirect)
        //{
        //    transform.rotation = Quaternion.Euler(0f, 0f, _fRotateDegree);
        //}
    }
    void PlayerSkill()
    {
        if (!SGameMng.I.bMobileOn)
        {
            if (bMoveAccess && !bRollin)
            {
                if (Input.GetKeyDown(KeyCode.R))
                {
                    StartCoroutine(Rollin());
                }
            }
            if (bRollin)
                RollinDirect();
        }

    }

    void Attack()
    {
        if (!bBulletShooting)
        {
            fBulletDelay = Time.time;

            if (!SGameMng.I.bMobileOn)
                Instantiate(BulletPre, GunTr.position, Quaternion.Euler(0f, 0f, _fRotateDegree - 90f));
            else
                Instantiate(BulletPre, GunTr.position, Quaternion.Euler(0f, 0f, fGunRot));
            bBulletShooting = true;
        }


    }

    IEnumerator MobileRollin()
    {
        SaveMoveVec = _MoveVec;
        bMoveAccess = false;
        PlayerRig.velocity = Vector2.zero;
        bRollin = true;
        fMoveSpeed = 0.0f;
        yield return new WaitForSeconds(1.0f);
        bMoveAccess = true;
        bRollin = false;
        fMoveSpeed = 5.0f;
    }

    IEnumerator Rollin()
    {
        bMoveAccess = false;
        PlayerRig.velocity = Vector2.zero;
        bRollin = true;
        fMoveSpeed = 0.0f;

        yield return new WaitForSeconds(1.0f);
        bMoveAccess = true;
        bRollin = false;
        fMoveSpeed = 5.0f;
    }

    void MobileRollinSkill()
    {
        if (bRollin)
        {
            _PlayerMoveVec.x += SaveMoveVec.x * 2.0f * Time.deltaTime;
            _PlayerMoveVec.y += SaveMoveVec.y * 2.0f * Time.deltaTime;
        }
    }

    void RollinDirect()
    {
        switch (direction)
        {
            case PLAYERDIRECT.DONTMOVE:
                transform.localPosition = Vector2.Lerp(transform.localPosition, new Vector2(transform.localPosition.x - 1.0f, transform.localPosition.y), 10.0f * Time.deltaTime);
                //transform.localPosition = new Vector2(transform.localPosition.x + 0.005f, transform.localPosition.y);
                break;

            case PLAYERDIRECT.LEFT:
                transform.localPosition = Vector2.Lerp(transform.localPosition, new Vector2(transform.localPosition.x - 1.0f, transform.localPosition.y), 5.0f * Time.deltaTime);
                //transform.localPosition = new Vector2(transform.localPosition.x - 0.005f, transform.localPosition.y);
                //_PlayerMoveVec.x -= fMoveSpeed * 2.0f * Time.deltaTime;
                break;

            case PLAYERDIRECT.RIGHT:
                transform.localPosition = Vector2.Lerp(transform.localPosition, new Vector2(transform.localPosition.x + 1.0f, transform.localPosition.y), 5.0f * Time.deltaTime);
                //transform.localPosition = new Vector2(transform.localPosition.x + 0.005f, transform.localPosition.y);
                //_PlayerMoveVec.x += fMoveSpeed * 2.0f * Time.deltaTime;
                break;

            case PLAYERDIRECT.DOWN:
                transform.localPosition = Vector2.Lerp(transform.localPosition, new Vector2(transform.localPosition.x, transform.localPosition.y - 1.0f), 3.5f * Time.deltaTime);
                //transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y - 0.005f);
                //_PlayerMoveVec.y -= fMoveSpeed * 2.0f * Time.deltaTime;
                break;

            case PLAYERDIRECT.UP:
                transform.localPosition = Vector2.Lerp(transform.localPosition, new Vector2(transform.localPosition.x, transform.localPosition.y + 1.0f), 3.5f * Time.deltaTime);
                //transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y + 0.005f);
                //_PlayerMoveVec.y += fMoveSpeed * 2.0f * Time.deltaTime;
                break;
        }
        //PlayerRig.velocity = _PlayerMoveVec;
    }

    IEnumerator DamageCtrl()
    {
        PlayerSr.color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 125 / 255f);                 // 피격시 플레이어의 스프라이트 알파값 조정(잠시 무적이라는 의미 *임시임)
        bDmgAccess = false;
        yield return new WaitForSeconds(1.5f);
        PlayerSr.color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f);
        bDmgAccess = true;
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        if (col.transform.CompareTag("Monster"))
        {
            if (bDmgAccess)
            {
                StartCoroutine(DamageCtrl());
                nPlayerHp -= 10;
            }
        }
        else if (col.transform.CompareTag("Door"))
        {

        }
    }
}