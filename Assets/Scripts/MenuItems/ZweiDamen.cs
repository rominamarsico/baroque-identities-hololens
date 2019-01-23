using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZweiDamen : MonoBehaviour {

    void OnSelect()
    {
        Debug.Log("Zwei Damen ist sichtbar");
        GetComponentInParent<MenuController>().SelectZweiDamen();
    }
}
