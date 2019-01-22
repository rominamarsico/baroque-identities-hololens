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
    public AudioSource click;

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

        ClearNfcInput();

        HideText();
    }

    void Update()
    {
        StartCoroutine(GetClickedMenu());
        StartCoroutine(GetNfcTag());
    }

    IEnumerator ClearNfcInput ()
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
        CursorArrowRechts.transform.position = new Vector3(2, -2, 9);
        CursorArrowLinks.transform.position = new Vector3(-2, -2, 9);
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
        else
            Debug.Log("IsInventar is false");
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
        Debug.Log(ClickCounterRight);
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
                MenuObjectOne.transform.position = new Vector3(-2, 0, 9);
                MenuObjectOne.SetActive(MenuObjectOne);
                if (right == true)
                {
                    for (int a = 0; a < 0 + ObjectCounter; a++)
                    {
                        newInventoryObjects[a].SetActive(false);
                        Debug.Log("Righta:" + a);
                    }
                }
                if (Left==true){
                    for (int a = 0+newInventoryObjects.Count-1; a > ObjectCounter; a--)
                    {
                        Debug.Log("Left:" + a);
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
                MenuObjectThree.transform.position = new Vector3(2, 0, 9);
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

    public void HideText(){
        foreach (GameObject _text in TextObjects)
        {
            _text.SetActive(false);
        }
        Panel.SetActive(false);
    }

    public void SelectPortraitLudwig()
    {
        IsInventar = false;
        HideInventar();
        portraitludwig.transform.position = new Vector3(-2, 0, 9);
        portraitludwig.SetActive(true);
        Panel.SetActive(true);
        PortraitLudwigText.SetActive(true);
    }

    public void SelectPinsel()
    {
        IsInventar = false;
        HideInventar();
        pinsel.transform.position = new Vector3(-2, 0, 9);
        pinsel.SetActive(true);
        Panel.SetActive(true);
        PinselText.SetActive(true);
    }

    public void SelectRoteHand()
    {
        IsInventar = false;
        HideInventar();
        rotehand.transform.position = new Vector3(-2, 0, 9);
        rotehand.SetActive(true);
        Panel.SetActive(true);
        RoteHandText.SetActive(true);
    }
    public void SelectGunHvG()
    {
        IsInventar = false;
        HideInventar();
        gun_HvG.transform.position = new Vector3(-2, 0, 9);
        gun_HvG.SetActive(true);
        Panel.SetActive(true);
        GunHvGText.SetActive(true);
    }
    public void SelectGunKvP()
    {
        IsInventar = false;
        HideInventar();
        gun_KvP.transform.position = new Vector3(-2, 0, 9);
        gun_KvP.SetActive(true);
        Panel.SetActive(true);
        GunKvPText.SetActive(true);
    }
    public void SelectGunLvH()
    {
        IsInventar = false;
        HideInventar();
        gun_LvH.transform.position = new Vector3(-2, 0, 9);
        gun_LvH.SetActive(true);
        Panel.SetActive(true);
        GunLvHText.SetActive(true);
    }
    public void SelectWindcompass()
    {
        IsInventar = false;
        HideInventar();
        windkompass_anim.transform.position = new Vector3(-2, 0, 9);
        windkompass_anim.SetActive(true);
        Panel.SetActive(true);
        WindkompassText.SetActive(true);
    }
    public void SelectSkizzenbuch()
    {
        IsInventar = false;
        HideInventar();
        skizzenbuch.transform.position = new Vector3(-2, 0, 9);
        skizzenbuch.SetActive(true);
        Panel.SetActive(true);
        SkizzenbuchText.SetActive(true);
    }
    public void SelectSchatulle()
    {
        IsInventar = false;
        HideInventar();
        Schatulle.transform.position = new Vector3(-2, 0, 9);
        Schatulle.SetActive(true);
        Panel.SetActive(true);
        SchatulleText.SetActive(true);
    }
    public void SelectArchive()
    {
        IsInventar = false;
        HideInventar();
        archive.transform.position = new Vector3(-2, 0, 9);
        archive.SetActive(true);
        Panel.SetActive(true);
        ArchiveText.SetActive(true);
    }
    public void SelectZweiDamen()
    {
        IsInventar = false;
        HideInventar();
        zweiDamen.transform.position = new Vector3(-2, 0, 9);
        zweiDamen.SetActive(true);
        Panel.SetActive(true);
        ZweiDamenText.SetActive(true);
    }
    public void SelectBriefAugust()
    {
        IsInventar = false;
        HideInventar();
        Brief_August.transform.position = new Vector3(-2, 0, 9);
        Brief_August.SetActive(true);
        Panel.SetActive(true);
        BriefAugustText.SetActive(true);
    }
    public void SelectBriefFebruar()
    {
        IsInventar = false;
        HideInventar();
        Brief_Februar.transform.position = new Vector3(-2, 0, 9);
        Brief_Februar.SetActive(true);
        Panel.SetActive(true);
        BriefFebruarText.SetActive(true);
    }
    public void SelectBriefJuni()
    {
        IsInventar = false;
        HideInventar();
        Brief_Juni.transform.position = new Vector3(-2, 0, 9);
        Brief_Juni.SetActive(true);
        Panel.SetActive(true);
        BriefJuniText.SetActive(true);
    }
    public void SelectBriefMaerz()
    {
        IsInventar = false;
        HideInventar();
        Brief_Maerz.transform.position = new Vector3(-2, 0, 9);
        Brief_Maerz.SetActive(true);
        Panel.SetActive(true);
        BriefMaerzText.SetActive(true);
    }
    public void SelectBriefMai1741()
    {
        IsInventar = false;
        HideInventar();
        Brief_Mai_1741.transform.position = new Vector3(-2, 0, 9);
        Brief_Mai_1741.SetActive(true);
        Panel.SetActive(true);
        BriefMai1741Text.SetActive(true);
    }
    public void SelectBriefMai1754()
    {
        IsInventar = false;
        HideInventar();
        Brief_Mai_1741.transform.position = new Vector3(-2, 0, 9);
        Brief_Mai_1741.SetActive(true);
        Panel.SetActive(true);
        BriefMai1741Text.SetActive(true);
    }
    public void SelectDeerBrosche()
    {
        IsInventar = false;
        HideInventar();
        Deer_Brosche.transform.position = new Vector3(-2, 0, 9);
        Deer_Brosche.SetActive(true);
        Panel.SetActive(true);
        DeerBroscheText.SetActive(true);
    }
    public void SelectGeburtsurkunde()
    {
        IsInventar = false;
        HideInventar();
        Geburtsurkunde.transform.position = new Vector3(-2, 0, 9);
        Geburtsurkunde.SetActive(true);
        Panel.SetActive(true);
        GeburtsurkundeText.SetActive(true);
    }
    public void SelectKugel()
    {
        /*IsInventar = false;
        HideInventar();
        .transform.position = new Vector3(-2, 0, 9);
        Geburtsurkunde.SetActive(true);
        Panel.SetActive(true);
        GeburtsurkundeText.SetActive(true);*/
    }
    public void SelectSkizzeMitFrau()
    {
        IsInventar = false;
        HideInventar();
        skizze_mit_frau.transform.position = new Vector3(-2, 0, 9);
        skizze_mit_frau.SetActive(true);
        Panel.SetActive(true);
        SkizzeMitFrauText.SetActive(true);
    }
    public void SelectTuch()
    {
        IsInventar = false;
        HideInventar();
        Tuch.transform.position = new Vector3(-2, 0, 9);
        Tuch.SetActive(true);
        Panel.SetActive(true);
        TuchText.SetActive(true);
    }
    public void SelectZettelAmKompass()
    {
        IsInventar = false;
        HideInventar();
        Zettel_am_Kompass.transform.position = new Vector3(-2, 0, 9);
        Zettel_am_Kompass.SetActive(true);
        Panel.SetActive(true);
        ZettelAmKompassText.SetActive(true);
    }

    public void Characters () {
        Debug.Log("Characters");
    }

    public void Mission () {
        Debug.Log("Mission");
    }
}