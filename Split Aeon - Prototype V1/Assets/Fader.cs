using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fader : MonoBehaviour
{

    public Image fade;
    float decay = 2;

    void Start()
    {
        fade.color = Color.white;
        fade.CrossFadeAlpha(0, 1, true);
    }

    void Update()
    {
        decay -= Time.deltaTime;

        if (decay < 0)
        {
            Destroy(this.gameObject);
        }
    }
}
