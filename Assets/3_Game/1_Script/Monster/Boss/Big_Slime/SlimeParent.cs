using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeParent : MonoBehaviour
{
    public GameObject _BigSlimeBulletGams;
    public GameObject _JumpBulletGams;
    public GameObject _SlimeGams;
    public GameObject[] PhaseGams = new GameObject[2];

    public SlimeParent[] SmallSlime;

    public Transform FirstSlimeTr;
    public Transform[] _SummonPos = new Transform[3];

    public UnityEngine.UI.Image _BossHpBarImg;

    public Animator _BigSlimeAnime;

    public SpriteRenderer[] _StateSr = new SpriteRenderer[4];
    public SpriteRenderer _SlimeSr;

    public float _fMoveSpeed;

    public int _nAttackCount;
    public int _nAttackChildCount;
    public int _nHp;
    public int _nRand;
    public int[] nStateCount = new int[4];

    public bool[] _bPatern = new bool[3];
    public bool _bDie = false;
    public bool _bPhase = false;
    public bool[] bStateOn = new bool[4];

    public void _Move()
    {
        if (!_bDie && !SGameMng.I.PlayerSc._bPlayerDie)
            transform.position = Vector2.MoveTowards(transform.position, SGameMng.I.PlayerSc.transform.position, _fMoveSpeed * Time.deltaTime);
    }

    public void _State()
    {
        if (SGameMng.I.PlayerSc.transform.position.x > transform.position.x)
            _SlimeSr.flipX = true;
        else if (SGameMng.I.PlayerSc.transform.position.x < transform.position.x)
            _SlimeSr.flipX = false;
        if (_nHp <= 0)
        {
            SGameMng.I.bBossStage = false;
            SGameMng.I.bBossTarget = false;
            _bDie = true;
        }
    }

    public void _SelectPatern()
    {
        _nRand = Random.Range(10, 20);
        int rand = Random.Range(1, 4);
        if (rand.Equals(1))
            _bPatern[0] = true;
        else if (rand.Equals(2))
            _bPatern[1] = true;
        else if (rand.Equals(3))
            _bPatern[2] = true;
        if (!_bDie)
            Invoke("_SelectPatern", (float)_nRand);
    }

    public void _Jump()
    {
        Invoke("JumpEnd", 3);
    }

    void JumpEnd()
    {
        _BigSlimeAnime.SetBool("Jump", false);
        Instantiate(_JumpBulletGams, transform.position, Quaternion.Euler(new Vector3(0.0f, 0.0f, 0.0f)));
    }

    public void _SummonSlime()
    {
        for (int i = 0; i < _SummonPos.Length; i++)
        {
            Instantiate(_SlimeGams, _SummonPos[i].position, Quaternion.Euler(new Vector3(0.0f, 0.0f, 0.0f)));
        }
        Invoke("SummonAni", 1);
    }

    void SummonAni()
    {
        _BigSlimeAnime.SetBool("Summon", false);
        _fMoveSpeed = 4.0f;
    }

    public void _Divide()
    {
        if (!_bPhase)
        {
            _bPhase = true;
            PhaseGams[1].SetActive(true);
            PhaseGams[0].SetActive(false);
            SGameMng.I.nMonCount--;
        }
        else
        {
            SGameMng.I.nMonCount--;
            //SGameMng.I.nGameOver = 1;
            //SGameMng.I.PlayerSc._bPlayerDie = true;
            Destroy(gameObject);
        }
    }

    public void _Attack()
    {
        AttackChild();
        if (!_nAttackCount.Equals(0))
        {
            _nAttackCount--;
            Invoke("_Attack", 2.0f);
        }
        else if (_nAttackCount.Equals(0))
        {
            _BigSlimeAnime.SetBool("Attack", false);
            _fMoveSpeed = 4.0f;
        }
    }

    void AttackChild()
    {
        Instantiate(_BigSlimeBulletGams, transform.position, Quaternion.Euler(new Vector3(0.0f, 0.0f, 0.0f)));
        if (!_nAttackChildCount.Equals(0))
        {
            _nAttackChildCount--;
            Invoke("AttackChild", 0.3f);
        }
        else if (_nAttackChildCount.Equals(0))
            _nAttackChildCount = 1;
    }
}
