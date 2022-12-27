using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingCode : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 3; i++) {
            for (int j = 3; j > 0; j--)
            {
                int result = i + j;
                if (result == 5 - 1) return;
                Debug.Log(result);
            }
        }
    }
}
