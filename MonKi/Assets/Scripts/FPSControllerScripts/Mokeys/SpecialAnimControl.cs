using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAnimControl : MonoBehaviour
{
    public InteracterScript script;

    public void EndAnim()
    {
        script.AnimationRetake();
    }
}
