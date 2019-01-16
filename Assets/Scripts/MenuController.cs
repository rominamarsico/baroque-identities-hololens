using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
//using Academy.HoloToolkit.Unity; --> Müssen wir noch runterladen

public class MenuController : MonoBehaviour
{

    /*public InventarController myInventarController;
    public CharactersController myCharacterController;
    public MissionController myMissionController;*/
    //Inventar von Beginn an
    public GameObject CursorArrowRechts;
    public GameObject CursorArrowLinks;
    public GameObject pinsel;
    public GameObject portraitludwig;
    public GameObject rotehand;
    public GameObject gun_HvG;
    public GameObject gun_KvP;
    public GameObject gun_LvH;
    public GameObject windkompass_anim;

    public GameObject[] InventaryObjects;

    //Inventar Atelier

    //Inventar Tatort

    public void HideInventar(){
        foreach (GameObject _gameObject in InventaryObjects)
        {
            gameObject.SetActive(false);
        }
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
        //StartCoroutine(GetText());

        Debug.Log(InventaryObjects.Length);
    }

    void Update()
    {
        StartCoroutine(GetText());
    }
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
        /*Debug.Log(inventar.Length);
        Debug.Log(mission.Length);
        Debug.Log(character.Length);*/

        if (inventar.Contains("Inventar"))
        {
            // Show results as text
            //Debug.Log("Inventar database text: ");
            Debug.Log(wwwInventar.downloadHandler.text);

            // Or retrieve results as binary data
            //byte[] results = wwwInventar.downloadHandler.data;
            Inventar();
        }
        else if (character.Contains("Character")) // character.Length == 12
        {
            // Show results as text
            //Debug.Log("Character database text: ");
            Debug.Log(wwwCharacter.downloadHandler.text);

            // Or retrieve results as binary data
            //byte[] results = wwwCharacter.downloadHandler.data;
            Characters();
        }
        else if (mission.Contains("Mission")) // mission.Length == 10
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
            HideInventar();
        }

    }

    public void Arrows ()
    {
        CursorArrowRechts.transform.position = new Vector3(8, 0, 9);
        CursorArrowLinks.transform.position = new Vector3(-8, 0, 9);
    }

    public void Inventar () {
        Arrows();
        var ClickCounterRight = GetComponentInChildren<CursorArrowRechts>().ClickRight;
        Debug.Log(ClickCounterRight);
        var MenuObjectOne = InventaryObjects[0];
        MenuObjectOne.transform.position = new Vector3(-4, 0, 9);
        MenuObjectOne.SetActive(true);

        var MenuObjectTwo = InventaryObjects[1];
        MenuObjectTwo.SetActive(true);
        MenuObjectTwo.transform.position = new Vector3(0, 0, 9);

        var MenuObjectThree = InventaryObjects[2];
        MenuObjectThree.SetActive(true);
        MenuObjectThree.transform.position = new Vector3(4, 0, 9);

        //void on Select Pfeil rechts evtl 3 for schleifen, die so oft laufen, wie eine ClickCounter ausgeführt wurde. 
        //Durch die dann den ArrayIndex der drei Objecte + drei zählt. und dann Set Active setzt und dir position setzt. 
        //Die Ursprünglichen drei Objecte obendrüber werden dann False gesetzt
        //Gleiche umgekehrt mit Pfeil zurück
        //Sobald es nicht nochmal weiter geht dürfen die Pfeile nicht mehr angezeigt werden
        //Muss noch einen else part geben, wenn nur zwei oder ein Object bei der Zählung übrig bleiben, sodass dann kein Fehler kommt, dass Objekte fehlen. 
        
      
        Debug.Log("Inventar Function");
    }

    public void SelectPortraitLudwig()
    {
        Debug.Log("Click on portrait of Ludwig");
    }

    public void Characters () {
        Debug.Log("Characters");
    }

    public void Mission () {
        Debug.Log("Mission");
    }
}