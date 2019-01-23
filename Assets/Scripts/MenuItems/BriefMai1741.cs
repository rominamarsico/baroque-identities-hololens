using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BriefMai1741 : MonoBehaviour {

    void OnSelect()
    {
        Debug.Log("Brief Mai 1741 ist sichtbar");
        GetComponentInParent<MenuController>().SelectBriefMai1741();
    }
}
