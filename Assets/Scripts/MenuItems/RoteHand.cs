using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoteHand : MonoBehaviour {

    // Use this for initialization
    void OnSelect()
    {
        Debug.Log("Portrait Rote Hand ist sichtbar");
        GetComponentInParent<MenuController>().SelectRoteHand();
    }
}
