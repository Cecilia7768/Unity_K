using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatMoving : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rigid2D;
    float jump = 680.0f;
    float walkForce = 30.0f;
    float maxWalkSpeed = 2.0f;
    void Start()
    {
        this.rigid2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
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

    }
}
