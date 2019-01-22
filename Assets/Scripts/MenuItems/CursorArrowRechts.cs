using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorArrowRechts : MonoBehaviour {
    public int ClickRight;
    public int ObjectCounter;

    void OnSelect()
    {
        //Debug.Log("Arrow Rechts Clicked");
        ClickRight++;
        var menuController = GameObject.Find("MenuController").GetComponent<MenuController>();
        menuController.Left = false;
        menuController.right = true;
        menuController.ObjectCounter = menuController.ObjectCounter+3;

    }
}