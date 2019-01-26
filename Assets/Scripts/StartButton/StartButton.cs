using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour {

    public AudioSource ButtonClick;

    void Awake()
    {
        StartCoroutine(ClearNfcInput());
    }

    IEnumerator ClearNfcInput()
    {
        UnityWebRequest wwwClearNfcInput = new UnityWebRequest("https://us-central1-baroque-identities.cloudfunctions.net/addMessage?text=");
        wwwClearNfcInput.downloadHandler = new DownloadHandlerBuffer();
        wwwClearNfcInput.chunkedTransfer = false;
        yield return wwwClearNfcInput.SendWebRequest();
        Debug.Log("clear nfc input");
    }

    public void OnPlayButtonClick()
    {
        ButtonClick.Play();
        SceneManager.LoadScene("IntroMission");
    }
}
