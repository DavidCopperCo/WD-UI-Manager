using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UI_Dialog : UI_PageElement
{
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
        UI_Manager.Instance.OpenDialog(this);
    }
    public void Close()
    {
        UI_Manager.Instance.CloseDialog(this , false);
    }
}
