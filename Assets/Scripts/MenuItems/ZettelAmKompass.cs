using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZettelAmKompass : MonoBehaviour {

    void OnSelect()
    {
        Debug.Log("Zettel am Kompass ist sichtbar");
        GetComponentInParent<MenuController>().SelectZettelAmKompass();
    }
}
