using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class Timewarp : MonoBehaviour
{

    public GameObject firstWorldParent;
    public GameObject secondWorldParent;

    public Transform particleLocation;
    public GameObject particleObject;

    public PostProcessVolume volume;

    ChromaticAberration cromAb;
    LensDistortion lensDis;

    public GameObject flash;

    private void Start()
    {
        volume.profile.TryGetSettings(out cromAb);
        volume.profile.TryGetSettings(out lensDis);
    }

    private void Update()
    {      
        if (Input.GetKeyDown(KeyCode.E))
        {
            SwapWorlds();
        }

        if (cromAb.intensity.value >= 0)
        {
            cromAb.intensity.value -= 0.5f * Time.deltaTime;
        }

        if (lensDis.intensity.value <= 0)
        {
            lensDis.intensity.value += 60 * Time.deltaTime;
        }


    }

    public void SwapWorlds()
    {

        firstWorldParent.SetActive(!firstWorldParent.activeSelf);
        secondWorldParent.SetActive(!secondWorldParent.activeSelf);

        GameObject.Instantiate(particleObject, particleLocation.position, particleLocation.rotation);
        GameObject.Instantiate(flash, FindObjectOfType<Canvas>().transform.position, new Quaternion(), FindObjectOfType<Canvas>().transform);

        TriggerPostEffect();
    }

    void TriggerPostEffect()
    {
        cromAb.intensity.value = 1;
        lensDis.intensity.value = -60;
        
    }


}
