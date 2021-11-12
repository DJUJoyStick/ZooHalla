using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game_End : MonoBehaviour
{
    public List<Sprite> GetGameOverImg ;
    private Image GameOverSpr;
    public Image GetPlayerIcon;
    public Image GetBackGround;

    public List<Image> CraftItems;
    private List<Sprite> GetItems;

    public Button[] GameEndBtns;


    private void Start()
    {
        GameOverSpr = GetComponent<Image>();
        GetItems = SGameMng.I.GetInven.CraftItemsSpr;
    }

    private void Update()
    {
        if (!SGameMng.I.nGameOver.Equals(0))
        {
            for (int i = 0; i < GetItems.Count; i++)
            {
                CraftItems[i].color = Color.white;
                CraftItems[i].sprite = GetItems[i];
            }
            GameOverCheck();
        }
    }

    public void GameOverCheck()
    {
        GameOverSpr.color = Color.white;
        GetBackGround.color = Color.white;
        GetPlayerIcon.color = Color.white;
        GetPlayerIcon.sprite = SGameMng.I.PlayerSc._PlayerSr.sprite;
        if (SGameMng.I.nGameOver.Equals(1))//클리어
        {
            GameOverSpr.sprite = GetGameOverImg[0];
            GameEndBtns[0].gameObject.SetActive(true);
            GameEndBtns[1].gameObject.SetActive(true);
            GameEndBtns[2].gameObject.SetActive(false);
        }
        else if (SGameMng.I.nGameOver.Equals(2))//게임오버
        {
            GameOverSpr.sprite = GetGameOverImg[1];
            GameEndBtns[0].gameObject.SetActive(true);
            GameEndBtns[1].gameObject.SetActive(false);
            GameEndBtns[2].gameObject.SetActive(true);
        }
        SGameMng.I.nGameOver = 0;
    }

    public void GoToRobby()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Lobby");
    }

    public void NextStage()
    {
        //UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
        Debug.Log("추후 업데이트 예정입니다.");
    }

    public void GameExit()
    {
        Application.Quit();
    }

}
