using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KarolineCharacter : MonoBehaviour {

    void OnSelect()
    {
        GetComponentInParent<MenuController>().OnClickKaroline();
    }
}
