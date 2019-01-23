using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class MenuController : MonoBehaviour
{
    public bool Left = false;
    public bool right = false;
    public bool IsInventar = true;
    public GameObject CursorArrowRechts;
    public GameObject CursorArrowLinks;
    public GameObject[] InventaryObjects;
    public IList<GameObject> newInventoryObjects;
    public IList<GameObject> TextObjects;

    public AudioSource menu;
    public AudioSource scan;

    //Inventar von Beginn an
    public GameObject pinsel;
    public GameObject portraitludwig;
    public GameObject rotehand;
    public GameObject gun_HvG;
    public GameObject gun_KvP;
    public GameObject gun_LvH;
    public GameObject windkompass_anim;

    public int ObjectCounter;

    //Inventar Atelier
    public GameObject skizzenbuch;
    public GameObject skizze_mit_frau;
    public GameObject archive;
    public GameObject Schatulle;
    public GameObject Brief_August;
    public GameObject Brief_Februar;
    public GameObject Brief_Juni;
    public GameObject Brief_Maerz;
    public GameObject Brief_Mai_1741;
    public GameObject Brief_Mai_1754;
    public GameObject Zettel_am_Kompass;
    public GameObject zweiDamen;
    public GameObject Deer_Brosche;

    //Inventar Tatort
    public GameObject Geburtsurkunde;
    public GameObject Tuch;

    //Inventar Texte
    public GameObject Panel;
    public GameObject PortraitLudwigText;
    public GameObject PinselText;
    public GameObject RoteHandText;
    public GameObject GunHvGText;
    public GameObject GunKvPText;
    public GameObject GunLvHText;
    public GameObject WindkompassText;
    public GameObject SkizzenbuchText;
    public GameObject SkizzeMitFrauText;
    public GameObject ArchiveText;
    public GameObject BriefAugustText;
    public GameObject SchatulleText;
    public GameObject BriefFebruarText;
    public GameObject BriefMai1741Text;
    public GameObject BriefMai1754Text;
    public GameObject BriefMaerzText;
    public GameObject ZettelAmKompassText;
    public GameObject ZweiDamenText;
    public GameObject KugelText;
    public GameObject BriefJuniText;
    public GameObject TuchText;
    public GameObject GeburtsurkundeText;
    public GameObject DeerBroscheText;

    // Use this for initialization
    void Start () {
        InventaryObjects = GameObject.FindGameObjectsWithTag("Inventar");
        newInventoryObjects = new List<GameObject>(InventaryObjects);
        TextObjects = GameObject.FindGameObjectsWithTag("Text");

        for (int i = 0; i <TextObjects.Count; i++)
        {
            Debug.Log("Inventar Number " + i + " is named " + TextObjects[i].name);
        }

        StartCoroutine(ClearNfcInput());
        HideText();
    }

    void Update()
    {
        StartCoroutine(GetClickedMenu());
        StartCoroutine(GetNfcTag());
    }

    IEnumerator ClearNfcInput()
    {
        UnityWebRequest wwwClearNfcInput = new UnityWebRequest("https://us-central1-baroque-identities.cloudfunctions.net/addMessage?text=");
        wwwClearNfcInput.downloadHandler = new DownloadHandlerBuffer();
        wwwClearNfcInput.chunkedTransfer = false;
        yield return wwwClearNfcInput.SendWebRequest();
    }

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

        if (inventar.Contains("Inventar") || character.Contains("Character") || mission.Contains("Mission"))
        {
            //menu.Play();
        }

        if (inventar.Contains("Inventar"))
        {
            Debug.Log(wwwInventar.downloadHandler.text);
            OnTriggerInventar();
        }
        else if (character.Contains("Character"))
        {
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

        if (nfcTag.Contains("schatulle") && !newInventoryObjects.Contains(Schatulle))
        {
            newInventoryObjects.Add(Schatulle);
        }
        else if (nfcTag.Contains("skizzenbuch") && !newInventoryObjects.Contains(skizzenbuch))
        {
            newInventoryObjects.Add(skizzenbuch);
        }
        else if (nfcTag.Contains("skizzeFrau") && !newInventoryObjects.Contains(skizze_mit_frau))
        {
            newInventoryObjects.Add(skizze_mit_frau);
        } else if (nfcTag.Contains("archive") && !newInventoryObjects.Contains(archive))
        {
            newInventoryObjects.Add(archive);
        }
        else if (nfcTag.Contains("2damen") && !newInventoryObjects.Contains(zweiDamen))
        {
            newInventoryObjects.Add(zweiDamen);
        }
        else if (nfcTag.Contains("Deer") && !newInventoryObjects.Contains(Deer_Brosche))
        {
            newInventoryObjects.Add(Deer_Brosche);
        }
        else if (nfcTag.Contains("tuch") && !newInventoryObjects.Contains(Tuch))
        {
            newInventoryObjects.Add(Tuch);
        }
        else if (nfcTag.Contains("geburtsurkunde") && !newInventoryObjects.Contains(Geburtsurkunde))
        {
            newInventoryObjects.Add(Geburtsurkunde);
        }
        else if (nfcTag.Contains("zettelKompass") && !newInventoryObjects.Contains(Zettel_am_Kompass))
        {
            newInventoryObjects.Add(Zettel_am_Kompass);
        }
        else if (nfcTag.Contains("briefAugust") && !newInventoryObjects.Contains(Brief_August))
        {
            newInventoryObjects.Add(Brief_August);
        }
        else if (nfcTag.Contains("briefFebruar") && !newInventoryObjects.Contains(Brief_Februar))
        {
            newInventoryObjects.Add(Brief_Februar);
        }
        else if (nfcTag.Contains("briefJuni") && !newInventoryObjects.Contains(Brief_Juni))
        {
            newInventoryObjects.Add(Brief_Juni);
        }
        else if (nfcTag.Contains("briefMaerz") && !newInventoryObjects.Contains(Brief_Maerz))
        {
            newInventoryObjects.Add(Brief_Maerz);
        }
        else if (nfcTag.Contains("briefMai1741") && !newInventoryObjects.Contains(Brief_Mai_1741))
        {
            newInventoryObjects.Add(Brief_Mai_1741);
        }
        else if (nfcTag.Contains("skizzeFrau") && !newInventoryObjects.Contains(skizze_mit_frau))
        {
            newInventoryObjects.Add(skizze_mit_frau);
        }
        else if (nfcTag.Contains("briefMai1754") && !newInventoryObjects.Contains(Brief_Mai_1754))
        {
            newInventoryObjects.Add(Brief_Mai_1754);
        }
        else
        {
            Debug.Log("no nfc tag scanned");
        }
    }

    public void Arrows ()
    {
        CursorArrowRechts.transform.position = new Vector3(1, -1, 9);
        CursorArrowLinks.transform.position = new Vector3(-1, -1, 9);
    }

    public void HideArrows ()
    {
        CursorArrowRechts.SetActive(false);
        CursorArrowLinks.SetActive(false);
    }

    public void OnTriggerInventar(){
        if (IsInventar == true)
        {
            Inventar();
        }
        //else
            //Debug.Log("IsInventar is false");
    }
    public void TrueInventar(){
        IsInventar = true;
    }

    public void Inventar () {
        Arrows();
        HideText();
        var ClickCounterRight = CursorArrowRechts.GetComponent<CursorArrowRechts>().ClickRight;
        var ClickCounterLeft = CursorArrowLinks.GetComponent<CursorArrowLinks>().ClickLeft;
        var ClickCounter = ClickCounterRight - ClickCounterLeft;
        //Debug.Log(ClickCounterRight);
        if (ObjectCounter == 0)
        {
            CursorArrowLinks.SetActive(false);
        }
        else
            CursorArrowLinks.SetActive(true);

        if (ObjectCounter+3 == newInventoryObjects.Count || ObjectCounter == newInventoryObjects.Count - 1 || ObjectCounter == newInventoryObjects.Count - 2)
        {
            CursorArrowRechts.SetActive(false);
        }
        else
            CursorArrowRechts.SetActive(true);

        for (int i = ClickCounter; i <=newInventoryObjects.Count/3; i++)
        {
            if (0 + ObjectCounter < newInventoryObjects.Count)
            {
                var MenuObjectOne = newInventoryObjects[0 + ObjectCounter];
                MenuObjectOne.transform.position = new Vector3(-1, 0, 9);
                MenuObjectOne.SetActive(MenuObjectOne);
                if (right == true)
                {
                    for (int a = 0; a < 0 + ObjectCounter; a++)
                    {
                        newInventoryObjects[a].SetActive(false);
                        //Debug.Log("Righta:" + a);
                    }
                }
                if (Left==true){
                    for (int a = 0+newInventoryObjects.Count-1; a > ObjectCounter; a--)
                    {
                        //Debug.Log("Left:" + a);
                        newInventoryObjects[a].SetActive(false);

                    }
                }
            }

            if (1 + ObjectCounter < newInventoryObjects.Count)
            {
                var MenuObjectTwo = newInventoryObjects[1 + ObjectCounter];
                MenuObjectTwo.SetActive(MenuObjectTwo);
                MenuObjectTwo.transform.position = new Vector3(0, 0, 9);
            }

            if (2 + ObjectCounter < newInventoryObjects.Count)
            {
                var MenuObjectThree = newInventoryObjects[2 + ObjectCounter];
                MenuObjectThree.SetActive(MenuObjectThree);
                MenuObjectThree.transform.position = new Vector3(1, 0, 9);
            }
        }
    }

    public void HideInventar()
    {
        HideArrows();
        foreach (GameObject _gameObject in newInventoryObjects)
        {
            _gameObject.SetActive(false);
        }
    }

    public void HideText()
    {
        foreach (GameObject _text in TextObjects)
        {
            _text.SetActive(false);
        }
        Panel.SetActive(false);
    }

    public void OnInventoryItemClick(GameObject inventoryItem, GameObject inventoryItemText)
    {
        IsInventar = false;
        HideInventar();
        Panel.SetActive(true);
        inventoryItem.transform.position = new Vector3(-1, 0, 9);
        inventoryItem.SetActive(true);
        inventoryItemText.SetActive(true);
    }

    public void SelectPortraitLudwig()
    {
        OnInventoryItemClick(portraitludwig, PortraitLudwigText);
        Debug.Log("selected portrait ludwig");
    }
    public void SelectPinsel()
    {
        OnInventoryItemClick(pinsel, PinselText);
    }
    public void SelectRoteHand()
    {
        OnInventoryItemClick(rotehand, RoteHandText);
    }
    public void SelectGunHvG()
    {
        OnInventoryItemClick(gun_HvG, GunHvGText);
    }
    public void SelectGunKvP()
    {
        OnInventoryItemClick(gun_HvG, GunKvPText);
    }
    public void SelectGunLvH()
    {
        OnInventoryItemClick(gun_LvH, GunLvHText);
    }
    public void SelectWindcompass()
    {
        OnInventoryItemClick(windkompass_anim, WindkompassText);
    }
    public void SelectSkizzenbuch()
    {
        OnInventoryItemClick(skizzenbuch, SkizzenbuchText);
    }
    public void SelectSchatulle()
    {
        OnInventoryItemClick(Schatulle, SchatulleText);
    }
    public void SelectArchive()
    {
        OnInventoryItemClick(archive, ArchiveText);
    }
    public void SelectZweiDamen()
    {
        OnInventoryItemClick(zweiDamen, ZweiDamenText);
    }
    public void SelectBriefAugust()
    {
        OnInventoryItemClick(Brief_August, BriefAugustText);
    }
    public void SelectBriefFebruar()
    {
        OnInventoryItemClick(Brief_Februar, BriefFebruarText);
    }
    public void SelectBriefJuni()
    {
        OnInventoryItemClick(Brief_Juni, BriefJuniText);
    }
    public void SelectBriefMaerz()
    {
        OnInventoryItemClick(Brief_Maerz, BriefMaerzText);
    }
    public void SelectBriefMai1741()
    {
        OnInventoryItemClick(Brief_Mai_1741, BriefMai1741Text);
    }
    public void SelectBriefMai1754()
    {
        OnInventoryItemClick(Brief_Mai_1754, BriefMai1754Text);
    }
    public void SelectDeerBrosche()
    {
        OnInventoryItemClick(Deer_Brosche, DeerBroscheText);
    }
    public void SelectGeburtsurkunde()
    {
        OnInventoryItemClick(Geburtsurkunde, GeburtsurkundeText);
    }
    public void SelectKugel()
    {
        //OnInventoryItemClick();
    }
    public void SelectSkizzeMitFrau()
    {
        OnInventoryItemClick(skizze_mit_frau, SkizzeMitFrauText);
    }
    public void SelectTuch()
    {
        OnInventoryItemClick(Tuch, TuchText);
    }
    public void SelectZettelAmKompass()
    {
        OnInventoryItemClick(Zettel_am_Kompass, ZettelAmKompassText);
    }

    public void Characters () {
        //Debug.Log("Characters");
    }

    public void Mission () {
        //Debug.Log("Mission");
    }
}