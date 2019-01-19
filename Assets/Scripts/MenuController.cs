using UnityEngine;
using System.Collections;
using System.Collections.Generic;
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

    public GameObject[] InventaryObjects = new GameObject[12];
    IList<GameObject> newInventoryObjects;

    public GameObject skizzenbuch2;
    public GameObject Schatulle;

    //Inventar Atelier

    //Inventar Tatort

    // Use this for initialization
    void Start () {
        InventaryObjects = GameObject.FindGameObjectsWithTag("Inventar");
        newInventoryObjects = new List<GameObject>(InventaryObjects);

        for (int i = 0; i < InventaryObjects.Length; i++)
        {
            Debug.Log("Inventar Number " + i + " is named " + InventaryObjects[i].name);
        }
        Debug.Log(InventaryObjects.Length);
    }

    void Update()
    {
        StartCoroutine(GetClickedMenu());
        StartCoroutine(GetNfcTag());
    }
    //Switch für newInventar stuff add at first position to the Array --> So stimmen unten immer die ersten drei Objekte.

    IEnumerator GetClickedMenu()
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
            //Debug.Log("Inventar database text: ");
            Debug.Log(wwwInventar.downloadHandler.text);
            Inventar();
        }
        else if (character.Contains("Character"))
        {
            //Debug.Log("Character database text: ");
            Debug.Log(wwwCharacter.downloadHandler.text);
            Characters();
            HideInventar();
        }
        else if (mission.Contains("Mission"))
        {
            //Debug.Log("Mission database text: ");
            Debug.Log(wwwMission.downloadHandler.text);
            Mission();
            HideInventar();
        }
        else
        {
            Debug.Log("No menu controller is triggert.");
            HideInventar();
        }
    }

    IEnumerator GetNfcTag ()
    {
        UnityWebRequest wwwNfc = new UnityWebRequest("https://baroque-identities.firebaseio.com/nfcTag/val/.json?print=pretty/");
        wwwNfc.downloadHandler = new DownloadHandlerBuffer();
        wwwNfc.chunkedTransfer = false;
        yield return wwwNfc.SendWebRequest();

        string nfcTag = wwwNfc.downloadHandler.text;

        if (nfcTag.Contains("box") && !newInventoryObjects.Contains(Schatulle))
        {
            newInventoryObjects.Add(Schatulle);
        } else if (nfcTag.Contains("skizzenbuch") && !newInventoryObjects.Contains(skizzenbuch2))
        {
            newInventoryObjects.Add(skizzenbuch2);
        }
        else
        {
            Debug.Log("no nfc tag scanned");
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

        for (int i = ClickCounterRight; i <=newInventoryObjects.Count/3; i++)
        {
            if (0 + CounterObject < newInventoryObjects.Count)
            {
                var MenuObjectOne = newInventoryObjects[0 + CounterObject];
                MenuObjectOne.transform.position = new Vector3(-2, 0, 9);
                MenuObjectOne.SetActive(MenuObjectOne);
                for (int a = 0; a < 0 + CounterObject; a++)
                {
                    newInventoryObjects[a].SetActive(false);
                }
            }
            else
                CursorArrowRechts.SetActive(false);

            if (1 + CounterObject < newInventoryObjects.Count)
            {
                var MenuObjectTwo = newInventoryObjects[1 + CounterObject];
                MenuObjectTwo.SetActive(MenuObjectTwo);
                MenuObjectTwo.transform.position = new Vector3(0, 0, 9);
            }
            else
                CursorArrowRechts.SetActive(false);

            if (2 + CounterObject < newInventoryObjects.Count)
            {
                var MenuObjectThree = newInventoryObjects[2 + CounterObject];
                MenuObjectThree.SetActive(MenuObjectThree);
                MenuObjectThree.transform.position = new Vector3(2, 0, 9);
            }
            else
                CursorArrowRechts.SetActive(false);
        }
        //Gleiche umgekehrt mit Pfeil zurück

        Debug.Log("Inventar Function");
    }

    public void HideInventar()
    {
        foreach (GameObject _gameObject in InventaryObjects)
        {
            _gameObject.SetActive(false);
        }
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