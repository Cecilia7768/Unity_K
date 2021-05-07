using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    public static GameObject coin_prefab;
    public GameObject Coin_Objects;
    public bool collider_check;

    static List<GameObject> CoinList = new List<GameObject>(); 

    private void Start()
    {
        coin_prefab = Coin_Objects;
        StartCoroutine(Coin_Maker());
    }
    IEnumerator Coin_Maker()
    {
        //첫코인 로딩시 2초 대기후 다음코인 생성하기위해 사용
        while (true)
        {
            if (CoinList.Count < 10 ) //코인리스트가 10개 미만일때 (초기 리스트 사이즈 10개로 셋팅해줌)
            {
                Make_CoinList(); //프리팹을 하나씩 넣어줌
            }
            yield return new WaitForSeconds(2f); //2초 대기후 다음코인 생성   
        }
    }
    private static void Make_CoinList() //첫 로딩시 코인을 만들어주는 함수
    {
        GameObject C_obj = null;
        C_obj = GameObject.Instantiate(coin_prefab) as GameObject;        //coin프리팹 오브젝트를 가져와서
        C_obj.SetActive(true); //활성화 시켜주고
        Setting_XY(C_obj);    //활성화된 코인에 랜덤 위치설정
        CoinList.Add(C_obj); //리스트에 추가시켜준다
    }
    public static void Use_CoinList() //코인이 사라질때마다 보관된 리스트에서 다시 빌려주는 함수
    {
        GameObject C_obj = null;
        for (int i = 0; i < CoinList.Count; i++) //앞에서 미리 리스트 길이를 10개로 만들어줬고
        {         
            if (!CoinList[i].activeSelf) //비활성된 코인 자리가있으면(먹힌 코인이 있다면)
            {
                C_obj = CoinList[i]; //죽은 코인을 변수에 잠시 넣고
                Setting_XY(C_obj); //좌표만 새로 뽑아주고
                C_obj.SetActive(true); //해당 오브젝트를 생성없이 다시 쓸수있게 바로 활성화
            }
        }
    }
    private static void Setting_XY(GameObject obj) //좌표를 랜덤으로 새로 뽑아주는 함수
    {
        float x = Random.Range(-30f, 30f);
        float y = Random.Range(-30f, 30f);
        obj.transform.localPosition = new Vector3(x, 1, y);
    }

    ///////////////// 선생님과 했던 기존 오브젝트 풀링 예시 //////////////////////
    //private void Start()
    //{
    //    StartCoroutine(Coin_Maker());
    //}
    //IEnumerator Coin_Maker()
    //{
    //    while (true)
    //    {
    //        if (check >= 0 && check < 10) //10개까지만 생성
    //        //if (Free_coins.Count != 0)
    //        {
    //            Make();        //코인 지속생성
    //            check++;
    //        }
    //        yield return new WaitForSeconds(0.5f); //0.5초 대기후 다음코인 생성   
    //        //기존 코인 충돌 되면 코인 1개씩 다시 생성
    //    }
    //} 

    //public static void Push(GameObject go_)
    //{
    //    check--; //사라져서 만듬
    //    go_.SetActive(false);
    //    Free_coins.Enqueue(go_);
    //}
    //private static GameObject Pop()
    //{
    //    if (Free_coins.Count != 0)
    //    {
    //        var go = Free_coins.Dequeue();
    //        go.SetActive(true);
    //        return go;
    //    }
    //    return null;
    //}
    //private void Make()
    //{
    //    //코인생성
    //    GameObject item;

    //    item = Pop();
    //    if (item == null) //item에 담긴게 없으면
    //    {
    //        item = GameObject.Instantiate(coin_prefab) as GameObject;
    //        //coin프리팹 오브젝트를 가져와서
    //        item.SetActive(true);
    //        //활성화
    //    }
    //    //랜덤좌표생성 + 위치설정
    //    float x = Random.Range(-30f, 30f);
    //    float y = Random.Range(-30f, 30f);
    //    item.transform.localPosition = new Vector3(x, 1, y);
    //}

}


