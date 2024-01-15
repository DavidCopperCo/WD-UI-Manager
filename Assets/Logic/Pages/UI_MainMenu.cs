using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_MainMenu : UI_Page
{
    [SerializeField] public UI_PageElement gameMenuObject;

    public void B_Start()
    {
        Manager.OpenPage(gameMenuObject , true);
    }

    public void B_Exit()
    {
        Manager.alertDialog.ShowYesNoDialog("", null, "Yes", null, "Do You want to Quit?");
    }

    public void B_ClaimReward()
    {
            Manager.OpenDialog(this);
    }

}
