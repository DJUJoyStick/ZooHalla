using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperHunter : MonoBehaviour
{
    public GameObject[] SniperBulletPre = new GameObject[2];
    public GameObject HunterTrapPre;

    public Transform[] SniperSummonTr = new Transform[3];

    public SpriteRenderer[] StateSr = new SpriteRenderer[4];
    public SpriteRenderer SniperSr;

    public UnityEngine.UI.Image BossHpBarImg;

    public Animator SniperAnime;

    public float fMoveSpeed;

    public int nHp;
    public int nRand;
    public int nAttackChildCount;
    public int[] nStateCount = new int[4];
    public int[] nPaternCount = new int[2];

    public bool[] bPatern = new bool[4];
    public bool[] bStateOn = new bool[4];
    public bool bDie = false;

    void Start()
    {
        SGameMng.I.BossTr = transform;
        BossHpBarImg = GameObject.Find("BossHpBar").GetComponent<UnityEngine.UI.Image>();
        nHp = 200;
        fMoveSpeed = 4.0f;
        Invoke("SelectPatern", Random.Range(10, 20));
        BossHpBarImg.transform.localPosition = new Vector2(0.0f, 300.0f);
        SGameMng.I.nMonCount++;
    }

    void Update()
    {
        if (!bDie)
            Patern();
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        if (!bDie && !SGameMng.I.PlayerSc._bPlayerDie)
            transform.position = Vector2.MoveTowards(transform.position, SGameMng.I.PlayerSc.transform.position, fMoveSpeed * Time.deltaTime);
    }
    void State()
    {
        if (SGameMng.I.PlayerSc.transform.position.x > transform.position.x)
            SniperSr.flipX = true;
        else if (SGameMng.I.PlayerSc.transform.position.x < transform.position.x)
            SniperSr.flipX = false;
        if (nHp <= 0)
        {
            bDie = true;
            BossHpBarImg.transform.localPosition = new Vector2(1000.0f, 300.0f);
            for (int i = 0; i < StateSr.Length; i++)
                StateSr[i].gameObject.SetActive(false);
        }
        if (Vector2.Distance(transform.position, SGameMng.I.PlayerSc.transform.position) <= 7.0f)
        {
            SGameMng.I.bBossTarget = true;
        }
        else
            SGameMng.I.bBossTarget = false;
    }

    void SelectPatern()
    {
        nRand = Random.Range(10, 20);
        int rand = Random.Range(1, 5);
        if (rand.Equals(1))
            bPatern[0] = true;
        else if (rand.Equals(2))
            bPatern[1] = true;
        else if (rand.Equals(3))
            bPatern[2] = true;
        else if (rand.Equals(4))
            bPatern[3] = true;
        if (!bDie)
            Invoke("SelectPatern", (float)nRand);
    }

    void Patern()
    {
        if (bPatern[0])
        {
            Invoke("StrongAttack", 1.0f);
            SniperAnime.SetBool("Attack", true);
            nPaternCount[0] = 4;
            bPatern[0] = false;
            fMoveSpeed = 0.0f;
        }
        else if (bPatern[1])
        {
            StartCoroutine(Hidden());
            SniperAnime.SetBool("Hidden", true);
            fMoveSpeed = 0.0f;
            bPatern[1] = false;
        }
        else if (bPatern[2])
        {
            Invoke("SummonTrap", 0.5f);
            SniperAnime.SetBool("Summon", true);
            fMoveSpeed = 0.0f;
            bPatern[2] = false;
        }
        else if (bPatern[3])
        {
            Invoke("Attack", 0.5f);
            nPaternCount[1] = 1;
            nAttackChildCount = 3;
            SniperAnime.SetBool("Attack", true);
            fMoveSpeed = 0.0f;
            bPatern[3] = false;
        }
    }

    void StrongAttack()
    {
        Instantiate(SniperBulletPre[0], transform.position, Quaternion.Euler(Vector3.zero));
        if (!nPaternCount[0].Equals(0))
        {
            nPaternCount[0]--;
            Invoke("StrongAttack", 1.0f);
        }
        else if (nPaternCount[0].Equals(0))
        {
            SniperAnime.SetBool("Attack", false);
            fMoveSpeed = 4.0f;
        }
    }

    void HiddenAniEnd()
    {
        fMoveSpeed = 4.0f;
    }

    IEnumerator Hidden()
    {
        yield return new WaitForSeconds(5.0f);
        SniperSr.color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f);
        SniperAnime.SetBool("Hidden", false);
    }

    void SummonTrap()
    {
        for (int i = 0; i < SniperSummonTr.Length; i++)
        {
            Instantiate(HunterTrapPre, SniperSummonTr[i].position, Quaternion.Euler(Vector3.zero));
        }
        Invoke("SummonAni", 1);
    }

    void SummonAni()
    {
        fMoveSpeed = 4.0f;
        SniperAnime.SetBool("Summon", false);
    }

    void Attack()
    {
        AttackChild();
        if (!nPaternCount[1].Equals(0))
        {
            nPaternCount[1]--;
            Invoke("Attack", 2.0f);
        }
        else if (nPaternCount[1].Equals(0))
        {
            SniperAnime.SetBool("Attack", false);
            fMoveSpeed = 4.0f;
        }
    }

    void AttackChild()
    {
        Instantiate(SniperBulletPre[1], transform.position, Quaternion.Euler(Vector3.zero));
        if (!nAttackChildCount.Equals(0))
        {
            nAttackChildCount--;
            Invoke("AttackChild", 0.25f);
        }
        else if (nAttackChildCount.Equals(0))
            nAttackChildCount = 3;
    }

    public void DebuffList(STATE_ELIMET state, int time = 0)
    {
        switch (state)
        {
            case STATE_ELIMET.NONE:
                break;
            case STATE_ELIMET.BURN:
                StateSr[0].gameObject.SetActive(true);
                nStateCount[0] = time;
                bStateOn[0] = true;
                Invoke("Burn", 1);
                break;
            case STATE_ELIMET.POISON:
                StateSr[2].gameObject.SetActive(true);
                nStateCount[2] = time;
                bStateOn[2] = true;
                Invoke("Poison", 1);
                break;
            case STATE_ELIMET.SLOW:
                StateSr[3].gameObject.SetActive(true);
                break;
            case STATE_ELIMET.THUNDER:
                StateSr[3].gameObject.SetActive(true);
                nStateCount[3] = time;
                Invoke("Thunder", 1);
                break;
        }
    }

    void Burn()
    {
        nHp--;
        if (!nStateCount[0].Equals(0))
        {
            nStateCount[0]--;
            Invoke("Burn", 1);
        }
        else if (nStateCount[0].Equals(0))
        {
            StateSr[0].gameObject.SetActive(false);
            bStateOn[0] = false;
        }
    }

    void Poison()
    {
        nHp--;
        if (!nStateCount[2].Equals(0))
        {
            nStateCount[2]--;
            Invoke("Poison", 1);
        }
        else if (nStateCount[2].Equals(0))
        {
            StateSr[2].gameObject.SetActive(false);
            bStateOn[2] = false;
        }
    }

    void Slow()
    {

    }
    void Thunder()
    {
        nHp--;
        if (!nStateCount[3].Equals(0))
        {
            nStateCount[3]--;
            Invoke("Thunder", 1);
        }
        else if (nStateCount[3].Equals(0))
            StateSr[3].gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Bullet"))
        {

            if (SGameMng.I.PlayerSc._PlayerWeapon.Equals(ItemPrivateNum.FIRE_BIRD))
            {
                if (!bStateOn[0])
                    DebuffList(STATE_ELIMET.BURN, 5);
                else
                    nStateCount[0] += 5;
            }
            else if (SGameMng.I.PlayerSc._PlayerWeapon.Equals(ItemPrivateNum.POISON_NEEDLE))
            {
                if (!bStateOn[2])
                    DebuffList(STATE_ELIMET.POISON, 5);
                else
                    nStateCount[2] += 5;
            }
            if (SGameMng.I.PlayerType.Equals(PLAYERTYPE.WOLF))
                nHp -= SGameMng.I.PlayerSc._nFinalDmg;
            else
                nHp -= SGameMng.I.PlayerSc._nWeaponDmg;
        }
        Destroy(col.gameObject);

    }

}
