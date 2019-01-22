using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BriefFebruar : MonoBehaviour {

    void OnSelect()
    {
        Debug.Log("Brief Februar ist sichtbar");
        GetComponentInParent<MenuController>().SelectBriefFebruar();
    }
}
