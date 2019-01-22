using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BriefJuni : MonoBehaviour {

    void OnSelect()
    {
        Debug.Log("Brief Juni ist sichtbar");
        GetComponentInParent<MenuController>().SelectBriefJuni();
    }
}
