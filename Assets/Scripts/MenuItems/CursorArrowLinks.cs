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
        menuController.Left = true;
       
        Debug.Log(menuController.Left);
        if (menuController.ObjectCounter >= 1)
        {
            menuController.ObjectCounter = menuController.ObjectCounter - 3;

        }

    }
}
