using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class Timewarp : MonoBehaviour
{
    public Transform particleLocation;
    public GameObject particleObject;

    public PostProcessVolume volume;

    ChromaticAberration cromAb;
    LensDistortion lensDis;

    public GameObject flash;

    bool isInPresent = true;

    public CharacterController controller;
    public GameObject player;

    private void Start()
    {
        volume.profile.TryGetSettings(out cromAb);
        volume.profile.TryGetSettings(out lensDis);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            //SwapWorlds();
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

        if (isInPresent)
        {
            controller.enabled = false;
            player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y - 50, player.transform.position.z);
            controller.enabled = true;
        }
        else
        {
            controller.enabled = false;
            player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 50, player.transform.position.z);
            controller.enabled = true;
        }

        TriggerTeleportEffect();

        isInPresent = !isInPresent;
    }

    void TriggerTeleportEffect()
    {
        GameObject.Instantiate(particleObject, particleLocation.position, particleLocation.rotation);
        GameObject.Instantiate(flash, FindObjectOfType<Canvas>().transform.position, new Quaternion(), FindObjectOfType<Canvas>().transform);

        cromAb.intensity.value = 1;
        lensDis.intensity.value = -60;
    }
}
