using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{

    Transform PlayerTr;

    float fCamSpeed;

    // Start is called before the first frame update
    void Start()
    {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name.Equals("Game"))
            PlayerTr = SGameMng.I.PlayerSc.transform;
        else if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name.Equals("Lobby"))
            PlayerTr = Mng.I.CharSc.transform;
        fCamSpeed = 7.0f;
    }

    void FixedUpdate()
    {
        Vector3 TargetPos = new Vector3(PlayerTr.position.x, PlayerTr.position.y, -10f);

        transform.position = Vector3.Lerp(transform.position, TargetPos, Time.deltaTime * fCamSpeed);
    }
}
