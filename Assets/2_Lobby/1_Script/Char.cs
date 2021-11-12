using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Char : MonoBehaviour
{
    public GameObject InteractionBtnGams;
    public Transform JoystickTr;

    public Rigidbody2D PlayerRig;

    public Animator PlayerAnime;

    public RuntimeAnimatorController[] ani = new RuntimeAnimatorController[3];

    public Vector2 MoveVec;
    public Vector2 RotVec;

    float fMoveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        Mng.I.Play("Lobby", false, true);
        fMoveSpeed = 10.0f;
    }

    // Update is called once per frame
    void Update()
    {
        getKey();
        movement();
        Init(Mng.I.SelectedType);
        State();
    }

    public void getKey()
    {
        MoveVec = new Vector3(CnControls.CnInputManager.GetAxis("Horizontal"), CnControls.CnInputManager.GetAxis("Vertical"));
        RotVec = new Vector3(CnControls.CnInputManager.GetAxis("RotateX"), CnControls.CnInputManager.GetAxis("RotateY"));
    }

    public void movement()
    {
        PlayerRig.velocity = MoveVec * fMoveSpeed;
    }

    void Init(PLAYERTYPE type)
    {
        if(Mng.I.bSelected)
        {
            Mng.I.bSelected = false;
            PlayerAnime.runtimeAnimatorController = ani[(int)type];
        }
    }

    void State()
    {
        if (JoystickTr.localPosition.x < 0)
        {
            PlayerAnime.SetBool("isLWalking", true);
            PlayerAnime.SetBool("isRWalking", false);
            PlayerAnime.SetBool("isIdle", false);
        }
        if (JoystickTr.localPosition.x > 0)
        {
            PlayerAnime.SetBool("isLWalking", false);
            PlayerAnime.SetBool("isRWalking", true);
            PlayerAnime.SetBool("isIdle", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("LobbyGate"))
            UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
        if (col.CompareTag("Unimplemented"))
        {
            Mng.I.nUnimplemented = 1;
            InteractionBtnGams.SetActive(true);
        }
        if (col.CompareTag("Selecting"))
        {
            Mng.I.nUnimplemented = 2;
            InteractionBtnGams.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Unimplemented"))
        {
            Mng.I.nUnimplemented = 0;
            InteractionBtnGams.SetActive(false);
        }
        if (col.CompareTag("Selecting"))
        {
            Mng.I.nUnimplemented = 0;
            InteractionBtnGams.SetActive(false);
        }
    }
}
