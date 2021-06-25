using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    //int command = 0;
    MenuSheet[] menuPanels = new MenuSheet[5];

    MenuSheet activePanel;
    public RectTransform menu;
    //public Sprite[] originals = new Sprite[4], tradables = new Sprite[4];
    //public Image[] buttons = new Image[4];
    public RectTransform cursor;
    bool menuOut, selecting, changed;

    void Start()
    {
        RectTransform thing = GameObject.Find("PlayOnlineBackground").GetComponent<RectTransform>();
        menuPanels[0] = new MenuSheet(thing, thing.anchoredPosition.normalized);
        thing = GameObject.Find("MonkeySelectionMenu").GetComponent<RectTransform>();
        menuPanels[1] = new MenuSheet(thing, thing.anchoredPosition.normalized);
        thing = GameObject.Find("OptionsBackground").GetComponent<RectTransform>();
        menuPanels[2] = new MenuSheet(thing, thing.anchoredPosition.normalized);
        thing = GameObject.Find("Credits").GetComponent<RectTransform>();
        menuPanels[3] = new MenuSheet(thing, thing.anchoredPosition.normalized);
        thing = GameObject.Find("StartMenuContents").GetComponent<RectTransform>();
        menuPanels[4] = new MenuSheet(thing, thing.anchoredPosition.normalized);
        activePanel = menuPanels[4];
        //buttons[command].sprite = originals[command];
        //buttons[command].sprite = tradables[command];
    }

    /*
    // Update is called once per frame
    void Update()
    {
        if(!menuOut)
        {
            OperateMenu();
            if(Input.GetButtonUp("Interact")){
                if(command == 3) Application.Quit();

                menuOut = true;
                selecting = command == 0 ? true : false;
                GoToPos();
            }
        }
        if(Input.GetButtonUp("Fire2") && menuOut)
        {
            menuOut = false;
            selecting = false;
            ReturnToPos();
        }
    }
    void OperateMenu()
    {
        changed = Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.LeftArrow)? true : false;
        if(changed)
        {
            buttons[command].sprite = originals[command];
            command = Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.LeftArrow)? command-1 : command;
            command = Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.RightArrow)? command+1 : command;
            command = command < 0 ? buttons.Length-1 : command;
            command = command % tradables.Length;
            buttons[command].sprite = tradables[command];
        }
    }*/
    public void GoToPos(int command)
    {
        StopAllCoroutines();
        StartCoroutine(MoveMenu(menuPanels[command], false));
    }
    public void ReturnToPos(int command)
    {
        StopAllCoroutines();
        StartCoroutine(MoveMenu(menuPanels[command], true));
    }

    IEnumerator MoveMenu(MenuSheet panel, bool reverse)
    {
        Vector2 target = new Vector2(0,0);
        int multiplier = reverse? -1: 1;
        while(panel.sheet.anchoredPosition != -menu.anchoredPosition)
        {
            menu.transform.Translate((menu.anchoredPosition - panel.sheet.anchoredPosition).normalized * 1000 *Time.unscaledDeltaTime * multiplier);
            //panel.group.alpha = (panel.sheet.position.x - panel.sheet.anchoredPosition.x) / (activePanel.sheet.anchoredPosition.x - panel.sheet.anchoredPosition.x);
            //activePanel.group.alpha = (activePanel.sheet.position.x - activePanel.sheet.anchoredPosition.x)/(-panel.sheet.localPosition.x - activePanel.sheet.localPosition.x);
            if(multiplier * (Mathf.Abs(panel.sheet.anchoredPosition.x)- Mathf.Abs(menu.anchoredPosition.x)) < 100 && multiplier * (Mathf.Abs(panel.sheet.anchoredPosition.y)-Mathf.Abs(menu.anchoredPosition.y)) < 100)
            {
                menu.anchoredPosition = -panel.sheet.anchoredPosition;
            }
            yield return null; 
        }
        panel.group.alpha = 1;
        panel.group.interactable = true;
        panel.group.blocksRaycasts = true;
        activePanel.group.alpha = 0;
        activePanel.group.interactable = false;
        activePanel.group.blocksRaycasts = false;
        activePanel = panel;
    }
}

public class MenuSheet
{
    private RectTransform Sheet;
    public RectTransform sheet
    {
        get
        {
            return Sheet;
        }
    }

    private Vector2 Dir;
    public Vector2 dir
    {
        get
        {
            return Dir;
        }
    }

    //private CanvasGroup Group;

    public CanvasGroup group;
/*    {
        get
        {
            return Group;
        }
    }*/

    private Vector2 Target;
    public Vector2 target
    {
        get
        {
            return Target;
        }
    }

    public MenuSheet(GameObject menu, UIAnimationType AnimType)
    {
        switch(AnimType)
        {
            case UIAnimationType.Move:
                break;
            case UIAnimationType.Fade_in:
                break;
            case UIAnimationType.Pop:
                break;
            default:
                break;
        }
    }
    public MenuSheet(RectTransform menu, Vector2 dir)
    {
        
        Sheet = menu;
        Dir = dir;
        group = menu.GetComponent<CanvasGroup>();
    }
}

[Flags]
public enum UIAnimationType
{
    Move,
    Fade_in,
    Pop,
    None
}
