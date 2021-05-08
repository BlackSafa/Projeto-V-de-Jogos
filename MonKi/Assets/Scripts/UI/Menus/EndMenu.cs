using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndMenu : MonoBehaviour
{
    int command = 0;
    public GameObject winPannel, gameOverPannel;
    bool win = false;
    public Sprite[] originals = new Sprite[2], tradables = new Sprite[2];
    public Image[] buttons = new Image[2];

    bool changed;
    void Start()
    {
        /*if(true)
        {
            winPannel.SetActive(true);
            gameOverPannel.SetActive(false);
            win = true;
        }
        else
        {
            winPannel.SetActive(false);
            gameOverPannel.SetActive(true);
            buttons[command].sprite = originals[command];
            buttons[command].sprite = tradables[command];
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        if(win)
        {
            if(Input.GetButtonUp("Accelerate"))
            {
                SceneManager.LoadScene("Main Menu", LoadSceneMode.Single);
            }
        }
        else
        {
            OperateMenu();
            if(Input.GetButtonUp("Accelerate"))
            {
                switch (command)
                {
                    case 0:
                        SceneManager.LoadScene(2, LoadSceneMode.Single);
                        break;
                    case 1:
                        SceneManager.LoadScene("Main Menu", LoadSceneMode.Single);
                        break;
                }
            }
        }
    }
    void OperateMenu()
    {
        changed = Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.LeftArrow)? true : false;
        if(changed)
        {
            buttons[command].sprite = originals[command];
            command = Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.RightArrow)? command-1 : command;
            command = Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.LeftArrow)? command+1 : command;
            command = Mathf.Abs(command);
            command = command % tradables.Length;
            buttons[command].sprite = tradables[command];
        }
    }
}
