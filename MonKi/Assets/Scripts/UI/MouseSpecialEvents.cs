using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseSpecialEvents : MonoBehaviour
{
    public int index;
    public bool go;

    MainMenu menu;
    void Start()
    {
        menu = FindObjectOfType<MainMenu>();
    }

    private void OnMouseOver() {
        menu.cursor.position = GetComponent<RectTransform>().position;
    }

    public void CallMenu()
    {
        if(go)
            menu.GoToPos(index);
        else
            menu.ReturnToPos(index);
    }

    public void CallQuits()
    {
        Application.Quit();
    }
}
