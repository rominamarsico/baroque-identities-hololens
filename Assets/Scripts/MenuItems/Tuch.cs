using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tuch : MonoBehaviour {

    void OnSelect()
    {
        Debug.Log("Tuch ist sichtbar");
        GetComponentInParent<MenuController>().SelectTuch();
    }
}
