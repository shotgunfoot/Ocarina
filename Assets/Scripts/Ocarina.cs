using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ocarina : MonoBehaviour
{
    #region Public Variables

    [Header("Audio Sources")]
    public AudioSource A;
    public AudioSource CUp;
    public AudioSource CDown;
    public AudioSource CLeft;
    public AudioSource CRight;

    #endregion

    private ToggleMic mode;

    private void Start()
    {
        if(mode == null)
        {
            mode = GetComponent<ToggleMic>();
        }        
    }

    #region PlayNotes

    [Header("Configuration")]
    public float MinimumPlayTime = .5f;

    private void PlayNoteA()
    {
        PlayNote(A);
    }

    private void PlayNoteCDown()
    {
        PlayNote(CDown);        
    }

    private void PlayNoteCUp()
    {
        PlayNote(CUp);        
    }

    private void PlayNoteCLeft()
    {
        PlayNote(CLeft);        
    }

    private void PlayNoteCRight()
    {
        PlayNote(CRight);       
    }

    #endregion

    #region StopNotes

    private void StopNoteA()
    {
        StartCoroutine(WaitForMinTime(A));        
    }

    private void StopNoteCUp()
    {
        StartCoroutine(WaitForMinTime(CUp));        
    }

    private void StopNoteCDown()
    {
        StartCoroutine(WaitForMinTime(CDown));        
    }

    private void StopNoteCLeft()
    {
        StartCoroutine(WaitForMinTime(CLeft));       
    }

    private void StopNoteCRight()
    {
        StartCoroutine(WaitForMinTime(CRight));        
    }

    #endregion

    private void PlayNote(AudioSource note)
    {
        if (!mode.UsingMicrophone())
        {
            note.mute = false;
            note.Play();
        }
    }

    public void PlayANote(GameObject source)
    {
        string note = source.name;
        switch (note)
        {
            case "A":
                PlayNoteA();
                break;
            case "C_UP":
                PlayNoteCUp();
                break;
            case "C_DOWN":
                PlayNoteCDown();
                break;
            case "C_RIGHT":
                PlayNoteCRight();
                break;
            case "C_LEFT":
                PlayNoteCLeft();
                break;
            default:
                Debug.Log("No Note Found to Play.");
                break;
        }
    }

    public void StopANote(GameObject source)
    {

        string note = source.name;
        switch (note)
        {
            case "A":
                StopNoteA();
                break;
            case "C_UP":
                StopNoteCUp();
                break;
            case "C_DOWN":
                StopNoteCDown();
                break;
            case "C_RIGHT":
                StopNoteCRight();
                break;
            case "C_LEFT":
                StopNoteCLeft();
                break;
            default:
                Debug.Log("No Note Found to Stop.");
                break;
        }
    }

    //Cutting the note off after a time sounds better than an immediate cut off when the finger/mouse is lifted.
    IEnumerator WaitForMinTime(AudioSource note)
    {
        yield return new WaitForSeconds(MinimumPlayTime);
        note.mute = true;
    }

}
