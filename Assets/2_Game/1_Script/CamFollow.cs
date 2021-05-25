using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{

    public Transform[] PlayerTr = new Transform[3];

    float fCamSpeed;

    // Start is called before the first frame update
    void Awake()
    {
        fCamSpeed = 2.0f;
    }

    void FixedUpdate()
    {

        if (SGameMng.I.PlayerType.Equals(PLAYERTYPE.RAT))
        {
            ChaseCam(SGameMng.I.PlayerType);
        }
        else if (SGameMng.I.PlayerType.Equals(PLAYERTYPE.TURTLE))
        {
            ChaseCam(SGameMng.I.PlayerType);
        }
        else if (SGameMng.I.PlayerType.Equals(PLAYERTYPE.WOLF))
        {
            ChaseCam(SGameMng.I.PlayerType);
        }
    }

    void ChaseCam(PLAYERTYPE type)
    {
        Vector3 TargetPos = new Vector3(PlayerTr[(int)type].position.x, PlayerTr[(int)type].position.y, -10f);
        transform.position = Vector3.Lerp(transform.position, TargetPos, Time.deltaTime * fCamSpeed);
    }

}
