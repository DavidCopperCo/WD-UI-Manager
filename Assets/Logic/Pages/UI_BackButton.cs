using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_BackButton : UI_Overlay
{
    public void B_Back()
    {
        Manager.BackPress();
    }
}
