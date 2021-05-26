using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{

    public Transform PlayerTr;

    float fCamSpeed;

    // Start is called before the first frame update
    void Awake()
    {
        fCamSpeed = 2.0f;
    }

    void FixedUpdate()
    {
        Vector3 TargetPos = new Vector3(PlayerTr.position.x, PlayerTr.position.y, -10f);

        transform.position = Vector3.Lerp(transform.position, TargetPos, Time.deltaTime * fCamSpeed);

    }
}
