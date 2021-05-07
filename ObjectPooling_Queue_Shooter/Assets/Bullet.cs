using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 direction;
    public void Shoot(Vector3 dir)
    {
        direction = dir; //매개변수로 받은 방향을 변수에 넣어줌
        //Destroy(gameObject, 5f); //발사된지 5초가 지나면 파괴

        //5초후에 DestroyBullet 함수를 호출
        Invoke("DestroyBullet", 5f);
    }

    private void DestroyBullet()
    {
        //사용이 끝난 총알을 ObjectPool에 반환하게 만듬
        ObjectPool.ReturnObject(this);
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction); //프레임마다 디렉션방향으로 이동
    }
}
