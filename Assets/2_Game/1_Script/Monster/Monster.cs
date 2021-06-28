using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Monster : MonoBehaviour
{
    public GameObject MobBulletPre;

    public Monster SelfMonsterSc;

    public SpriteRenderer MobSp;

    public Rigidbody2D MonsterRigid;

    public MONSTERTYPE MobType;
    public MONSTERLIST MobName;

    public int nMonsterHp;
    public int nMonsterDmg;
    public int nDotCount;                                                       // 도트데미지 횟수 변수
    public int nRand;

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

    void Start()
    {
        SelfMonsterSc = this;
        MobSp = GetComponent<SpriteRenderer>();
        MonsterRigid = GetComponent<Rigidbody2D>();
        SGameMng.I.FindMobList.Add(this);
        MobInit(transform.name);
        nRand = Random.Range(0, 9);
        nDotCount = 0;
        bMoveAccess = true;
        bPosChange = true;
        InvokeRepeating("MonsterInitReset", 5.0f, 5.0f);
        fPlayerDis = 10.0f;                                             // 게임이 시작되면 0으로 초기화되기에 그걸 방지하기 위함
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
                MobSetting(MONSTERTYPE.RANGED_MONSTER, MONSTERLIST.VIPER, 10, 1, 5.0f, 3.0f, 5.0f);
                break;
            case "Hunter_Trap":
                MobSetting(MONSTERTYPE.MELEE_MONSTER, MONSTERLIST.HUNTER_TRAP, 5, 2, 4.0f, 0.0f, 5.0f);
                break;
            case "Big_Carnivorous_Plant":
                MobSetting(MONSTERTYPE.RANGED_MONSTER, MONSTERLIST.BIG_CARNIVOROUS_PLANT, 5, 3, 10.0f, 4.0f);
                break;
        }
    }

    // 근,원거리 여부, 몬스터 이름, 체력, 데미지, 속도, 추격범위, 공격범위 (디폴트 매개변수가 있으니 공격범위, 이동속도는 지정 안해줘도됨)
    void MobSetting(MONSTERTYPE type, MONSTERLIST name, int hp, int dmg, float chaserg, float attackrg = 0.0f, float speed = 0.0f)
    {
        MobType = type;
        MobName = name;
        nMonsterHp = hp;
        nMonsterDmg = dmg;
        ChaseRange = chaserg;
        fMonsterSpeed = speed;
        fSaveSpeed = fMonsterSpeed;
        AttackRange = attackrg;
    }

    void Move()
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
            Instantiate(MobBulletPre, transform.position, Quaternion.Euler(0f, 0f, 0f));
            if (!bRangeAttDelay)
                StartCoroutine(AttackDelay());
        }

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
        nRand = Random.Range(0, 9);
        fMonsterSpeed = fSaveSpeed;
    }

    void MonsterState()
    {
        fPlayerDis = Vector2.Distance(transform.position, SGameMng.I.PlayerSc.transform.position);
        if (nMonsterHp <= 0)
            Destroy(gameObject);

        if (fPlayerDis <= 7.0f)
            bFindMobOn = true;
        else
            bFindMobOn = false;
    }

    public void MobDebuffs(PLAYERTYPE Type)
    {
        switch (Type)
        {
            case PLAYERTYPE.RAT:
                nDotCount = 10;
                StartCoroutine(DotDmg());
                break;
        }
    }

    IEnumerator DotDmg()                        // 독 도트데미지
    {
        nDotCount--;
        nMonsterHp--;
        yield return new WaitForSeconds(1.0f);
        if (nDotCount > 0)
            StartCoroutine(DotDmg());
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.CompareTag("Monster") || col.transform.CompareTag("Wall"))
        {
            MonsterInitReset();
        }
    }

}
