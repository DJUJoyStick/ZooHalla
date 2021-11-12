using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public Monster BulletOwnerMonSc;
    public SpriteRenderer MonBulletSr;

    float fBulletSpeed;
    float fBulletDegree;

    public int nBulletType;
    public int nBulletDmg;

    void Start()
    {
        if (nBulletType.Equals(1))
        {
            BulletOwnerMonSc = transform.parent.GetComponent<Monster>();
            nBulletDmg = BulletOwnerMonSc.nMonsterDmg;
            BulletDmgSetting();

            if (!BulletOwnerMonSc.bStateOn[1])
            {
                SGameMng.I.BulletDirectToPlayer(transform);
            }
            else
            {
                float dy = SGameMng.I.TargetEnemyTr.position.y - transform.position.y;
                float dx = SGameMng.I.TargetEnemyTr.position.x - transform.position.x;
                fBulletDegree = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;

                transform.rotation = Quaternion.AngleAxis(fBulletDegree - 90f, Vector3.forward);
            }
        }
        
        fBulletSpeed = 6.0f;
        StartCoroutine(DestroyBullet());
    }

    void Update()
    {
        BulletMove();
    }

    void BulletDmgSetting()
    {
        switch (transform.parent.name)
        {
            case "Viper":
                MonBulletSr.color = new Color(175 / 255f, 0 / 255f, 255 / 255f);
                break;

            case "Big_Carnivorous_Plant":
                MonBulletSr.color = new Color(255 / 255f, 0 / 255f, 0 / 255f);
                break;

            case "Spirit":
                MonBulletSr.color = new Color(255 / 255f, 0 / 255f, 255 / 255f);
                break;
        }
        transform.parent = GameObject.Find("Game").transform;
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

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if (!BulletOwnerMonSc.bStateOn[1])
            {
                if (SGameMng.I.PlayerSc._bDmgAccess)
                    SGameMng.I.PlayerSc._nPlayerHp -= nBulletDmg;
                Destroy(gameObject);
            }
        }

        if (col.CompareTag("Monster"))
        {
            if (BulletOwnerMonSc.bStateOn[1])
            {
                Monster colmon = col.GetComponent<Monster>();
                if (!colmon.Equals(BulletOwnerMonSc))
                {
                    colmon.nMonsterHp -= nBulletDmg;
                    Destroy(gameObject);
                }
            }

        }

        if (col.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
