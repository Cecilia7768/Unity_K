using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour
{
    //3인칭으로 게임오브젝트를 보기위해 설정
    //게임오브젝트에서 멀리 떨어져있어야하므로 퍼블릭으로 이동

    public GameObject target;
    public float xOffset, yOffset, zoffset;
    void Update()
    {
        //위치를 이동하고 조금 멀어지게해야 하므로 new vec 좌표설정
        transform.position = target.transform.position + new Vector3(xOffset,yOffset,zoffset);

        transform.LookAt(target.transform.position);
    }
}
