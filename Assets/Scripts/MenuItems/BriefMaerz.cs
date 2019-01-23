using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BriefMaerz : MonoBehaviour {

    void OnSelect()
    {
        Debug.Log("Brief März ist sichtbar");
        GetComponentInParent<MenuController>().SelectBriefMaerz();
    }
}
