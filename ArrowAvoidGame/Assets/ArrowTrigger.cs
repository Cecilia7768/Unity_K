using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrigger : MonoBehaviour
{
    GameObject player;
    void Start()
    {
        this.player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(0, -0.01f, 0);
        if (transform.position.y < -5.0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            this.gameObject.SetActive(false);           
            GameObject hpBar = GameObject.Find("HPManager");
            hpBar.GetComponent<HPManager>().DecreaseHp();
            Destroy(this.gameObject);
        }
    } 

    private void SetArrows()
    {

    }
}
