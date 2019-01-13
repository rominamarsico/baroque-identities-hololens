using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class MenuController : MonoBehaviour
{

    /*public InventarController myInventarController;
    public CharactersController myCharacterController;
    public MissionController myMissionController;*/
    //Inventar Beginn
    public GameObject pinsel;
    public GameObject portraitludwig;
    public GameObject rotehand;
    public GameObject gun_HvG;
    public GameObject gun_KvP;
    public GameObject gun_LvH;
    public GameObject windkompass_anim;

    public GameObject[] InventaryObjects;

    public Vector3 MenuPos1 = new Vector3(1, 0, 1);
    public Vector3 MenuPos2 = new Vector3(0, 0, 1);
    public Vector3 MenuPos3 = new Vector3(-1, 0, 1);

    //Inventar Atelier

    //Inventar Tatort

    public void HideInventar(){
        portraitludwig.SetActive(false);
        pinsel.SetActive(false);
        rotehand.SetActive(false);
        gun_HvG.SetActive(false);
        gun_KvP.SetActive(false);
        gun_LvH.SetActive(false);
        windkompass_anim.SetActive(false);
    }
    // Use this for initialization
    void Start () {
        InventaryObjects = GameObject.FindGameObjectsWithTag("Inventar");
        for (int i = 0; i < InventaryObjects.Length; i++)
        {
            Debug.Log("Inventar Number " + i + " is named " + InventaryObjects[i].name);

        }
        /* myInventarController = GameObject.Find("InventarController").GetComponent<InventarController>();
         myCharacterController = GameObject.Find("CharactersController").GetComponent<CharactersController>();
         myMissionController = GameObject.Find("MissionController").GetComponent<MissionController>();*/

    }

    void Update()
    {
        StartCoroutine(GetText());
    }
    // TODO:
    //Switch für newInventar stuff add at first position to the Array --> So stimmen Unten immer die ersten drei Objekte.

    IEnumerator GetText()
    {
        UnityWebRequest wwwInventar = new UnityWebRequest("https://baroque-identities.firebaseio.com/Inventar/val/.json?print=pretty");
        wwwInventar.downloadHandler = new DownloadHandlerBuffer();
        wwwInventar.chunkedTransfer = false;
        yield return wwwInventar.SendWebRequest();

        UnityWebRequest wwwCharacter = new UnityWebRequest("https://baroque-identities.firebaseio.com/Character/val/.json?print=pretty");
        wwwCharacter.downloadHandler = new DownloadHandlerBuffer();
        wwwCharacter.chunkedTransfer = false;
        yield return wwwCharacter.SendWebRequest();

        UnityWebRequest wwwMission = new UnityWebRequest("https://baroque-identities.firebaseio.com/Mission/val/.json?print=pretty");
        wwwMission.downloadHandler = new DownloadHandlerBuffer();
        wwwMission.chunkedTransfer = false;
        yield return wwwMission.SendWebRequest();

        if (wwwInventar.isNetworkError || wwwInventar.isHttpError || wwwMission.isNetworkError || wwwMission.isHttpError || wwwCharacter.isNetworkError || wwwCharacter.isHttpError)
        {
            Debug.Log("Error: ");

            Debug.Log(wwwInventar.error);
            Debug.Log(wwwMission.error);
            Debug.Log(wwwCharacter.error);

            Debug.Log(wwwInventar.responseCode);
            Debug.Log(wwwMission.responseCode);
            Debug.Log(wwwCharacter.responseCode);
        }

        string inventar = wwwInventar.downloadHandler.text;
        string mission = wwwMission.downloadHandler.text;
        string character = wwwCharacter.downloadHandler.text;

        if (inventar.Contains("Inventar"))
        {
            // Show results as text
            //Debug.Log("Inventar database text: ");
            Debug.Log(wwwInventar.downloadHandler.text);

            // Or retrieve results as binary data
            //byte[] results = wwwInventar.downloadHandler.data;
            Inventar();
        }
        else if (character.Contains("Character"))
        {
            // Show results as text
            //Debug.Log("Character database text: ");
            Debug.Log(wwwCharacter.downloadHandler.text);

            // Or retrieve results as binary data
            //byte[] results = wwwCharacter.downloadHandler.data;
            Characters();
        }
        else if (mission.Contains("Mission"))
        {
            // Show results as text
            //Debug.Log("Mission database text: ");
            Debug.Log(wwwMission.downloadHandler.text);

            // Or retrieve results as binary data
            //byte[] results = wwwMission.downloadHandler.data;
            Mission();
        }
        else
        {
            Debug.Log("No menu controller is triggert.");
        }

    }

    public void Inventar (){
        var MenuObjectOne = InventaryObjects[0];
        MenuObjectOne.transform.position = MenuPos1;
        MenuObjectOne.SetActive(true);

        var MenuObjectTwo = InventaryObjects[1];
        MenuObjectTwo.transform.position = MenuPos2;
        MenuObjectTwo.SetActive(true);

        var MenuObjectThree = InventaryObjects[2];
        MenuObjectThree.transform.position = MenuPos3;
        MenuObjectThree.SetActive(true);

        // TODO:
        //void on Select Pfeil rechts evtl 3 for schleifen, die so oft laufen, wie eine ClickCounter ausgeführt wurde. 
        //Durch die dann den ArrayIndex der drei Objecte + drei zählt. und dann Set Active setzt und dir position setzt. 
        //Die Ursprünglichen drei Objecte obendrüber werden dann False gesetzt
        //Gleiche umgekehrt mit Pfeil zurück
        //Sobald es nicht nochmal weiter geht dürfen die Pfeile nicht mehr angezeigt werden
        //Muss noch einen else part geben, wenn nur zwei oder ein Object bei der Zählung übrig bleiben, sodass dann kein Fehler kommt, dass Objekte fehlen. 
        
       /* portraitludwig.SetActive(true);
        pinsel.SetActive(true);
        rotehand.SetActive(true);*/
        Debug.Log("Inventar Function");
    }

    public void Characters () {
        Debug.Log("Characters");
    }

    public void Mission () {
        Debug.Log("Mission");
    }
}