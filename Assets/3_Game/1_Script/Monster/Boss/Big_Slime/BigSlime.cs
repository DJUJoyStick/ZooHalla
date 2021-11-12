using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigSlime : SlimeParent
{

    void Start()
    {
        SGameMng.I.BossTr = transform;
        SGameMng.I.bBossStage = true;
        _BossHpBarImg = GameObject.Find("BossHpBar").GetComponent<UnityEngine.UI.Image>();
        _fMoveSpeed = 4.0f;
        Invoke("_SelectPatern", Random.Range(10, 20));
        SGameMng.I.nMonCount++;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_bDie)
        {
            Patern();
            _State();

            SGameMng.I.fBossDis = Vector2.Distance(transform.position, SGameMng.I.PlayerSc.transform.position);
            if (!_bDie)
            {
                SGameMng.I.bBossStage = true;
                if (SGameMng.I.fBossDis <= 7.0f)
                {
                    SGameMng.I.bBossTarget = true;
                }
                else
                    SGameMng.I.bBossTarget = false;
            }
        }
        //else
        //{
        //    SGameMng.I.nGameOver = 1;
        //    SGameMng.I.PlayerSc._bPlayerDie = true;
        //}
        if (SGameMng.I.PlayerType.Equals(PLAYERTYPE.RAT))
        {
            if (SGameMng.I.PlayerSc._bSkillOn)
            {
                DebuffList(STATE_ELIMET.POISON, 9);
            }
        }
        if (SmallSlime[0]._nHp <= 0 && SmallSlime[1]._nHp <= 0)
        {
            SGameMng.I.nGameOver = 1;
        }
    }

    private void FixedUpdate()
    {
        _Move();
    }

    void Patern()
    {
        if (_bPatern[0])
        {
            _Jump();
            _BigSlimeAnime.SetBool("Jump", true);
            _bPatern[0] = false;
        }
        else if (_bPatern[1])
        {
            _BigSlimeAnime.SetBool("Summon", true);
            _fMoveSpeed = 0.0f;
            _SummonSlime();
            _bPatern[1] = false;
        }
        else if (_bPatern[2])
        {
            _BigSlimeAnime.SetBool("Attack", true);
            _nAttackCount = 1;
            _nAttackChildCount = 1;
            _fMoveSpeed = 0.0f;
            Invoke("_Attack", 0.5f);
            _bPatern[2] = false;
        }
        if (!_bPhase && _nHp <= 0)
        {
            PhaseGams[1].transform.position = FirstSlimeTr.position;
            _BigSlimeAnime.SetBool("Seperation", true);
        }
    }

    public void DebuffList(STATE_ELIMET state, int time = 0)
    {
        switch (state)
        {
            case STATE_ELIMET.NONE:
                break;
            case STATE_ELIMET.BURN:
                _StateSr[0].gameObject.SetActive(true);
                nStateCount[0] = time;
                bStateOn[0] = true;
                Invoke("Burn", 1);
                break;
            case STATE_ELIMET.POISON:
                _StateSr[2].gameObject.SetActive(true);
                nStateCount[2] = time;
                bStateOn[2] = true;
                Invoke("Poison", 1);
                break;
            case STATE_ELIMET.SLOW:
                _StateSr[3].gameObject.SetActive(true);
                //Invoke("Slow", 1);
                break;
            case STATE_ELIMET.THUNDER:
                _StateSr[3].gameObject.SetActive(true);
                nStateCount[3] = time;
                Invoke("Thunder", 1);
                break;
        }
    }

    void Burn()
    {
        _nHp--;
        if (!nStateCount[0].Equals(0))
        {
            nStateCount[0]--;
            Invoke("Burn", 1);
        }
        else if (nStateCount[0].Equals(0))
        {
            _StateSr[0].gameObject.SetActive(false);
            bStateOn[0] = false;
        }
    }

    void Poison()
    {
        _nHp--;
        if (!nStateCount[2].Equals(0))
        {
            nStateCount[2]--;
            Invoke("Poison", 1);
        }
        else if (nStateCount[2].Equals(0))
        {
            _StateSr[2].gameObject.SetActive(false);
            bStateOn[2] = false;
        }
    }

    void Slow()
    {

    }
    void Thunder()
    {
        _nHp--;
        if (!nStateCount[3].Equals(0))
        {
            nStateCount[3]--;
            Invoke("Thunder", 1);
        }
        else if (nStateCount[3].Equals(0))
            _StateSr[3].gameObject.SetActive(false);
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
                _nHp -= SGameMng.I.PlayerSc._nFinalDmg;
            else
                _nHp -= SGameMng.I.PlayerSc._nWeaponDmg;

            Destroy(col.gameObject);
        }
    }

}
