using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorArrowLinks : MonoBehaviour {

    public int ClickLeft;
    public int CharacterArrowClickLeft;

    public AudioSource click;

    void OnSelect()
    {
        click.Play();

        //Debug.Log("Arrow Links Clicked");
        var menuController = GameObject.Find("MenuController").GetComponent<MenuController>();
        menuController.Left = true;
        menuController.right = false;
        
        if (menuController.ObjectCounter >= 1)
        {
            ClickLeft++;
            menuController.ObjectCounter = menuController.ObjectCounter - 3;
        }

        if (menuController.CharacterObjectCounter >= 1)
        {
            CharacterArrowClickLeft++;
            menuController.CharacterObjectCounter = menuController.CharacterObjectCounter - 3;
        }
    }
}
