using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkizzeMitFrau : MonoBehaviour {

    void OnSelect()
    {
        Debug.Log("Skizze mit Frau ist sichtbar");
        GetComponentInParent<MenuController>().SelectSkizzeMitFrau();
    }
}
