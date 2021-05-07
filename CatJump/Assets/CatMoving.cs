using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CatMoving : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rigid2D;
    Animator animator;
    float jump = 680.0f;
    float walkForce = 30.0f;
    float maxWalkSpeed = 2.0f;
    void Start()
    {
        this.rigid2D = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && this.rigid2D.velocity.y == 0)
        {
            this.animator.SetTrigger("JumpTrigger"); //점프액션 삽입
            //SetTrigger ->  매개변수로 지정한 이름의트리거를 열어 애니메이션으로 전환
            this.rigid2D.AddForce(transform.up * this.jump);     
        }

        int key = 0;
        if(Input.GetKey(KeyCode.LeftArrow))        
            key = -1;        
        if (Input.GetKey(KeyCode.RightArrow))
            key = 1;

        float speedx = Mathf.Abs(this.rigid2D.velocity.x);
        if (speedx < this.maxWalkSpeed)
            this.rigid2D.AddForce(transform.right * key * this.walkForce);

        if (key != 0)
            transform.localScale = new Vector3(key, 1, 1);

        ///y축 방향 속도를 보고 점프중인지 아닌지 체크
        if (this.rigid2D.velocity.y == 0)
            this.animator.speed = speedx / 2.0f; //플레이어 속도에 맞춰 애니메이션 속도를 바꿈
        else
            this.animator.speed = 1.0f;

        if (transform.position.y < -10) // 화면 밖으로 나갔다면 처음부터
            SceneManager.LoadScene("CatJump");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("골");
        SceneManager.LoadScene("ClearScene");
    }
}
