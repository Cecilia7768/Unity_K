using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour
{
    private Rigidbody body;
    int score;
    public float ballSpeed;
    public Text CountText;
    public Text WinText;
    public float timelimit ;
    public Text timer;
    void Start()
    {
        body = GetComponent<Rigidbody>();
        score = 0;
        setCountText();
    }
    private void Awake()
    {
        timelimit = 10f;
    }
    void Update()
    {
        float xSpeed = Input.GetAxis("Horizontal");
        float ySpeed = Input.GetAxis("Vertical");
        body.AddTorque(new Vector3(xSpeed, 0, ySpeed) * ballSpeed * Time.deltaTime);

        if (timelimit > 0 && score < 3)
        {
            timelimit -= Time.deltaTime;
        }
        else if (timelimit <= 0)
        {
            WinText.text = "You Lose ! ";
            Application.Quit();
        }

        timer.text = string.Format("{0:N2}", timelimit);     
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("coin"))
        {
            other.gameObject.SetActive(false);
            score += 1;
            setCountText();
        }
    }
    void setCountText()
    {
        WinText.text = "";
        CountText.text = "Score : " + score.ToString();
        if (score >= 3)
        {
            WinText.text = "You Win ! ";
            Application.Quit();
        }
    }

}

