using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//클릭시 이동
//화살표 x좌표 랜덤출현 (코루틴?)
//화살표가 ㅈ고앵이 콜라이터 충돌시 HP바 -- 구현
public class CharacterController : MonoBehaviour
{
    public AudioClip cat;
    AudioSource aud;

    void Start()
    {
        this.aud = GetComponent<AudioSource>();
    }
    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Arrow"))
        {
            this.aud.PlayOneShot(this.cat);
        }
    }
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.Translate(-3, 0, 0);
        }
        if(Input.GetKeyUp(KeyCode.RightArrow))
        {
            transform.Translate(3, 0, 0);
        }
    }
}
