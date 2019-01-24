using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoctorCharacter : MonoBehaviour {

    void OnSelect()
    {
        GetComponentInParent<MenuController>().OnClickDoctor();
    }
}
