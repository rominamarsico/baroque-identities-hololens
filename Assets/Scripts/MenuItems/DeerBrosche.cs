using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeerBrosche : MonoBehaviour {

    void OnSelect()
    {
        Debug.Log("Deer Brosche ist sichtbar");
        GetComponentInParent<MenuController>().SelectDeerBrosche();
    }
}
