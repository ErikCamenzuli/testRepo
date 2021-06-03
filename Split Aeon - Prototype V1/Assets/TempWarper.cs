using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempWarper : MonoBehaviour
{
    [Header("Teleporting")]
    public Timewarp warp;

    public void Teleport()
    {
        warp.SwapWorlds();
    }
}
