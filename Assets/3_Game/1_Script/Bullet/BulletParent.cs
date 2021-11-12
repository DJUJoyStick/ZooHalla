using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletParent : MonoBehaviour
{
    public EnemyBullet[] ChildBulletSc;
    public Monster ParentMonSc;
    float fBulletDegree;

    // Start is called before the first frame update
    void Start()
    {
        ParentMonSc = transform.parent.GetComponent<Monster>();
        if (!ParentMonSc.bStateOn[1])
        {
            float dy = SGameMng.I.PlayerSc.transform.position.y - transform.position.y;
            float dx = SGameMng.I.PlayerSc.transform.position.x - transform.position.x;
            fBulletDegree = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.AngleAxis(fBulletDegree - 90f, Vector3.forward);
        }
        else
        {
            float dy = SGameMng.I.TargetEnemyTr.position.y - transform.position.y;
            float dx = SGameMng.I.TargetEnemyTr.position.x - transform.position.x;
            fBulletDegree = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.AngleAxis(fBulletDegree - 90f, Vector3.forward);
        }
        for(int i = 0; i< ChildBulletSc.Length; i++)
        {
            ChildBulletSc[i].BulletOwnerMonSc = ParentMonSc;
            ChildBulletSc[i].nBulletDmg = ParentMonSc.nMonsterDmg;
            BulletColorSetting(ChildBulletSc[i]);
        }
        transform.parent = GameObject.Find("Game").transform;
        StartCoroutine(DestroySelf());
    }


    void BulletColorSetting(EnemyBullet childbullet)
    {
        switch (transform.parent.name)
        {
            case "Viper":
                childbullet.MonBulletSr.color = new Color(175 / 255f, 0 / 255f, 255 / 255f);
                break;

            case "Big_Carnivorous_Plant":
                childbullet.MonBulletSr.color = new Color(255 / 255f, 0 / 255f, 0 / 255f);
                break;

            case "Spirit":
                childbullet.MonBulletSr.color = new Color(255 / 255f, 0 / 255f, 255 / 255f);
                break;
        }

    }

    IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(5.0f);
        Destroy(gameObject);
    }
}
