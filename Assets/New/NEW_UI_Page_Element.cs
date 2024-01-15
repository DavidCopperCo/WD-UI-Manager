using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NEW_UI_Page_Element : MonoBehaviour
{
    public abstract void Init();

    public abstract void OnOpen();
    public abstract void OnClose();
}
