using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigSlimeBullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SGameMng.I.BulletDirectToPlayer(transform);
        StartCoroutine(SGameMng.I.BulletDestroy(gameObject));
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * 15.0f * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
