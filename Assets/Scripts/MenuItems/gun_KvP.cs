using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gun_KvP : MonoBehaviour {

    // Use this for initialization
    void OnSelect()
    {
        Debug.Log("Portrait gun_KvP ist sichtbar");
        GetComponentInParent<MenuController>().SelectGunKvP();
    }
}
