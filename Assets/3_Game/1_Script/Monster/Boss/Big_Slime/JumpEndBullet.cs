using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpEndBullet : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * 15.0f * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

}
