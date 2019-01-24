using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeutnantinCharacter : MonoBehaviour {

    void OnSelect()
    {
        GetComponentInParent<MenuController>().OnClickLeutnantin();
    }
}
