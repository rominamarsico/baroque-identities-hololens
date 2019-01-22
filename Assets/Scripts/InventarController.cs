using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class InventarController : MonoBehaviour
{
    void Awake()
    {
        //Debug.Log("HideInventar");
        // GetComponentInParent<MenuController>().InventaryObjects = GameObject.FindGameObjectsWithTag("Inventar");
        GetComponentInParent<MenuController>().HideInventar();
        //skeywordManager.StartKeywordRecognizer ();
    }

    void Start()
    {
        StartCoroutine(GetText());
    }



    IEnumerator GetText()
    {
        UnityWebRequest www = new UnityWebRequest("https://baroque-identities.firebaseio.com/messages/original/.json?print=pretty");
        www.downloadHandler = new DownloadHandlerBuffer();
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log("Error: ");
            Debug.Log(www.error);
        }
        else
        {
            // Show results as text
            //Debug.Log("JSON from realtime database: ");
            //Debug.Log(www.downloadHandler.text);

            // Or retrieve results as binary data
            //byte[] results = www.downloadHandler.data;
        }
    }
}