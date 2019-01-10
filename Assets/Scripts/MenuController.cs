using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class MenuController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(GetText());
    }

    IEnumerator GetText()
    {
        UnityWebRequest wwwInventar = new UnityWebRequest("https://baroque-identities.firebaseio.com/Inventar/val/.json?print=pretty");
        wwwInventar.downloadHandler = new DownloadHandlerBuffer();
        yield return wwwInventar.SendWebRequest();

        UnityWebRequest wwwCharacter = new UnityWebRequest("https://baroque-identities.firebaseio.com/Character/val/.json?print=pretty");
        wwwCharacter.downloadHandler = new DownloadHandlerBuffer();
        yield return wwwCharacter.SendWebRequest();

        UnityWebRequest wwwMission = new UnityWebRequest("https://baroque-identities.firebaseio.com/Mission/val/.json?print=pretty");
        wwwMission.downloadHandler = new DownloadHandlerBuffer();
        yield return wwwMission.SendWebRequest();

        if (wwwInventar.isNetworkError || wwwInventar.isHttpError || wwwMission.isNetworkError || wwwMission.isHttpError || wwwCharacter.isNetworkError || wwwCharacter.isHttpError)
        {
            Debug.Log("Error: ");
            Debug.Log(wwwInventar.error);
            Debug.Log(wwwMission.error);
            Debug.Log(wwwCharacter.error);


            if (wwwInventar.downloadHandler.data != null)
            {
                // Show results as text
                Debug.Log("JSON from realtime database: ");
                Debug.Log(wwwInventar.downloadHandler.text);

                // Or retrieve results as binary data
                byte[] results = wwwInventar.downloadHandler.data;
            }
            else if (wwwCharacter.downloadHandler.data != null)
            {
                // Show results as text
                Debug.Log("JSON from realtime database: ");
                Debug.Log(wwwCharacter.downloadHandler.text);

                // Or retrieve results as binary data
                byte[] results = wwwCharacter.downloadHandler.data;
            }
            else if (wwwMission.downloadHandler.data != null)
            {
                // Show results as text
                Debug.Log("JSON from realtime database: ");
                Debug.Log(wwwMission.downloadHandler.text);

                // Or retrieve results as binary data
                byte[] results = wwwMission.downloadHandler.data;

            }
            else
            {
                Debug.Log("No MenueController is triggert.");
            }
        }

    }
}