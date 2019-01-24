using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MalerCharacter : MonoBehaviour {

    void OnSelect()
    {
        GetComponentInParent<MenuController>().OnClickMaler();
    }
}
