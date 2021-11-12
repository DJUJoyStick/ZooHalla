using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueenSpider : MonoBehaviour
{
    public GameObject[] QueenSpiderBulletGams = new GameObject[3];

    public SpriteRenderer QueenSpiderSr;
    public SpriteRenderer[] StateSr = new SpriteRenderer[4];

    public UnityEngine.UI.Image BossHpBarImg;

    public Animator QueenSpiderAnime;

    float fSaveY;

    public int nHp;
    public int nDmg;
    public int[] nPaternCount = new int[4];
    public int nGunCount;
    public int[] nStateCount = new int[4];

    public bool[] bPatern = new bool[4];
    public bool bDmgAccess = false;
    public bool bAttackAccess = false;
    public bool[] bStateOn = new bool[4];
    bool bDie = false;

    // Start is called before the first frame update
    void Start()
    {
        SGameMng.I.BossTr = transform;
        SGameMng.I.bBossStage = true;
        BossHpBarImg = GameObject.Find("BossHpBar").GetComponent<UnityEngine.UI.Image>();
        QueenSpiderSr = GetComponent<SpriteRenderer>();
        QueenSpiderAnime = GetComponent<Animator>();
        nHp = 4000;
        nDmg = 1;
        bDmgAccess = true;
        bAttackAccess = true;
        fSaveY = transform.position.y;
        for (int i = 0; i < bPatern.Length; i++)
            bPatern[i] = false;
        Invoke("SelectPatern", 5);
        BossHpBarImg.transform.localPosition = new Vector2(0.0f, 300.0f);
        SGameMng.I.nMonCount++;
    }

    // Update is called once per frame
    void Update()
    {
        if (!bDie)
        {
            Patern();
            State();
        }
        else
        {
            SGameMng.I.nGameOver = 1;
            Destroy(gameObject);
        }

    }

    void State()
    {
        if (!nPaternCount[0].Equals(0))
        {
            if (transform.position.y < fSaveY + 3.0f)
                transform.Translate(Vector2.up * 3.0f * Time.deltaTime);
        }
        if (nPaternCount[0].Equals(0))
        {
            if (fSaveY < transform.position.y)
                transform.Translate(Vector2.down * 3.0f * Time.deltaTime);
        }
        if (SGameMng.I.PlayerSc.transform.position.x > transform.position.x)
            QueenSpiderSr.flipX = true;
        else if (SGameMng.I.PlayerSc.transform.position.x < transform.position.x)
            QueenSpiderSr.flipX = false;
        BossHpBarImg.fillAmount = (float)nHp / 4000;
        if (nHp <= 0)
        {
            bDie = true;
            BossHpBarImg.transform.localPosition = new Vector2(1000.0f, 300.0f);
            for (int i = 0; i < StateSr.Length; i++)
                StateSr[i].gameObject.SetActive(false);
            SGameMng.I.bBossStage = false;
            SGameMng.I.bBossTarget = false;
        }
        SGameMng.I.fBossDis = Vector2.Distance(transform.position, SGameMng.I.PlayerSc.transform.position);
        if (!bDie)
        {
            if (SGameMng.I.fBossDis <= 7.0f)
            {
                SGameMng.I.bBossTarget = true;
            }
            else
                SGameMng.I.bBossTarget = false;
        }
        if (SGameMng.I.PlayerType.Equals(PLAYERTYPE.RAT))
        {
            if(SGameMng.I.PlayerSc._bSkillOn)
            {
                DebuffList(STATE_ELIMET.POISON, 9);
            }
        }
    }

    void SelectPatern()
    {
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
            Invoke("SelectPatern", 5);
    }

    void Patern()
    {
        if (bPatern[0])
        {
            Invoke("RecoveryHealth", 1.0f);
            nPaternCount[0] = 2;
            QueenSpiderAnime.SetBool("Heal", true);
            bDmgAccess = false;
            bAttackAccess = false;
            bPatern[0] = false;
        }
        else if (bPatern[1])
        {
            Invoke("InjectionWeb", 0.5f);
            nPaternCount[1] = 4;
            QueenSpiderAnime.SetBool("Attack", true);
            bPatern[1] = false;
        }
        else if (bPatern[2])
        {
            Invoke("FanShapeAttack", 0.5f);
            nPaternCount[2] = 1;
            QueenSpiderAnime.SetBool("Skill", true);
            bPatern[2] = false;
        }
        else if (bPatern[3])
        {
            Invoke("MachineGunWeb", 0.5f);
            nPaternCount[3] = 2;
            QueenSpiderAnime.SetBool("Attack", true);
            nGunCount = 2;
            bPatern[3] = false;
        }
    }

    void RecoveryHealth()
    {
        nHp += 7;
        if (nHp >= 4000)
            nHp = 4000;

        if (!nPaternCount[0].Equals(0))
        {
            nPaternCount[0]--;
            Invoke("RecoveryHealth", 1f);
        }
        else if (nPaternCount[0].Equals(0))
        {
            bDmgAccess = true;
            QueenSpiderAnime.SetBool("Heal", false);
        }
    }

    void InjectionWeb()
    {
        Instantiate(QueenSpiderBulletGams[1], transform.position, Quaternion.Euler(new Vector3(0.0f, 0.0f, 0.0f)));
        if (!nPaternCount[1].Equals(0))
        {
            nPaternCount[1]--;
            Invoke("InjectionWeb", 0.5f);
        }
        else if (nPaternCount[1].Equals(0))
            QueenSpiderAnime.SetBool("Attack", false);
    }

    void FanShapeAttack()
    {
        Instantiate(QueenSpiderBulletGams[2], transform.position, Quaternion.Euler(new Vector3(0.0f, 0.0f, 0.0f)));
        if (!nPaternCount[2].Equals(0))
        {
            nPaternCount[2]--;
            Invoke("FanShapeAttack", 0.5f);
        }
        else if (nPaternCount[2].Equals(0))
            QueenSpiderAnime.SetBool("Skill", false);
    }

    void MachineGunWeb()
    {
        MachineWebChild();
        if (!nPaternCount[3].Equals(0))
        {
            nPaternCount[3]--;
            Invoke("MachineGunWeb", 3.0f);
        }
        else if (nPaternCount[3].Equals(0))
            QueenSpiderAnime.SetBool("Attack", false);
    }

    void MachineWebChild()
    {
        Instantiate(QueenSpiderBulletGams[0], transform.position, Quaternion.Euler(new Vector3(0.0f, 0.0f, 0.0f)));
        if (!nGunCount.Equals(0))
        {
            nGunCount--;
            Invoke("MachineWebChild", 0.3f);
        }
        else if (nGunCount.Equals(0))
            nGunCount = 2;
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
                //Invoke("Slow", 1);
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
            if (bDmgAccess)
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

}
