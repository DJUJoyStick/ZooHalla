using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SGameMng : MonoBehaviour
{
    private static SGameMng _Instance = null;

    public static SGameMng I
    {
        get
        {
            if (_Instance.Equals(null))
            {
                Debug.Log("Instance is null");
            }
            return _Instance;
        }
    }

    public PLAYERTYPE PlayerType;

    public GameObject[] PlayerGams = new GameObject[3];

    public List<Monster> FindMobList = new List<Monster>();

    public Transform GameTr;
    public Transform TargetEnemyTr;
    public Transform BossTr;

    public UnityEngine.UI.Text testLog;
    public UnityEngine.UI.Image C_MapColor;
    public Inventory GetInven;
    public ItemMng GetItemMng;
    public BtnMng GetBtnMng;

    public Player PlayerSc;
    public MonsterMng MonsterMngSc;
    public Monster TargetEnemySc;
    public TextMng TextMngSc;
    public GameObject[] GetItemGams;
    public TMPro.TextMeshProUGUI GetRoomInfoText;

    public BtnMng BtnMngSc;

    public UnityEngine.UI.Image CharImg;
    public UnityEngine.UI.Button CharActiveBtn;

    public Sprite[] WeaponSp;
    public Sprite[] CharSp = new Sprite[3];
    public Sprite[] StateSp = new Sprite[5];        // 0 : 화상 1 : 매혹 2 : 독 3 : 둔화 4 : 감전
    public Sprite[] PlayerBulletSp;
    public Sprite[] CharActiveSkillSp = new Sprite[3];

    public int nMonCount;
    public int nGameOver = 0;

    public float fTargetDis;
    public float fBossDis;

    public bool bJoystickDown = false;
    public bool bRoomStart = false;
    public bool bBossTarget = false;
    public bool bBossStage = false;
    public bool b_WeaponSelected = false;
    public bool bMobileOn = false;

    private void Awake()
    {
        _Instance = this;
        PlayerType = Mng.I.SelectedType;
        bMobileOn = true;
        if (PlayerType.Equals(PLAYERTYPE.RAT))
        {
            Instantiate(PlayerGams[0], GameTr.position, Quaternion.Euler(new Vector3(0.0f, 0.0f, 0.0f))).transform.parent = GameTr;
            PlayerSc = GameObject.Find("Rat(Clone)").GetComponent<Player>();
            CharActiveBtn.image.sprite = CharActiveSkillSp[0];
        }
        else if (PlayerType.Equals(PLAYERTYPE.TURTLE))
        {
            Instantiate(PlayerGams[1], GameTr.position, Quaternion.Euler(new Vector3(0.0f, 0.0f, 0.0f))).transform.parent = GameTr;
            PlayerSc = GameObject.Find("Turtle(Clone)").GetComponent<Player>();
            CharActiveBtn.image.sprite = CharActiveSkillSp[1];
        }
        else if (PlayerType.Equals(PLAYERTYPE.WOLF))
        {
            Instantiate(PlayerGams[2], GameTr.position, Quaternion.Euler(new Vector3(0.0f, 0.0f, 0.0f))).transform.parent = GameTr;
            PlayerSc = GameObject.Find("Wolf(Clone)").GetComponent<Player>();
            CharActiveBtn.image.sprite = CharActiveSkillSp[2];
        }
        nMonCount = 0;
    }

    void Start()
    {

        if (PlayerType.Equals(PLAYERTYPE.RAT))
        {
            CharImg.sprite = CharSp[0];
        }
        else if (PlayerType.Equals(PLAYERTYPE.TURTLE))
        {
            CharImg.sprite = CharSp[1];
        }
        else if (PlayerType.Equals(PLAYERTYPE.WOLF))
        {
            CharImg.sprite = CharSp[2];
        }
        //CharImg.sprite = CharSp[2];
    }

    public void log(string msg)
    {
        testLog.text += msg + "\n";
    }

    public void BulletDirectToPlayer(Transform tr)
    {
        float dy = PlayerSc.transform.position.y - tr.position.y;
        float dx = PlayerSc.transform.position.x - tr.position.x;
        float fBulletDegree = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;

        tr.rotation = Quaternion.AngleAxis(fBulletDegree - 90f, Vector3.forward);
    }
    public IEnumerator BulletDestroy(GameObject ob)
    {
        yield return new WaitForSeconds(5.0f);
        Destroy(ob);
    }

}