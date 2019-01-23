using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archive : MonoBehaviour {
    
    void OnSelect()
    {
        Debug.Log("Archive ist sichtbar");
        GetComponentInParent<MenuController>().SelectArchive();
    }
}
