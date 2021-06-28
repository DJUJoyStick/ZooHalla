using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    float fBulletSpeed;
    float fBulletDegree;

    void Start()
    {
        float dy = SGameMng.I.PlayerSc.transform.position.y - transform.position.y;
        float dx = SGameMng.I.PlayerSc.transform.position.x - transform.position.x;
        fBulletDegree = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.AngleAxis(fBulletDegree - 90f, Vector3.forward);
        fBulletSpeed = 15.0f;
        StartCoroutine(DestroyBullet());
    }

    void Update()
    {
        BulletMove();
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
            //SGameMng.I.PlayerSc._nPlayerHp -= ;
            Destroy(gameObject);
        }
        if (col.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
