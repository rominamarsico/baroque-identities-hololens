using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public bool Left = false;
    public bool right = false;
    public bool IsInventar = true;
    public bool IsCharacter = true;
    public bool InventoryArrows = false;
    public bool CharacterArrows = false;

    public GameObject CursorArrowRechts;
    public GameObject CursorArrowLinks;
    public GameObject MissionPlan;
    public GameObject RepeatButton;
    public GameObject[] InventaryObjects;

    public IList<GameObject> newInventoryObjects;
    public IList<GameObject> TextObjects;
    public IList<GameObject> PortraitImages;
    public IList<GameObject> PortraitTexts;

    //Inventar von Beginn an
    public GameObject pinsel;
    public GameObject portraitludwig;
    public GameObject rotehand;
    public GameObject gun_HvG;
    public GameObject gun_KvP;
    public GameObject gun_LvH;
    public GameObject windkompass_anim;

    public int ObjectCounter;
    public int CharacterObjectCounter;

    //Inventar Atelier
    public GameObject skizzenbuch;
    public GameObject skizze_mit_frau;
    public GameObject archive;
    public GameObject Schatulle;
    public GameObject Brief_August;
    public GameObject Brief_Februar;
    public GameObject Brief_Maerz;
    public GameObject Brief_Mai_1741;
    public GameObject Brief_Mai_1754;
    public GameObject Zettel_am_Kompass;
    public GameObject zweiDamen;

    //Inventar Tatort
    public GameObject Geburtsurkunde;
    public GameObject Tuch;
    public GameObject Deer_Brosche;
    public GameObject Brief_Juni;

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

    //Characters
    public GameObject doctorPortrait;
    public GameObject eduardoPortrait;
    public GameObject karolinePortrait;
    public GameObject ludwigPortrait;
    public GameObject malerPortrait;

    //Character Texte
    public GameObject doctorText;
    public GameObject eduardoText;
    public GameObject karolineText;
    public GameObject ludwigText;
    public GameObject malerText;

    //Audio Source Clicks
    public AudioSource menuArrowButtonClick;
    public AudioSource menuItemClick;
    public AudioSource scanSound;

    //Audio Source NFC tags
    public AudioSource ArchiveSound;
    public AudioSource BriefAugustSound;
    public AudioSource BriefsammlungSound;
    public AudioSource IntroSound;
    public AudioSource SchatulleSound;
    public AudioSource skizzeFrauSound;
    public AudioSource skizzenBuchSound;
    public AudioSource zettelKompassSound;
    public AudioSource zweiDamenSound;

    void Awake ()
    {
        StartCoroutine(ClearNfcInput());
    }

    // Use this for initialization
    void Start () {
        InventaryObjects = GameObject.FindGameObjectsWithTag("Inventar");
        newInventoryObjects = new List<GameObject>(InventaryObjects);
        TextObjects = GameObject.FindGameObjectsWithTag("Text");
        PortraitImages = GameObject.FindGameObjectsWithTag("character");

        for (int i = 0; i <TextObjects.Count; i++)
        {
            // Debug.Log("Inventar Number " + i + " is named " + TextObjects[i].name);
        }
        
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
        Debug.Log("clear nfc input");
    }

    IEnumerator GetClickedMenu()
    {
        StartCoroutine(ClearNfcInput());

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
            //menuArrowButtonClick.Play();
        }

        if (inventar.Contains("Inventar"))
        {
            Debug.Log(wwwInventar.downloadHandler.text);
            OnTriggerInventar();
            HideMission();
            HideCharacters();
            InventoryArrows = true;
            CharacterArrows = false;
        }
        else if (character.Contains("Character"))
        {
            Debug.Log(wwwCharacter.downloadHandler.text);
            OnTriggerCharacter();
            HideInventar();
            HideMission();
            CharacterArrows = true;
            InventoryArrows = false;
        }
        else if (mission.Contains("Mission"))
        {
            Debug.Log(wwwMission.downloadHandler.text);
            Mission();
            HideInventar();
            HideCharacters();
            CharacterArrows = false;
            InventoryArrows = false;
            HideArrows();
        }
        else
        {
            Debug.Log("No menu controller is triggert.");
            HideInventar();
            HideMission();
            HideCharacters();
            HideArrows();
            CharacterArrows = false;
            InventoryArrows = false;
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
            SchatulleSound.Play();
        }
        else if (nfcTag.Contains("briefsammlung") && !newInventoryObjects.Contains(Brief_Februar))
        {
            newInventoryObjects.Add(Brief_Februar);
            newInventoryObjects.Add(Brief_Mai_1741);
            newInventoryObjects.Add(Brief_Maerz);
            newInventoryObjects.Add(Brief_Mai_1754);
            BriefsammlungSound.Play();
        }
        else if (nfcTag.Contains("skizzenbuch") && !newInventoryObjects.Contains(skizzenbuch))
        {
            newInventoryObjects.Add(skizzenbuch);
            skizzenBuchSound.Play();
        }
        else if (nfcTag.Contains("skizzeFrau") && !newInventoryObjects.Contains(skizze_mit_frau))
        {
            newInventoryObjects.Add(skizze_mit_frau);
            skizzeFrauSound.Play();
        }
        else if (nfcTag.Contains("archive") && !newInventoryObjects.Contains(archive))
        {
            newInventoryObjects.Add(archive);
            ArchiveSound.Play();
        }
        else if (nfcTag.Contains("2damen") && !newInventoryObjects.Contains(zweiDamen))
        {
            newInventoryObjects.Add(zweiDamen);
            zweiDamenSound.Play();
        }
        else if (nfcTag.Contains("Deer") && !newInventoryObjects.Contains(Deer_Brosche))
        {
            newInventoryObjects.Add(Deer_Brosche);
            scanSound.Play();
        }
        else if (nfcTag.Contains("tuch") && !newInventoryObjects.Contains(Tuch))
        {
            newInventoryObjects.Add(Tuch);
            scanSound.Play();
        }
        else if (nfcTag.Contains("geburtsurkunde") && !newInventoryObjects.Contains(Geburtsurkunde))
        {
            newInventoryObjects.Add(Geburtsurkunde);
            scanSound.Play();
        }
        else if (nfcTag.Contains("zettelKompass") && !newInventoryObjects.Contains(Zettel_am_Kompass))
        {
            newInventoryObjects.Add(Zettel_am_Kompass);
            zettelKompassSound.Play();
        }
        else if (nfcTag.Contains("briefAugust") && !newInventoryObjects.Contains(Brief_August))
        {
            newInventoryObjects.Add(Brief_August);
            BriefAugustSound.Play();
        }
        else if (nfcTag.Contains("briefJuni") && !newInventoryObjects.Contains(Brief_Juni))
        {
            newInventoryObjects.Add(Brief_Juni);
            scanSound.Play();
        }
        else if (nfcTag.Contains("skizzeFrau") && !newInventoryObjects.Contains(skizze_mit_frau))
        {
            newInventoryObjects.Add(skizze_mit_frau);
            skizzeFrauSound.Play();
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

    public void OnBackButtonClick()
    {
        menuItemClick.Play();
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

    public void OnTriggerCharacter()
    {
        if (IsCharacter == true)
        {
            Characters();
        }
    }

    public void TrueCharacter()
    {
        IsCharacter = true;
    }

    public void Inventar()
    {
        buildMenu(newInventoryObjects, CursorArrowRechts.GetComponent<CursorArrowRechts>().ClickRight, CursorArrowLinks.GetComponent<CursorArrowLinks>().ClickLeft, ObjectCounter);
    }

    public void HideInventar()
    {
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

    public void Characters()
    {
        buildMenu(PortraitImages, CursorArrowRechts.GetComponent<CursorArrowRechts>().CharacterArrowClickRight, CursorArrowLinks.GetComponent<CursorArrowLinks>().CharacterArrowClickLeft, CharacterObjectCounter);
    }

    public void HideCharacters()
    {
        foreach (GameObject _gameObject in PortraitImages)
        {
            _gameObject.SetActive(false);
        }
    }

    public void Mission()
    {
        MissionPlan.SetActive(true);
        RepeatButton.SetActive(true);
        MissionPlan.transform.position = new Vector3(0, 0, 9);
        RepeatButton.transform.position = new Vector3(0, -1, 9);
    }

    public void HideMission()
    {
        MissionPlan.SetActive(false);
        RepeatButton.SetActive(false);
    }

    public void OnRepeatMissionButtonClick()
    {
        menuArrowButtonClick.Play();
        SceneManager.LoadScene("IntroMission");
    }

    public void OnInventoryItemClick(GameObject inventoryItem, GameObject inventoryItemText)
    {
        IsInventar = false;
        HideInventar();
        HideArrows();
        Panel.SetActive(true);
        inventoryItem.transform.position = new Vector3(-1, 0, 9);
        inventoryItem.SetActive(true);
        inventoryItemText.SetActive(true);
        menuItemClick.Play();
    }

    public void SelectPortraitLudwig()
    {
        OnInventoryItemClick(portraitludwig, PortraitLudwigText);
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

    public void OnCharacterClick(GameObject character, GameObject characterText)
    {        
        IsCharacter = false;
        HideCharacters();
        HideArrows();
        Panel.SetActive(true);
        character.transform.position = new Vector3(-1, 0, 9);
        character.SetActive(true);
        characterText.SetActive(true);
        menuItemClick.Play();
    }

    public void OnClickDoctor()
    {
        OnCharacterClick(doctorPortrait, doctorText);
    }
    public void OnClickEduardo()
    {
        OnCharacterClick(eduardoPortrait, eduardoText);
    }
    public void OnClickKaroline()
    {
        OnCharacterClick(karolinePortrait, karolineText);
    }
    public void OnClickLudwig()
    {
        OnCharacterClick(ludwigPortrait, ludwigText);
    }
    public void OnClickMaler()
    {
        OnCharacterClick(malerPortrait, malerText);
    }

    public void buildMenu(IList<GameObject> ObjectArray, int ClickCounterRight, int ClickCounterLeft, int ObjectCounter)
    {
        Arrows();
        HideText();
        var ClickCounter = ClickCounterRight - ClickCounterLeft;

        if (ObjectCounter == 0)
        {
            CursorArrowLinks.SetActive(false);
        }
        else
            CursorArrowLinks.SetActive(true);

        if (ObjectCounter + 3 == ObjectArray.Count || ObjectCounter == ObjectArray.Count - 1 || ObjectCounter == ObjectArray.Count - 2)
        {
            CursorArrowRechts.SetActive(false);
        }
        else
        {
            CursorArrowRechts.SetActive(true);
        }

        for (int i = ClickCounter; i <= ObjectArray.Count / 3; i++)
        {
            if (0 + ObjectCounter < ObjectArray.Count)
            {
                var MenuObjectOne = ObjectArray[0 + ObjectCounter];
                MenuObjectOne.transform.position = new Vector3(-1, 0, 9);
                MenuObjectOne.SetActive(MenuObjectOne);
                if (right == true)
                {
                    for (int a = 0; a < 0 + ObjectCounter; a++)
                    {
                        ObjectArray[a].SetActive(false);
                    }
                }
                if (Left == true)
                {
                    for (int a = 0 + ObjectArray.Count - 1; a > ObjectCounter; a--)
                    {
                        ObjectArray[a].SetActive(false);
                    }
                }
            }

            if (1 + ObjectCounter < ObjectArray.Count)
            {
                var MenuObjectTwo = ObjectArray[1 + ObjectCounter];
                MenuObjectTwo.SetActive(MenuObjectTwo);
                MenuObjectTwo.transform.position = new Vector3(0, 0, 9);
            }

            if (2 + ObjectCounter < ObjectArray.Count)
            {
                var MenuObjectThree = ObjectArray[2 + ObjectCounter];
                MenuObjectThree.SetActive(MenuObjectThree);
                MenuObjectThree.transform.position = new Vector3(1, 0, 9);
            }
        }
    }
}