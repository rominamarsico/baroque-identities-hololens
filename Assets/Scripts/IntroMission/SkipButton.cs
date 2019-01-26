using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipButton : MonoBehaviour {

    public AudioSource ButtonClick;

    public void OnSkipButtonClick()
    {
        ButtonClick.Play();
        SceneManager.LoadScene("SampleScene");
    }
}
