using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kugel : MonoBehaviour {

    void OnSelect()
    {
        Debug.Log("Kugel ist sichtbar");
        GetComponentInParent<MenuController>().SelectKugel();
    }
}
