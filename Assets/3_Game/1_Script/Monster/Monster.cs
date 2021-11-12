using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Monster : MonoBehaviour
{
    public GameObject[] MobBulletPre;
    public GameObject BabySpiderPre;

    public Monster SelfMonsterSc;

    public Animator MonsterAnime;

    public SpriteRenderer MobSp;
    public SpriteRenderer[] MobStateSr = new SpriteRenderer[4];

    public Rigidbody2D MonsterRigid;

    public MONSTERTYPE MobType;
    public MONSTERLIST MobName;

    public int nMonsterHp;
    public int nMonsterDmg;
    public int[] nStateCount = new int[5];  // 0 : 화상 1 : 매혹 2 : 독 3 : 둔화 4 : 감전
    public int nRand;
    public int nMonsterAttackType;      // 0 : 근접 1 : 한발 2 : 산탄 3 : 십자가

    public float fPlayerDis;
    public float fMonsterSpeed;
    public float fSaveSpeed;
    public float ChaseRange;
    public float AttackRange;

    public bool bMoveAccess = false;
    public bool bThisTarget = false;
    public bool bFindMobOn = false;
    public bool bDebuffOn = false;
    public bool bPosChange = false;
    public bool bRangeAttackOn = false;
    bool bRangeAttDelay = false;
    public bool[] bStateOn = new bool[5];     // 0 : 화상 1 : 매혹 2 : 독 3 : 둔화 4 : 감전

    void Start()
    {
        SelfMonsterSc = this;
        MobSp = GetComponent<SpriteRenderer>();
        MonsterRigid = GetComponent<Rigidbody2D>();
        MonsterAnime = GetComponent<Animator>();
        SGameMng.I.FindMobList.Add(this);
        MobInit(transform.name);
        nRand = Random.Range(1, 9);
        bMoveAccess = true;
        bPosChange = true;
        InvokeRepeating("MonsterInitReset", 5.0f, 5.0f);
        fPlayerDis = 10.0f;                                             // 게임이 시작되면 0으로 초기화되기에 그걸 방지하기 위함
        if (transform.name.Equals("Spider_Egg_House"))
            InvokeRepeating("SummonSpider", 5.0f, 5.0f);
        SGameMng.I.nMonCount++;
        for (int i = 0; i < nStateCount.Length; i++)
        {
            nStateCount[i] = 0;
            bStateOn[i] = false;
        }
    }

    void Update()
    {
        MonsterState();
    }

    private void FixedUpdate()
    {
        Move();
    }

    void MobInit(string name)
    {
        switch (name)
        {
            case "Viper":
                MobSetting(MONSTERTYPE.RANGED_MONSTER, MONSTERLIST.VIPER, 280, 1, 5.0f, 3.0f, 5.0f, 1);
                break;
            case "Hunter_Trap":
                MobSetting(MONSTERTYPE.MELEE_MONSTER, MONSTERLIST.HUNTER_TRAP, 280, 2, 5.0f, 0.0f, 5.0f);
                break;
            case "Big_Carnivorous_Plant":
                MobSetting(MONSTERTYPE.RANGED_MONSTER, MONSTERLIST.BIG_CARNIVOROUS_PLANT, 320, 3, 10.0f, 4.0f, 0.0f, 2);
                break;
            case "Spirit":
                MobSetting(MONSTERTYPE.RANGED_MONSTER, MONSTERLIST.STRANGE_SPIRIT, 320, 3, 6.0f, 4.0f, 5.0f, 3);
                break;
            case "Bulb":
                MobSetting(MONSTERTYPE.MELEE_MONSTER, MONSTERLIST.BULB, 160, 3, 7.0f, 0.0f, 5.0f);
                break;
            case "Poison_Slime":
                MobSetting(MONSTERTYPE.MELEE_MONSTER, MONSTERLIST.POISON_SLIME, 160, 3, 5.0f, 0.0f, 5.0f, 0);
                break;
            case "Slime":
                MobSetting(MONSTERTYPE.MELEE_MONSTER, MONSTERLIST.SLIME, 160, 3, 10.0f, 5.0f, 5.0f, 0);
                break;
            case "Ghost":
                MobSetting(MONSTERTYPE.MELEE_MONSTER, MONSTERLIST.GHOST, 160, 3, 5.0f, 0.0f, 5.0f, 0);
                break;
            case "Meteor_Golem":
                MobSetting(MONSTERTYPE.MELEE_MONSTER, MONSTERLIST.METEOR_GOLEM, 320, 3, 5.0f, 0.0f, 5.0f, 0);
                break;
            case "Moss_Golem":
                MobSetting(MONSTERTYPE.MELEE_MONSTER, MONSTERLIST.METEOR_GOLEM, 320, 3, 5.0f, 0.0f, 5.0f, 0);
                break;
            case "Spider_Egg_House":
                MobSetting(MONSTERTYPE.MELEE_MONSTER, MONSTERLIST.SPIDER_EGG_HOUSE, 160, 3, 0.0f, 0.0f, 0.0f, 0);
                break;
            case "Hornet":
                MobSetting(MONSTERTYPE.MELEE_MONSTER, MONSTERLIST.HORNET, 280, 3, 5.0f, 0.0f, 5.0f, 0);
                break;
            case "Baby_Spider":
                MobSetting(MONSTERTYPE.MELEE_MONSTER, MONSTERLIST.BABY_SPIDER, 160, 3, 5.0f, 0.0f, 3.0f, 0);
                break;
            case "Baby_Spider(Clone)":
                MobSetting(MONSTERTYPE.MELEE_MONSTER, MONSTERLIST.BABY_SPIDER, 160, 3, 5.0f, 0.0f, 3.0f, 0);
                break;
            case "Slime(Clone)":
                MobSetting(MONSTERTYPE.MELEE_MONSTER, MONSTERLIST.SLIME, 160, 3, 5.0f, 0.0f, 5.0f, 0);
                break;
            case "Hunter_Trap(Clone)":
                MobSetting(MONSTERTYPE.MELEE_MONSTER, MONSTERLIST.HUNTER_TRAP, 160, 2, 5.0f, 0.0f, 5.0f);
                break;
        }
    }

    // 근,원거리 여부, 몬스터 이름, 체력, 데미지, 속도, 추격범위, 공격범위 (디폴트 매개변수가 있으니 공격범위, 이동속도는 지정 안해줘도됨), 원거리 공격 방법
    void MobSetting(MONSTERTYPE type, MONSTERLIST name, int hp, int dmg, float chaserg, float attackrg = 0.0f, float speed = 0.0f, int attacktype = 0)
    {
        MobType = type;
        MobName = name;
        nMonsterHp = hp;
        nMonsterDmg = dmg;
        ChaseRange = chaserg;
        fMonsterSpeed = speed;
        fSaveSpeed = fMonsterSpeed;
        AttackRange = attackrg;
        nMonsterAttackType = attacktype;
    }

    void Move()
    {
        if (!bStateOn[1])
        {
            if (bMoveAccess && !SGameMng.I.PlayerSc._bPlayerDie)
            {
                if (fPlayerDis <= ChaseRange)
                {
                    transform.position = Vector2.MoveTowards(transform.position, SGameMng.I.PlayerSc.transform.position, fMonsterSpeed * Time.deltaTime);
                    if (MobType.Equals(MONSTERTYPE.RANGED_MONSTER))
                    {
                        if (fPlayerDis <= AttackRange && !bRangeAttackOn)
                            MonsterAttack();
                    }
                    if (transform.position.x > SGameMng.I.PlayerSc.transform.position.x)
                        MobSp.flipX = false;
                    else if (transform.position.x < SGameMng.I.PlayerSc.transform.position.x)
                        MobSp.flipX = true;
                }
                else
                    AutoMoving();
            }
            if (SGameMng.I.PlayerSc._bPlayerDie)
                AutoMoving();
        }
        else
        {
            if (!SGameMng.I.TargetEnemyTr.Equals(null))
            {
                transform.position = Vector2.MoveTowards(transform.position, SGameMng.I.TargetEnemyTr.position, fMonsterSpeed * Time.deltaTime);
                if (MobType.Equals(MONSTERTYPE.RANGED_MONSTER))
                {
                    if (Vector2.Distance(transform.position, SGameMng.I.TargetEnemyTr.position) <= AttackRange && !bRangeAttackOn)
                        MonsterAttack();
                }
                if (transform.position.x > SGameMng.I.TargetEnemyTr.position.x)
                    MobSp.flipX = false;
                else if (transform.position.x < SGameMng.I.TargetEnemyTr.position.x)
                    MobSp.flipX = true;
            }
        }
    }

    void AutoMoving()
    {
        switch (nRand)
        {
            case 0:
                fMonsterSpeed = 0.0f;
                break;
            case 1:
                MonsterRigid.velocity = Vector2.left * fMonsterSpeed;
                MobSp.flipX = false;
                break;
            case 2:
                MonsterRigid.velocity = Vector2.right * fMonsterSpeed;
                MobSp.flipX = true;
                break;
            case 3:
                MonsterRigid.velocity = Vector2.up * fMonsterSpeed;
                break;
            case 4:
                MonsterRigid.velocity = Vector2.down * fMonsterSpeed;
                break;
            case 5:
                MonsterRigid.velocity = new Vector2(MonsterRigid.velocity.x - 1, MonsterRigid.velocity.y + 1).normalized * fMonsterSpeed;
                MobSp.flipX = false;
                break;
            case 6:
                MonsterRigid.velocity = new Vector2(MonsterRigid.velocity.x + 1, MonsterRigid.velocity.y + 1).normalized * fMonsterSpeed;
                MobSp.flipX = true;
                break;
            case 7:
                MonsterRigid.velocity = new Vector2(MonsterRigid.velocity.x + 1, MonsterRigid.velocity.y - 1).normalized * fMonsterSpeed;
                MobSp.flipX = true;
                break;
            case 8:
                MonsterRigid.velocity = new Vector2(MonsterRigid.velocity.x - 1, MonsterRigid.velocity.y - 1).normalized * fMonsterSpeed;
                MobSp.flipX = false;
                break;
        }
    }

    void MonsterAttack()
    {
        bRangeAttackOn = true;
        if (bRangeAttackOn)
        {
            MonsterAnime.SetBool("Attack", true);
            if (nMonsterAttackType.Equals(1))
                Instantiate(MobBulletPre[0], transform.position, Quaternion.Euler(0f, 0f, 0f)).transform.parent = transform;
            else if (nMonsterAttackType.Equals(2))
                Instantiate(MobBulletPre[1], transform.position, Quaternion.Euler(0f, 0f, 0f)).transform.parent = transform;
            if (nMonsterAttackType.Equals(3))
                Instantiate(MobBulletPre[2], transform.position, Quaternion.Euler(0f, 0f, 0f)).transform.parent = transform;
            if (!bRangeAttDelay)
                StartCoroutine(AttackDelay());
        }

    }

    void SummonSpider()
    {
        Instantiate(BabySpiderPre, transform.position, Quaternion.Euler(new Vector3(0.0f, 0.0f, 0.0f))).transform.parent = GameObject.Find("MonsterMng").transform;
    }

    public void AttackAniEnd()
    {
        MonsterAnime.SetBool("Attack", false);
    }

    IEnumerator AttackDelay()
    {
        bRangeAttDelay = true;
        yield return new WaitForSeconds(3.0f);
        bRangeAttackOn = false;
        bRangeAttDelay = false;
    }

    void MonsterInitReset()
    {
        nRand = Random.Range(1, 9);
        fMonsterSpeed = fSaveSpeed;
    }

    void MonsterState()
    {
        fPlayerDis = Vector2.Distance(transform.position, SGameMng.I.PlayerSc.transform.position);
        if (nMonsterHp <= 0)
        {
            SGameMng.I.nMonCount--;
            Destroy(gameObject);
        }
        if (fPlayerDis <= 7.0f)
            bFindMobOn = true;
        else
            bFindMobOn = false;

    }

    public void DebuffList(STATE_ELIMET state, int time = 0)
    {
        switch (state)
        {
            case STATE_ELIMET.NONE:
                break;
            case STATE_ELIMET.BURN:
                MobStateSr[0].gameObject.SetActive(true);
                nStateCount[0] = time;
                bStateOn[0] = true;
                Invoke("Burn", 1);
                break;
            case STATE_ELIMET.FASCINATION:
                MobStateSr[1].gameObject.SetActive(true);
                Fascination();
                break;
            case STATE_ELIMET.POISON:
                MobStateSr[2].gameObject.SetActive(true);
                nStateCount[2] = time;
                bStateOn[2] = true;
                Invoke("Poison", 1);
                break;
            case STATE_ELIMET.SLOW:
                MobStateSr[3].gameObject.SetActive(true);
                //Invoke("Slow", 1);
                break;
            case STATE_ELIMET.THUNDER:
                MobStateSr[3].gameObject.SetActive(true);
                nStateCount[3] = time;
                fMonsterSpeed = fMonsterSpeed * 0.7f;
                Invoke("Thunder", 1);
                break;
        }
    }

    void Burn()
    {
        nMonsterHp--;
        if (!nStateCount[0].Equals(0))
        {
            nStateCount[0]--;
            Invoke("Burn", 1);
        }
        else if (nStateCount[0].Equals(0))
        {
            MobStateSr[0].gameObject.SetActive(false);
            bStateOn[0] = false;
        }
    }
    void Fascination()
    {
        if (!bStateOn[1])
        {
            SGameMng.I.nMonCount--;
            bStateOn[1] = true;
        }
        SGameMng.I.FindMobList.Remove(this);
    }
    void Poison()
    {
        nMonsterHp--;
        if (!nStateCount[2].Equals(0))
        {
            nStateCount[2]--;
            Invoke("Poison", 1);
        }
        else if (nStateCount[2].Equals(0))
        {
            MobStateSr[2].gameObject.SetActive(false);
            bStateOn[2] = false;
        }
    }

    void Slow()
    {

    }
    void Thunder()
    {
        nMonsterHp--;
        if (!nStateCount[3].Equals(0))
        {
            nStateCount[3]--;
            Invoke("Thunder", 1);
        }
        else if (nStateCount[3].Equals(0))
        {
            MobStateSr[3].gameObject.SetActive(false);
            fMonsterSpeed = fSaveSpeed;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.CompareTag("Monster"))
        {
            if (!bStateOn[1])
                MonsterInitReset();
            else
            {
                Monster colmon = col.transform.GetComponent<Monster>();
                colmon.nMonsterHp -= nMonsterDmg;
            }    
        }
        if (col.transform.CompareTag("Wall"))
            MonsterInitReset();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (SGameMng.I.PlayerSc._PlayerWeaponType.Equals(ItemType.MELEE_WEAPON))
        {
            if (col.name.Equals("Weapon"))
            {
                if (SGameMng.I.PlayerSc._PlayerWeapon.Equals(ItemPrivateNum.MASTER_KEY))
                    DebuffList(STATE_ELIMET.FASCINATION);
                else
                    nMonsterHp -= SGameMng.I.PlayerSc._nWeaponDmg;
            }
        }
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
        }
    }
}
