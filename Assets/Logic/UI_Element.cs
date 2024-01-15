using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UI_Element : MonoBehaviour
{
    public UI_Manager Manager;

    public abstract void Init();

    public abstract void OnOpen();
    public abstract void OnClose();
}
