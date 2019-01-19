using UnityEngine;
using System.Collections;
using UnityEngine.Networking;


public class MenuController : MonoBehaviour
{

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
    public int counter;

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
   
        Debug.Log(InventaryObjects.Length);
    }

    void Update()
    {
        StartCoroutine(GetText());
    }
    //Switch für newInventar stuff add at first position to the Array --> So stimmen Unten immer die ersten drei Objekte.

    IEnumerator GetText()
    {
        UnityWebRequest wwwInventar = new UnityWebRequest("https://baroque-identities.firebaseio.com/Inventar/val/.json?print=pretty/");
        wwwInventar.downloadHandler = new DownloadHandlerBuffer();
        wwwInventar.chunkedTransfer = false;
        yield return wwwInventar.SendWebRequest();

        UnityWebRequest wwwCharacter = new UnityWebRequest("https://baroque-identities.firebaseio.com/Character/val/.json?print=pretty/");
        wwwCharacter.downloadHandler = new DownloadHandlerBuffer();
        wwwCharacter.chunkedTransfer = false;
        yield return wwwCharacter.SendWebRequest();

        UnityWebRequest wwwMission = new UnityWebRequest("https://baroque-identities.firebaseio.com/Mission/val/.json?print=pretty/");
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
        CursorArrowRechts.transform.position = new Vector3(2, -2, 9);
        CursorArrowLinks.transform.position = new Vector3(-2, -2, 9);
    }

    public void Inventar () {
        Arrows();
        var ClickCounterRight = CursorArrowRechts.GetComponent<CursorArrowRechts>().ClickRight;
        var CounterObject = CursorArrowRechts.GetComponent<CursorArrowRechts>().ObjectCounter;

        for (int i = ClickCounterRight; i <=InventaryObjects.Length/3; i++)
        {
        
            if (0 + CounterObject < InventaryObjects.Length)
            {
                var MenuObjectOne = InventaryObjects[0 + CounterObject];
                MenuObjectOne.transform.position = new Vector3(-2, 0, 9);
                MenuObjectOne.SetActive(MenuObjectOne);
                for (int a = 0; a < 0 + CounterObject; a++)
                {
                    InventaryObjects[a].SetActive(false);
                }
            }
            else
                CursorArrowRechts.SetActive(false);

            if (1 + CounterObject < InventaryObjects.Length)
            {
                var MenuObjectTwo = InventaryObjects[1 + CounterObject];
                MenuObjectTwo.SetActive(MenuObjectTwo);
                MenuObjectTwo.transform.position = new Vector3(0, 0, 9);
            }
            else
                CursorArrowRechts.SetActive(false);

            if (2 + CounterObject < InventaryObjects.Length)
            {
                var MenuObjectThree = InventaryObjects[2 + CounterObject];
                MenuObjectThree.SetActive(MenuObjectThree);
                MenuObjectThree.transform.position = new Vector3(2, 0, 9);
            }
            else
                CursorArrowRechts.SetActive(false);

        }

   
        //Gleiche umgekehrt mit Pfeil zurück


        Debug.Log("Inventar Function");
    }

    public void SelectPortraitLudwig()
    {
        Debug.Log("Click on portrait of Ludwig");
    }

    public void SelectPinsel()
    {
        Debug.Log("Click on pinsel");
    }

    public void SelectRoteHand()
    {
        Debug.Log("Click on rote Hand");
    }
    public void SelectGunHvG()
    {
        Debug.Log("Click on GunHvG");
    }
    public void SelectGunKvP()
    {
        Debug.Log("Click on GunKvP");
    }
    public void SelectGunLvH()
    {
        Debug.Log("Click on GunLvH");
    }
    public void SelectWindcompass()
    {
        Debug.Log("Click on Windcompass");
    }

    public void Characters () {
        Debug.Log("Characters");
    }

    public void Mission () {
        Debug.Log("Mission");
    }
}