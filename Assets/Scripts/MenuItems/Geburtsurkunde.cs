using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Geburtsurkunde : MonoBehaviour {

    void OnSelect()
    {
        Debug.Log("Geburtsurkunde ist sichtbar");
        GetComponentInParent<MenuController>().SelectGeburtsurkunde();
    }
}
