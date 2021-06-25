using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End : MonoBehaviour
{
    private void Start() {
        Cursor.lockState = CursorLockMode.None;
    }
    public void Endit()
    {
        Application.Quit();
    }
}
