using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutCheck : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        //큐브 콜라이더밖에 나갔을때 비활성화
        other.gameObject.SetActive(false);
    }

}
