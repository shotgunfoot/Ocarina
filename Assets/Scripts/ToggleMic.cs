using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleMic : MonoBehaviour {

    [Header("Icons")]
    public Sprite Mic;
    public Sprite NoMic;
    public Image MicButton;

    private bool useMicrophone;

    public bool UsingMicrophone()
    {
        return useMicrophone;
    }

    public void ToggleUsingMicrophone()
    {
        useMicrophone = !useMicrophone;

        if (useMicrophone)
        {
            MicButton.sprite = Mic;
            GetComponent<OcarinaMic>().enabled = true;
            GetComponent<OcarinaMic>().UpdateMicrophone();
            GetComponent<Ocarina>().enabled = false;
        }
        else
        {
            MicButton.sprite = NoMic;
            GetComponent<OcarinaMic>().StopMicrophone();
            GetComponent<OcarinaMic>().enabled = false;
            GetComponent<Ocarina>().enabled = true;
        }
    }
}
