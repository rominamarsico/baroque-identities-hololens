using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LudwigCharacter : MonoBehaviour {

    void OnSelect()
    {
        GetComponentInParent<MenuController>().OnClickLudwig();
    }
}
