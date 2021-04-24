using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    void Update() //공굴리기
    {
        transform.Rotate(new Vector3(0, 30, 0) * Time.deltaTime);
    }

}
