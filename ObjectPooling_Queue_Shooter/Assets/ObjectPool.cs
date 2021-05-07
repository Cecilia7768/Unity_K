using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    // Start is called before the first frame update
    public static ObjectPool Instance;
    [SerializeField]
    private GameObject poolingObjectPrefab;
    private Queue<Bullet> poolingObjectQueue = new Queue<Bullet>();
    //오브젝트들이 순서대로 기다리고있다가 차례대로 빌려짐. 반환되는 객체는 다시 제일 뒤로가서 자기 차례를 기다림.
    private void Awake()
    {
        Instance = this;
        Initialized(10);
    }
    private Bullet CreateNewObject()
    {
        //오브젝트 풀을 초기화할때 미리 빌려줄 오브젝트를 만들거나 오브젝트풀이 모든 오브젝트를 빌려줘서 빌려줄오브젝트를 새로 만들어야할때 사용
        var newObj = Instantiate(poolingObjectPrefab, transform).GetComponent<Bullet>();
        newObj.gameObject.SetActive(false);
        return newObj;
    }
    private void Initialized(int count)
    {
        //매개변수로 받은 수만큼 미리 풀링할 오브젝트를 생성하는 역할
        for (int i = 0; i < count; i++)
            poolingObjectQueue.Enqueue(CreateNewObject());
    }
    public static Bullet GetObject()
    {
        //ObjectPool에서 오브젝트를 빌려갈 때 사용할 함수
        //오브젝트를 빌려가는 순간에는 빌려줄 오브젝트가 있을때와 없을때 2가지로 나뉨
        if (Instance.poolingObjectQueue.Count > 0)
        {
            var obj = Instance.poolingObjectQueue.Dequeue();
            obj.transform.SetParent(null);
            obj.gameObject.SetActive(true);
            return obj;
        }
        else
        {
            var newobj = Instance.CreateNewObject();
            newobj.transform.SetParent(null);
            newobj.gameObject.SetActive(true);
            return newobj;
        }
    }
    public static void ReturnObject(Bullet bullet)
    {
        //빌려줬던 오브젝트를 돌려받는 함수
        //돌려받을 게임 오브젝트를 매개변수로 받아서 비활성화 시키고
        bullet.gameObject.SetActive(false);
        //ObjectPool 아래로 옮긴 뒤에 poolingObjectQueue에 넣음
        bullet.transform.SetParent(Instance.transform);
        Instance.poolingObjectQueue.Enqueue(bullet);

    }
}
