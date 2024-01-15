using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using static UI_BaseUIType;


public class AlertDialog : UI_BaseUIType
{
    public static AlertDialog Instance;

    [SerializeField] private TMP_Text contentTxt;
    [SerializeField] private AlertDialogButton singleDialogButton;
    [SerializeField] private AlertDialogButton yesButton, noButton;

    public AlertDialog()
    {
        Instance = this;
    }

    public void ShowSingleButtonDialog(string content, Action onPlayerSubmit = null, string buttonTxt = null)
    {
        DisableAllButtong();

        contentTxt.text = content;

        singleDialogButton.SetButtonListener(onPlayerSubmit == null ? CloseDialog : onPlayerSubmit);
        singleDialogButton.SetText(buttonTxt == null ? "Submit" : buttonTxt);

        singleDialogButton.SetActive(true);

        this.gameObject.SetActive(true);
    }

    public void ShowQuickSingleButtonMessage(string content)
    {
        content = content.Replace("\\n", "\n");

        ShowSingleButtonDialog(content);
    }

    public void ShowYesNoDialog(string content, Action onYesButtonClicked = null, string yesButtonTxt = null, Action onNoButtonClicked = null, string noButtonTxt = null)
    {
        DisableAllButtong();

        contentTxt.text = content;

        yesButton.SetButtonListener(onYesButtonClicked == null ? CloseDialog : onYesButtonClicked);
        yesButton.SetText(yesButtonTxt == null ? "Yes" : yesButtonTxt);

        noButton.SetButtonListener(onNoButtonClicked == null ? CloseDialog : onNoButtonClicked);
        noButton.SetText(noButtonTxt == null ? "No" : noButtonTxt);

        yesButton.SetActive(true);
        noButton.SetActive(true);

        this.gameObject.SetActive(true);
    }

    private void DisableAllButtong()
    {
        singleDialogButton.SetActive(false);
        yesButton.SetActive(false);
        noButton.SetActive(false);
    }

    public void CloseDialog()
    {
        this.gameObject.SetActive(false);
    }
}

[Serializable]
public struct AlertDialogButton
{
    [SerializeField]
    private Button btn;
    [SerializeField]
    private TMP_Text text;


    public void SetButtonListener(Action action)
    {
        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(() =>
        {
            AlertDialog.Instance.CloseDialog();
            action();
        });
    }

    public void SetText(string text)
    {
        if (this.text != null)
        {
            this.text.text = text; // ;)
        }
    }

    public void SetActive(bool active)
    {
        btn.gameObject.SetActive(active);
    }

    

}