using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorArrowRechts : MonoBehaviour {
    public int ClickRight;

    void OnSelect()
    {
        Debug.Log("Arrow Rechts Clicked");
        ClickRight++;
        var menuController = GameObject.Find("MenuController").GetComponent<MenuController>();
        menuController.ObjectCounter = ClickRight*3;
    }
}