using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class windcompass : MonoBehaviour {

    // Use this for initialization
    void OnSelect()
    {
        Debug.Log("Portrait windcompass ist sichtbar");
        GetComponentInParent<MenuController>().SelectWindcompass();
    }
}
