using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutScene : MonoBehaviour
{
    public SpriteRenderer CutSceneSr;
    public Sprite[] CutSceneSp = new Sprite[11];
    int nScene;

    private void Awake()
    {
        Screen.SetResolution(1280, 720, true);
    }

    void Start()
    {
        nScene = 0;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < CutSceneSp.Length; i++)
        {
            if (nScene.Equals(i))
                CutSceneSr.sprite = CutSceneSp[i];
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (!nScene.Equals(10))
                nScene++;
            else if (nScene.Equals(10))
                UnityEngine.SceneManagement.SceneManager.LoadScene("Lobby");
        }
    }
}
