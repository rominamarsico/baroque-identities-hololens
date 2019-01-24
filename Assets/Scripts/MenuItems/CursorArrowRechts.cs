using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorArrowRechts : MonoBehaviour {
    public int ClickRight;
    public int CharacterArrowClickRight;

    public AudioSource click;

    void OnSelect()
    {
        //Debug.Log("Arrow Rechts Clicked");
        click.Play();
        var menuController = GameObject.Find("MenuController").GetComponent<MenuController>();
        menuController.Left = false;
        menuController.right = true;

        if (menuController.CharacterArrows == true)
        {
            CharacterArrowClickRight++;
            menuController.CharacterObjectCounter = menuController.CharacterObjectCounter + 3;
        }
        else if (menuController.InventoryArrows == true)
        {
            ClickRight++;
            menuController.ObjectCounter = menuController.ObjectCounter + 3;
        }
    }
}