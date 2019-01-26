﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour {

    public AudioSource ButtonClick;

    public void OnPlayButtonClick()
    {
        ButtonClick.Play();
        SceneManager.LoadScene("IntroMission");
    }
}
