using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class MenuController : MonoBehaviour
{

    /*public InventarController myInventarController;
    public CharactersController myCharacterController;
    public MissionController myMissionController;*/
    //Inventar von Beginn an
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
        //StartCoroutine(GetText());

    }

    void Update()
    {
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
        }

        string inventar = wwwInventar.downloadHandler.text;
        string mission = wwwMission.downloadHandler.text;
        string character = wwwCharacter.downloadHandler.text;
        /*Debug.Log(inventar.Length);
        Debug.Log(mission.Length);
        Debug.Log(character.Length);*/

        if (inventar.Contains("Inventar")) // inventar.Length == 11, always 3 letter more that the actual word has (Inventar = 7 + 3 = 11)
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
        }

    }

    public void Inventar (){
        portraitludwig.SetActive(true);
        pinsel.SetActive(true);
        rotehand.SetActive(true);
        Debug.Log("Inventar Function");
    }

    public void Characters () {
        Debug.Log("Characters");
    }

    public void Mission () {
        Debug.Log("Mission");
    }
}