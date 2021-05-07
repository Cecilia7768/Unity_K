using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab; //총알 프리팹을 담을 변수
    private Camera maincam; //마우스 위치를 찾아내는데 쓸 카메라 변수
    void Start()
    {
        maincam = Camera.main; //씬의 메인카메라를 가져와서 담음
    }
    // Update is called once per frame
    void Update()
    {
     if(Input.GetMouseButton(0))
        {
            RaycastHit hitResult; //RaycastHit : 충돌감지가 일어났는지 감지. hit이 되면 감지가 되어 코드실행. 안되면 null, 아무일도 일어나지 않음
            if(Physics.Raycast(maincam.ScreenPointToRay(Input.mousePosition),out hitResult))
            //Physics.물리속성및 도우미메서드 Raycast.장면의 모든 충돌체에 대해 원점에서 방향으로 광선을 투사(ScreenPointToRay 카메라에서 시작된 화면 지점을 지나는 광선을 반환)
            //RayCast는 공간의 특정 지점에서 특정한 방향과 거리 안에 게임오브젝트가 존재하는지 판별하는 유니티 물리엔진 메서드(레이저를 쏴서 감지한다는 느낌)
            //충돌이 일어나면 Physics.Raycast() 메서드는  true를 반환함
            //out (매개변수 한정자) 직접 매개변수의 값을 바꿀 수있음. C에서 나오는 콜바이레퍼런스(변수의 주소를 전달하는것)랑 비슷
            //레이저를쏜다(메인카메라의 광선을쏴서(마우스키가 눌린곳에),RaycastHit이 된 물체를 반환)
            {
                var direction = new Vector3(hitResult.point.x,transform.position.y,hitResult.point.z) - transform.position;
                //direction(방향) 변수 = 벡터3(Hit된 물체의 x값,기본 y값?,z값) - 기본 포지션 값을 넣어준다
                var bullet = ObjectPool.GetObject();
                    //Instantiate(bulletPrefab, transform.position + direction.normalized, Quaternion.identity).GetComponent<Bullet>();
                //bullet 변수 = 게임도중에 오브젝트생성(불렛프리팹, 기본포지션+ direction의 normalized :보통처럼만들다,기준 개념을 만들다?,Quaternion 회전을 위한 함수.identity 굳이 회전안해도 될때).불렛컴포넌트 가져오기
                bullet.Shoot(direction.normalized); //bullet의 Shoot함수에 direction기본값 방향을 넘겨줌
            }
        }
    }
}
