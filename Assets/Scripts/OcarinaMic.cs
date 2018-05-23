using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OcarinaMic : MonoBehaviour {

    //Important we attach an audio mixer to the audio source used for the mic. This is to mute the feedback so we don't hear it.
    //Muting the audio source will mute the output, which we don't want.

    #region Audio Sources

    [Header("Audio Sources")]
    public AudioSource A;
    public AudioSource CUp;
    public AudioSource CDown;
    public AudioSource CLeft;
    public AudioSource CRight;

    #endregion

    #region Mic Config

    [Header("Mic Configuration")]
    public float MinimumPlayTime = .5f;    
    public AudioSource MicSource;
    public float sensitivity = 100;
    private float loudness = 0;
    public float Threshold = 5;

    #endregion

    #region Private variables

    private bool aPressed;
    private bool cDownPressed;
    private bool cUpPressed;
    private bool cLeftPressed;
    private bool cRightPressed;
    private string microphone;
    private int audioSampleRate = 44100;

    #endregion

    #region ButtonDown

    public void AButtonDown()
    {
        aPressed = true;
    }

    public void CUpButtonDown()
    {
        cUpPressed = true;
    }

    public void CDownButtonDown()
    {
        cDownPressed = true;
    }

    public void CLeftButtonDown()
    {
        cLeftPressed = true;
    }

    public void CRightButtonDown()
    {
        cRightPressed = true;
    }

    #endregion

    #region Button Up

    public void AButtonUP()
    {
        aPressed = false;
        StopPlayingNote(A);
    }
    
    public void CUpButtonUp()
    {
        cUpPressed = false;
        StopPlayingNote(CUp);
    }

    public void CDownButtonUp()
    {
        cDownPressed = false;
        StopPlayingNote(CDown);
    }

    public void CLeftButtonUp()
    {
        cLeftPressed = false;
        StopPlayingNote(CLeft);
    }

    public void CRightButtonUp()
    {
        cRightPressed = false;
        StopPlayingNote(CRight);
    }

    #endregion

    #region Update

    // Update is called once per frame
    private void Update () {
        loudness = GetAveragedVolume() * sensitivity;

        if(loudness > Threshold)
        {            
            if (aPressed)
            {                
                PlayNoteOnce(A);
            }

            if (cDownPressed)
            {
                PlayNoteOnce(CDown);
            }

            if (cUpPressed)
            {
                PlayNoteOnce(CUp);
            }

            if (cLeftPressed)
            {
                PlayNoteOnce(CLeft);
            }

            if (cRightPressed)
            {
                PlayNoteOnce(CRight);
            }
        }
    }

    #endregion

    private void PlayNoteOnce(AudioSource source)
    {
        if(source.isPlaying == false)
        {
            source.mute = false;
            source.Play();
        }
        
    }

    private void StopPlayingNote(AudioSource source)
    {
        StartCoroutine(WaitForMinTime(source));
    }

    //Cutting the note off after a time sounds better than an immediate cut off when the finger/mouse is lifted.
    IEnumerator WaitForMinTime(AudioSource note)
    {
        yield return new WaitForSeconds(MinimumPlayTime);
        note.mute = true;
    }

    #region Microphone

    private float GetAveragedVolume()
    {
        float[] data = new float[256];
        MicSource.GetOutputData(data, 0);

        float a = 0;

        foreach (float s in data)
        {
            a += Mathf.Abs(s);
        }
        return a / 256;
    }

    public void UpdateMicrophone()
    {
        MicSource.Stop();
        MicSource.clip = Microphone.Start(microphone, true, 10, audioSampleRate);
        MicSource.loop = true;
        Debug.Log(Microphone.IsRecording(microphone).ToString());

        if (Microphone.IsRecording(microphone))
        {
            while (!(Microphone.GetPosition(microphone) > 0))
            {
            }

            Debug.Log("Recording started with " + microphone);

            MicSource.Play();
        }
        else
        {
            Debug.Log(microphone + " is not working!");
        }
    }

    public void StopMicrophone()
    {
        Microphone.End(microphone);
    }    

    #endregion

}
