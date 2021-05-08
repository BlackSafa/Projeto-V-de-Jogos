using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    int command = 0;
    MenuSheet[] menuPanels = new MenuSheet[3];
    public RectTransform menu;
    public Sprite[] originals = new Sprite[4], tradables = new Sprite[4];
    public Image[] buttons = new Image[4];
    bool menuOut = false, changed = false;
    public bool paused = false;

    void Start()
    {
        gameObject.GetComponent<Image>().enabled = false;
        RectTransform thing = GameObject.Find("Controles").GetComponent<RectTransform>();
        menuPanels[0] = new MenuSheet(thing, thing.anchoredPosition.normalized);
        thing = GameObject.Find("Pause Menu").GetComponent<RectTransform>();
        menuPanels[1] = new MenuSheet(thing, thing.anchoredPosition.normalized);
        thing = GameObject.Find("Reference").GetComponent<RectTransform>();
        menuPanels[2] = new MenuSheet(thing, thing.anchoredPosition.normalized);
        buttons[command].sprite = originals[command];
        buttons[command].sprite = tradables[command];
    }

    // Downdate is called once per frame
    public IEnumerator Pause()
    {
        gameObject.GetComponent<Image>().enabled = true;
        Time.timeScale = 0;
        paused = true;
        GoToPos(1);
        menuOut = false;
        bool first = true;
        while(paused)
        {
            if(!menuOut)
            {
                OperateMenu(first);




                first = false;
                if(Input.GetButtonDown("Accelerate") || Input.GetKey(KeyCode.Return)){
                    switch (command)
                    {
                        case 0:
                            gameObject.GetComponent<Image>().enabled = false;
                            menuOut = true;
                            paused = false;
                            ReturnToPos(2);
                            Time.timeScale = 1;;
                            break;
                        case 1:
                            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                            Time.timeScale = 1;
                            break;
                        case 2:
                            menuOut = true;
                            GoToPos(0);
                            break;
                        case 3:
                            SceneManager.LoadScene(0, LoadSceneMode.Single);
                            Time.timeScale = 1;
                            break;
                    }             
                }
            }

            if(Input.GetButtonDown("Reverse") && menuOut)
            {
                menuOut = false;
                ReturnToPos(1);
            }
            yield return null;
        }
    }
    void OperateMenu(bool first)
    {
        changed = (Input.GetKeyDown( KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) )? true : false;
        if(changed && !first)
        {
            buttons[command].sprite = originals[command];

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                print(command + " sub " + (command-1));
                command--;

                if (command < 0)
                    command = buttons.Length - 1;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                print(command + " add " + (command + 1));
                command++;
            }

            command = command % tradables.Length;
            buttons[command].sprite = tradables[command];
        }
    }

    void GoToPos(int index)
    {
        StopCoroutine("MoveMenu");
        StartCoroutine(MoveMenu(menuPanels[index], false));
    }
    void ReturnToPos(int index)
    {
        StopCoroutine("MoveMenu");
        StartCoroutine(MoveMenu(menuPanels[index], true));
    }

    IEnumerator MoveMenu(MenuSheet panel, bool reverse)
    {
        int multiplier = reverse? -1: 1;
        while(-panel.sheet.anchoredPosition != menu.anchoredPosition)
        {
            menu.transform.Translate((menu.anchoredPosition - panel.sheet.anchoredPosition).normalized * 2000 * Time.fixedUnscaledDeltaTime * multiplier);
            if(multiplier * (Mathf.Abs(panel.sheet.anchoredPosition.x)- Mathf.Abs(menu.anchoredPosition.x)) < 100 && multiplier * (Mathf.Abs(panel.sheet.anchoredPosition.y)-Mathf.Abs(menu.anchoredPosition.y)) < 100)
            {
                menu.anchoredPosition = -panel.sheet.anchoredPosition;
            }
            yield return null;
        }
    }
}