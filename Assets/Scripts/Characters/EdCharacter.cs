using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdCharacter : MonoBehaviour {

    void OnSelect()
    {
        GetComponentInParent<MenuController>().OnClickEduardo();
    }
}
