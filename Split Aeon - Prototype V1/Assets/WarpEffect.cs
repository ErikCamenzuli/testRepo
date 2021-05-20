using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpEffect : MonoBehaviour
{

    float decayTime = 5;

    void Update()
    {
        decayTime -= Time.deltaTime;

        if (decayTime < 0)
        {
            GameObject.Destroy(this.gameObject);
        }
    }
}
