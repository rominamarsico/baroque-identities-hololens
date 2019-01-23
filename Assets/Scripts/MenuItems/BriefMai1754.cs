using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BriefMai1754 : MonoBehaviour {

    void OnSelect()
    {
        Debug.Log("Brief Mai 1754 ist sichtbar");
        GetComponentInParent<MenuController>().SelectBriefMai1754();
    }
}
