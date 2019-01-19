using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pinsel : MonoBehaviour {

    void OnSelect()
    {
        Debug.Log("Portrait Pinsel ist sichtbar");
        GetComponentInParent<MenuController>().SelectPinsel();
    }
}
