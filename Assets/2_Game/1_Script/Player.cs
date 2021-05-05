using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    public GameObject BulletPre;

    Rigidbody2D PlayerRig;

    Vector2 _PlayerMoveVec;

    public float fBulletDirect;
    float fMoveSpeed;
    float _fRotateDegree;
    float fBulletDelay;

    PLAYERDIRECT direction;

    public bool bBulletDirect = false;
    bool bMoveAccess = false;
    bool bRollin = false;
    bool bBulletShooting = false;


    // Start is called before the first frame update
    void Start()
    {
        direction = PLAYERDIRECT.DONTMOVE;
        PlayerRig = GetComponent<Rigidbody2D>();
        fMoveSpeed = 5.0f;
        bMoveAccess = true;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        PlayerSkill();
        //Attack();
        PlayerRotate();
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
                _PlayerMoveVec.x -= fMoveSpeed;
            }
            if (Input.GetKey(KeyCode.D))
            {
                direction = PLAYERDIRECT.RIGHT;
                _PlayerMoveVec.x += fMoveSpeed;
            }
            PlayerRig.velocity = _PlayerMoveVec;
        }
        if (Input.GetMouseButton(0))
        {
            //Instantiate(BulletPre, transform.localPosition, Quaternion.Euler(0f, 0f, _fRotateDegree - 90f));
            Attack();
        }
    }
    void PlayerRotate()
    {
        Vector3 mPosition = Input.mousePosition;
        Vector3 oPosition = transform.position;

        mPosition.z = oPosition.z - Camera.main.transform.position.z;

        Vector3 target = Camera.main.ScreenToWorldPoint(mPosition);

        float dy = target.y - oPosition.y;
        float dx = target.x - oPosition.x;
        _fRotateDegree = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;

        if (!bBulletDirect)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, _fRotateDegree);
        }
    }
    void PlayerSkill()
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

    void Attack()
    {
        if (!bBulletShooting)
        {
            fBulletDelay = Time.time;
            if (SGameMng.I.NearEnemyTr.Equals(null))
            {
                Instantiate(BulletPre, transform.localPosition, Quaternion.Euler(0f, 0f, _fRotateDegree - 90f));
            }
            else
            {
                Instantiate(BulletPre, transform.localPosition, Quaternion.identity);
            }
            bBulletShooting = true;
        }
        if (Time.time > fBulletDelay + 0.1f)                                                                                // 0.1f부분을 변수화해서 총알 딜레이 설정
        {
            bBulletShooting = false;
        }

    }

    IEnumerator Rollin()
    {
        bMoveAccess = false;
        PlayerRig.velocity = Vector2.zero;
        bRollin = true;

        yield return new WaitForSeconds(1.0f);
        bMoveAccess = true;
        bRollin = false;
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
                //transform.localPosition = Vector2.Lerp(transform.localPosition, new Vector2(transform.localPosition.x - 1.0f, transform.localPosition.y), 2.0f * Time.deltaTime);
                //transform.localPosition = new Vector2(transform.localPosition.x - 0.005f, transform.localPosition.y);
                _PlayerMoveVec.x -= fMoveSpeed * 2.0f * Time.deltaTime;
                break;

            case PLAYERDIRECT.RIGHT:
                //transform.localPosition = Vector2.Lerp(transform.localPosition, new Vector2(transform.localPosition.x + 1.0f, transform.localPosition.y), 2.0f * Time.deltaTime);
                //transform.localPosition = new Vector2(transform.localPosition.x + 0.005f, transform.localPosition.y);
                _PlayerMoveVec.x += fMoveSpeed * 2.0f * Time.deltaTime;
                break;

            case PLAYERDIRECT.DOWN:
                //transform.localPosition = Vector2.Lerp(transform.localPosition, new Vector2(transform.localPosition.x, transform.localPosition.y - 1.0f), 2.0f * Time.deltaTime);
                //transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y - 0.005f);
                _PlayerMoveVec.y -= fMoveSpeed * 2.0f * Time.deltaTime;
                break;

            case PLAYERDIRECT.UP:
                //transform.localPosition = Vector2.Lerp(transform.localPosition, new Vector2(transform.localPosition.x, transform.localPosition.y + 1.0f), 2.0f * Time.deltaTime);
                //transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y + 0.005f);
                _PlayerMoveVec.y += fMoveSpeed * 2.0f * Time.deltaTime;
                break;
        }
        PlayerRig.velocity = _PlayerMoveVec;
    }


}