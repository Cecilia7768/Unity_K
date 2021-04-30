using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    float CarSpeed = 0;
    // Start is called before the first frame update
    Vector2 now_Position;
    void Start()
    {
      //  Vector2 c_position = new Vector2(2, 0);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) //마우스 왼쪽을 클릭하면
        {
            //this.CarSpeed = 0.2f;
            this.now_Position = Input.mousePosition;
        }
        if(Input.GetMouseButtonUp(0))
        {
            Vector2 end_Position = Input.mousePosition;
            float swipeLength = end_Position.x - this.now_Position.x;
            this.CarSpeed = swipeLength / 500.0f;
            GetComponent<AudioSource>().Play();
        }
        transform.Translate(this.CarSpeed, 0, 0);
        this.CarSpeed *= 0.98f;
    }
}
