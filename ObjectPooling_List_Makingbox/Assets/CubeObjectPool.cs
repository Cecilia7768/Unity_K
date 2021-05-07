using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeObjectPool : MonoBehaviour
{
    //오브젝트 풀을 만드는 2가지 방법.
    //풀링(공간의 기준)의 기준을 어디로 두느냐에 따라 달라짐
    //1. Pool의 자식으로 들어가는 경우(오브젝트의 부모를 활용한 방법)
    public Transform poolTr; //부모
    public GameObject prefabObject;
    //private void Update()
    //{
    //    if(Input.GetKeyDown(KeyCode.Space))
    //    {
    //        GameObject t_obj = AddObject();
    //        t_obj.transform.position = poolTr.position;
    //        t_obj.GetComponent<Rigidbody>().velocity = Vector3.zero;
    //    }
    //}
    //public GameObject AddObject()
    //{
    //    GameObject t_obj = null;
    //    for(int i = 0; i< poolTr.childCount ; i++) //PoolManaget의 자식오브젝트 개수만큼 
    //    {
    //        if(!poolTr.GetChild(i).gameObject.activeSelf) //i번째 자식의 게임오브젝트가 비활성화 되어있다면
    //        {
    //            t_obj = poolTr.GetChild(i).gameObject; //i번째 자식의 게임오브젝트를 변수에 넣고
    //            t_obj.SetActive(true); //쓸수있게 활성화를 시켜줌 (반대 순서 아닌가??)
    //            break;
    //        }
    //    }
    //    if(t_obj == null) //변수가 비어있다면
    //    {
    //        t_obj = Instantiate(prefabObject, poolTr); //프리팹오브젝트를 생성함
    //    }
    //    return t_obj;
    //    //GameObject t_obj = Instantiate(prefabObject, poolTr);

    //2. 리스트(또는 큐) 활용
    public List<GameObject> poolList;
    private void Awake()
    {
        poolList = new List<GameObject>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject t_obj = AddObject();
            t_obj.transform.position = poolTr.position;
            t_obj.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
    public GameObject AddObject()
    {
        GameObject t_obj = null;
        for (int i = 0; i < poolList.Count; i++) //리스트의 갯수만큼 반복
        {
            if (!poolList[i].activeSelf) //i번째 오브젝트가 비활성화 되어있다면(쓰고 죽었다면)
            {
                t_obj = poolList[i]; //변수에 대입하고
                t_obj.SetActive(true); //다시쓸수있게 활성화
                break;
            }
        }
        if(t_obj == null) //변수가 비어있다면 -> 위의 과정이 실행이 안됐다면 -> 생성된적이 없다면
        {
            t_obj = Instantiate(prefabObject, poolTr);
            poolList.Add(t_obj); //ADD() <-배열관련 메서드. 리스트 뒤에 추가시켜줌
        }

        return t_obj;
    }


}

    //    public static CubeObjectPool Instance;
    //    [SerializeField]
    //    private GameObject CubePrefab;
    //    private Queue<GameObject> CubePoolingQueue = new Queue<GameObject>();

    //    //큐 목차 만들기
    //    //처음 큐 셋팅 -> 근데 트리거때 사라지면 그걸 넣을거 아닌가??? 그럼 ... 처음부터 만드는게 아니라 트리거 된거를 Push해야할거같은데?
    //    //지속적으로 큐 만들다가, 큐 목록이 꽉차면(10개) 인큐 중단
    //    //큐가 꽉찼고 -> 그이후로 다시 큐브를 쓸거면 -> Pop해서 꺼냄
    //    void Start()
    //    {
    //        Queue_Setting();//첨하는거니까 큐에 큐브프리팹 열번 셋팅부터 해주고
    //    }
    //    private GameObject Create_Cube() //큐브 한개 만드는 함수
    //    {
    //        var obj = Instantiate(CubePrefab, transform).GetComponent<GameObject>();
    //        obj.gameObject.SetActive(false); //당장쓸건아니니까 비활성화해둠
    //        return obj;
    //    }
    //    void Update()
    //    {
    //        //지속적으로 확인할것들. 뭘?? 큐를 하나 쓸거면 맨앞에거를 활성화해서 꺼내쓰고, 다쓰면 뒤에 다시 붙이고. 비활성화
    //        //언제 큐를 쓸건지?? 마우스 클릭?
    //        if(Input.GetKeyDown(KeyCode.Space)) //마우스 왼쪽버튼을 누르면 큐브가 열개 우루루쏟아지게
    //        {
    //            Pop();
    //        }
    //    }
    //    private void Queue_Setting() //큐 배열을 만드는 함수
    //    {
    //        for (int i = 0; i < 10; i++)
    //        {
    //            CubePoolingQueue.Enqueue(Create_Cube()); //큐브 열개 만들어서 큐 꽉차게 만듬
    //        }
    //    }    
    //    private GameObject Pop() 
    //    {
    //        //빌려줄 오브젝트가 있을때와 없을때 2가지로 나뉨. 근데 처음에 셋팅해줫으니까 전자의 경우는 없지않나?

    //            var obj = CubePoolingQueue.Dequeue();
    //            obj.SetActive(true); //바로써야하니까 활성화
    //            return obj;        
    //    }
    //    private void ReturnObject(GameObject obj_) //빌려줬던  오브젝트를 돌려받는 함수
    //    {
    //        //돌려받을 게임 오브젝트를 매개변수로 받아서 비활성화 시키고
    //        obj_.SetActive(false);
    //        CubePoolingQueue.Enqueue(obj_); //뒤에 다시 인큐해서 집어넣음
    //    } 


