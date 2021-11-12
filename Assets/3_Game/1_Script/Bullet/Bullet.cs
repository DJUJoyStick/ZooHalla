using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public SpriteRenderer BulletSr;
    public CircleCollider2D BulletCc;
    float fBulletSpeed;
    float fBulletDegree;

    void Start()
    {
        if (SGameMng.I.PlayerSc._PlayerWeapon.Equals(ItemPrivateNum.ENDOFCENTURYGAUNTLET))
            if (SGameMng.I.PlayerSc._nBulletAmount.Equals(0))
            {
                SGameMng.I.PlayerSc._FingerSnap();
                Destroy(gameObject);
            }
        BulletInit();
        if (!SGameMng.I.bBossTarget)
        {
            if (!SGameMng.I.TargetEnemyTr.Equals(null))
            {
                float dy = SGameMng.I.TargetEnemyTr.position.y - transform.position.y;
                float dx = SGameMng.I.TargetEnemyTr.position.x - transform.position.x;
                fBulletDegree = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;

                if (SGameMng.I.TargetEnemySc.bFindMobOn)
                {
                    transform.rotation = Quaternion.AngleAxis(fBulletDegree - 90f, Vector3.forward);
                }
            }
        }
        else
        {
            if (!SGameMng.I.BossTr.Equals(null))
            {
                float dy = SGameMng.I.BossTr.position.y - transform.position.y;
                float dx = SGameMng.I.BossTr.position.x - transform.position.x;
                fBulletDegree = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;

                transform.rotation = Quaternion.AngleAxis(fBulletDegree - 90f, Vector3.forward);

            }
        }

        fBulletSpeed = 15.0f;
        StartCoroutine(DestroyBullet());
    }

    void Update()
    {
        BulletMove();
    }

    void BulletInit()
    {
        switch (SGameMng.I.PlayerSc._PlayerWeapon)
        {
            case ItemPrivateNum.TFFGUN:
                if (SGameMng.I.PlayerSc._nBulletAmount.Equals(5) || SGameMng.I.PlayerSc._nBulletAmount.Equals(2))
                {
                    BulletSr.sprite = SGameMng.I.PlayerBulletSp[0];
                    transform.localScale = new Vector2(3.0f, 3.0f);
                    BulletCc.radius = 0.05f;
                }
                else if (SGameMng.I.PlayerSc._nBulletAmount.Equals(4) || SGameMng.I.PlayerSc._nBulletAmount.Equals(1))
                {
                    BulletSr.sprite = SGameMng.I.PlayerBulletSp[1];
                    transform.localScale = new Vector2(3.0f, 3.0f);
                    BulletCc.radius = 0.05f;
                }
                else if (SGameMng.I.PlayerSc._nBulletAmount.Equals(3) || SGameMng.I.PlayerSc._nBulletAmount.Equals(0))
                {
                    BulletSr.sprite = SGameMng.I.PlayerBulletSp[2];
                    transform.localScale = new Vector2(3.0f, 3.0f);
                    BulletCc.radius = 0.05f;
                }
                break;
            case ItemPrivateNum.FLAXGUN:
                BulletSr.sprite = SGameMng.I.PlayerBulletSp[3];
                transform.localScale = new Vector2(3.0f, 3.0f);
                BulletCc.radius = 0.05f;
                break;
            case ItemPrivateNum.WOOD_BOW:
                BulletSr.sprite = SGameMng.I.PlayerBulletSp[4];
                break;
            case ItemPrivateNum.FIRE_BIRD:
                BulletSr.sprite = SGameMng.I.PlayerBulletSp[5];
                break;
        }
    }

    void BulletMove()
    {
        transform.Translate(Vector2.up * fBulletSpeed * Time.deltaTime);
    }

    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(5.0f);
        Destroy(gameObject);
    }

    void TffDmg(Monster MonSc)
    {
        if (SGameMng.I.PlayerSc._nBulletAmount.Equals(5) || SGameMng.I.PlayerSc._nBulletAmount.Equals(2))
        {
            MonSc.nMonsterHp -= 3;
        }
        else if (SGameMng.I.PlayerSc._nBulletAmount.Equals(4) || SGameMng.I.PlayerSc._nBulletAmount.Equals(1))
        {
            MonSc.nMonsterHp -= 6;
        }
        else if (SGameMng.I.PlayerSc._nBulletAmount.Equals(3) || SGameMng.I.PlayerSc._nBulletAmount.Equals(0))
        {
            MonSc.nMonsterHp -= 9;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Monster"))
        {
            Monster HitMonsterSc = col.GetComponent<Monster>();
            if (SGameMng.I.PlayerType.Equals(PLAYERTYPE.WOLF))
            {
                if (SGameMng.I.PlayerSc._PlayerWeapon.Equals(ItemPrivateNum.TFFGUN))
                    TffDmg(HitMonsterSc);
                else
                    HitMonsterSc.nMonsterHp -= SGameMng.I.PlayerSc._nFinalDmg;
            }
            else
            {
                if (SGameMng.I.PlayerSc._PlayerWeapon.Equals(ItemPrivateNum.TFFGUN))
                    TffDmg(HitMonsterSc);
                else
                    HitMonsterSc.nMonsterHp -= SGameMng.I.PlayerSc._nWeaponDmg;
            }
            Destroy(gameObject);
        }
        if (col.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }

}
