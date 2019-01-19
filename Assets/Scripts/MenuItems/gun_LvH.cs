using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gun_LvH : MonoBehaviour {

    // Use this for initialization
    void OnSelect()
    {
        Debug.Log("Portrait gun_LvH ist sichtbar");
        GetComponentInParent<MenuController>().SelectGunLvH();
    }
}
