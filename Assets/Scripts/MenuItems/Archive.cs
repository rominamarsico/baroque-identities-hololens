using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archive : MonoBehaviour {

    // Use this for initialization
    void OnSelect()
    {
        Debug.Log("Archieve ist sichtbar");
        GetComponentInParent<MenuController>().SelectArchive();
    }
}
