using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class UI_Page : UI_PageElement
{
    [HideInInspector]public bool[] overlayState;
    
    public override void Init()
    {
        
    }

    public override void OnClose()
    {
        
    }

    public override void OnOpen()
    {
      
    }

    public void Open()
    {
        UI_Manager.Instance.OpenPage(this);
    }

    public void Close()
    {
        UI_Manager.Instance.ClosePage(false , false);
    }

}
