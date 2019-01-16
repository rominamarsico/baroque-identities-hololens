using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortraitLudwig : MonoBehaviour
{
    void OnSelect()
    {
        Debug.Log("Portrait Ludwig ist sichtbar");
        GetComponentInParent<MenuController>().SelectPortraitLudwig();
    }
}
