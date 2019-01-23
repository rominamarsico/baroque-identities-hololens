using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skizzenbuch : MonoBehaviour {

    void OnSelect()
    {
        Debug.Log("Skizzenbuch ist sichtbar");
        GetComponentInParent<MenuController>().SelectSkizzenbuch();
    }
}
