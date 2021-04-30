using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    public GameObject coin_prefab;

    private void Update()
    {
        //코인을 먹을때마다 다시 랜덤좌표에 새 코인 생성
        //트리거 됐을때 죽은 코인이 목록에 계속 떠있음. Destroy ?
        int check = 10;
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Coin");
        if (gos.Length == check - 1)
        {
            Make();
        }
        else if (gos.Length == 0)
        {
            for (int i = 0; i < check; i++)
            {
                Make();
            }
        }
    }
    private void Make()
    {
        GameObject item = Instantiate(coin_prefab) as GameObject;
        item.SetActive(true);
        float x = Random.Range(-30f, 30f);
        float y = Random.Range(-30f, 30f);
        item.transform.localPosition = new Vector3(x, 1, y);
    }
}


