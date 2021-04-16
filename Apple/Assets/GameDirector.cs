﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  // UI를 사용할 때는 잊지 않도록 주의!!

public class GameDirector : MonoBehaviour
{
    GameObject timerText;
    float time = 60.0f;

    void Start()
    {
        this.timerText = GameObject.Find("Time");
    }

    void Update()
    {
        this.time -= Time.deltaTime;
        this.timerText.GetComponent<Text>().text = this.time.ToString("F1");
    }
}
