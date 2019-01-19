using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorArrowLinks : MonoBehaviour {

    public int ClickLeft;
    public int ObjectCounter;

    void OnSelect()
    {
        Debug.Log("Arrow Links Clicked");
        ClickLeft++;
        var menuController = GameObject.Find("MenuController").GetComponent<MenuController>();
        var CursorArrowRechts = GameObject.Find("CursorArrowRechts").GetComponent<CursorArrowRechts>();
        menuController.ObjectCounter = CursorArrowRechts.ClickRight-3;
        Debug.Log(menuController.ObjectCounter);
    }
}