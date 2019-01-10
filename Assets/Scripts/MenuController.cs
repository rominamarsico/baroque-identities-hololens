using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class MenuController : MonoBehaviour {

    public InventarController myInventarController;
    public CharactersController myCharacterController;
    public MissionController myMissionController;

    // Use this for initialization
    void Start () {
        myInventarController = GameObject.Find("InventarController").GetComponent<InventarController>();
        myCharacterController = GameObject.Find("CharactersController").GetComponent<CharactersController>();
        myMissionController = GameObject.Find("MissionController").GetComponent<MissionController>();
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

                //myInventarController. Funktion die Inventar im Script InventarController aufruft
            }
            else if (wwwCharacter.downloadHandler.data != null)
            {
                // Show results as text
                Debug.Log("JSON from realtime database: ");
                Debug.Log(wwwCharacter.downloadHandler.text);

                // Or retrieve results as binary data
                byte[] results = wwwCharacter.downloadHandler.data;
                //myCharactersController. Funktion die Character im Script CharactersController aufruft
            }
            else if (wwwMission.downloadHandler.data != null)
            {
                // Show results as text
                Debug.Log("JSON from realtime database: ");
                Debug.Log(wwwMission.downloadHandler.text);

                // Or retrieve results as binary data
                byte[] results = wwwMission.downloadHandler.data;
                //myMissionController. Funktion die Mission im Script MissionController aufruft

            }
            else
            {
                Debug.Log("No MenueController is triggert.");
            }
        }

    }
}