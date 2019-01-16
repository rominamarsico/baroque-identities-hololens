using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gun_HvG : MonoBehaviour {

    void OnSelect()
    {
        Debug.Log("Portrait gun_HvG ist sichtbar");
        GetComponentInParent<MenuController>().SelectGunHvG();
    }
}
