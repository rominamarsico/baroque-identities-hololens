using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Schatulle : MonoBehaviour {

    void OnSelect()
    {
        Debug.Log("Schatulle ist sichtbar");
        GetComponentInParent<MenuController>().SelectSchatulle();
    }
}
