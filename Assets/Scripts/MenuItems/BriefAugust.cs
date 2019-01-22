using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BriefAugust : MonoBehaviour {

    void OnSelect()
    {
        Debug.Log("Brief August ist sichtbar");
        GetComponentInParent<MenuController>().SelectBriefAugust();
    }
}
