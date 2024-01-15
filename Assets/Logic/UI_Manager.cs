using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using NaughtyAttributes;

public class UI_Manager : MonoBehaviour
{
    public static UI_Manager Instance;

    [SerializeField] private UI_Page mainMenuPage;

    [SerializeField] private List<UI_Page> allPages;

    [SerializeField] private GameObject loadingOverlayPage;

   [HideInInspector] public List<UI_PageElement> allOpenElements = new List<UI_PageElement>();

    [Header("Dialogs")]
    [SerializeField] private List<UI_Dialog> allDialogs;
    [SerializeField] private Transform dialogsBg;

    public AlertDialog alertDialog;

    [OnValueChanged("OnAllOverlaysValueChanged")]
    public List<UI_Overlay> allOverlays = new List<UI_Overlay>();

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        OnStart();
    }

    public void OnAllOverlaysValueChanged()
    {
        foreach (var item in allPages)
            item.SetOverlayIndicators();
        foreach (var item in allDialogs)
            item.SetOverlayIndicators();
    }

    private void OnStart()
    {
        DeactiveAllElements();

        OpenPage(mainMenuPage);

    }

    public void OpenPage(UI_PageElement targetPage, bool alsoAddToStack = true)
    {
        if (targetPage == null)
        {
            return;
        }

        dialogsBg.gameObject.SetActive(false);

        CloseDialog(true, false);
        ClosePage(false , false);
        targetPage.gameObject.SetActive(true);
            allOpenElements.Add(targetPage);
        targetPage.OnOpen();
        OverlaysActivation(targetPage);
    }

    public void OverlaysActivation(UI_PageElement targetElement)
    {
        foreach(UI_OverlayActivationIndicator item in targetElement.uI_OverlayActivationIndicators)
        {
            if (item.active)
            {
                item.overlay.gameObject.SetActive(true);
            }
            else
            {
                item.overlay.gameObject.SetActive(false);
            }
        }
        
    }

    public void OpenPage<T>() where T : UI_PageElement
    {
        for (int i = 0; i < allPages.Count; i++)
        {
            if (typeof(T) == allPages[i].GetType())
            {
                OpenPage(allPages[i]);
                return;
            }
        }
    }

    public void OpenDialog(UI_PageElement targetDialog, bool alsoAddToStack = true)
    {
        if (targetDialog != null)
        {
            targetDialog.gameObject.SetActive(true);
            if (alsoAddToStack)
                allOpenElements.Add(targetDialog);
            targetDialog.OnOpen();

            //set the order in the dialogs parent list.
            dialogsBg.SetAsLastSibling();
            targetDialog.transform.SetAsLastSibling();
            dialogsBg.gameObject.SetActive(true);
            OverlaysActivation(targetDialog);
        }

    }

    private void DeactiveAllElements()
    {
        foreach (UI_Page UI in allPages)
        {
            UI.gameObject.SetActive(false);
        }

        foreach (UI_Dialog dialog in allDialogs)
        {
            dialog.gameObject.SetActive(false);
        }

        alertDialog.gameObject.SetActive(false);

        loadingOverlayPage.gameObject.SetActive(false);
    }

    private UI_PageElement GetCurrentActiveElement()
    {
        if (allOpenElements.Count != 0)
        {
            UI_PageElement obj = allOpenElements[allOpenElements.Count - 1];
            return obj;
        }
        else
        {
            return null;
        }
    }

    public void CloseDialog(UI_Dialog dialog , bool alsoRemoveFromList)
    {
        for (int i = allOpenElements.Count - 1; i >= 0; i--)
        {
            if (allOpenElements[i] == dialog)
            {
                if (alsoRemoveFromList)
                    allOpenElements.RemoveAt(i);
                dialog.gameObject.SetActive(false);
                dialog.OnClose();
                dialogsBg.gameObject.SetActive(false);
                return;
            }
        }
    }

    private void CloseDialog(bool all  , bool alsoRemoveFromList)
    {
        if (all)
        {
            if (allOpenElements.Count > 0)
            {
                foreach (UI_PageElement pageElement in allOpenElements)
                {
                    if (pageElement is UI_Dialog)
                    {
                        pageElement.gameObject.SetActive(false);
                        pageElement.OnClose();
                        dialogsBg.gameObject.SetActive(false);
                    }
                }
            }
        }
        else if (GetCurrentActiveElement() is UI_Dialog)
        {
            UI_PageElement dialog = allOpenElements.Last();
            dialog.gameObject.SetActive(false);
            if (alsoRemoveFromList)
                allOpenElements.Remove(dialog);
            dialog.OnClose();
            if (dialog == allOpenElements.Last())
            {
                allOpenElements.Last().gameObject.SetActive(false);
                if (alsoRemoveFromList)
                    allOpenElements.Remove(allOpenElements.Last());
            }
            if (allOpenElements.Last() is not UI_Dialog)
                dialogsBg.gameObject.SetActive(false);
        }
    }

    public void ClosePage(bool alsoRemoveFromStack, bool deletePrePage)
    {
        UI_PageElement pageElement = null;
        if (allOpenElements.Count > 0)
        {
            if (deletePrePage)
            {
                pageElement = GetPreviousActiveElement();
            }
            else
            {
                pageElement = GetCurrentActiveElement();
            }

            if (alsoRemoveFromStack && pageElement == mainMenuPage)
            {
                return;
            }
            else if (pageElement as UI_Page == null)
            {
                for (int i = allOpenElements.Count - 1; i >= 0; i--)
                {
                    pageElement = allOpenElements[i];
                    if (allOpenElements[i] is UI_Page)
                    {
                        pageElement.gameObject.SetActive(false);

                        break;
                    }
                    else
                    {
                        CloseDialog(false, false);
                    }
                }
            }
        }

        if (alsoRemoveFromStack)
        {
            allOpenElements.RemoveAt(allOpenElements.Count - 1);
        }

        if (pageElement != null)
        {
            pageElement.gameObject.SetActive(false);
            pageElement.OnClose();
        }

    }
    
    private UI_PageElement GetPreviousActiveElement()
    {
        if (allOpenElements.Count != 0)
        {
            UI_PageElement obj = allOpenElements[allOpenElements.Count - 2];
            return obj;
        }
        else
        {
            return null;
        }
    }

    public void BackPress()
    {
            if (!alertDialog.isActiveAndEnabled)
        {
            var activeElement = GetCurrentActiveElement();
            if (activeElement != null)
            {
                if (CurrentActiveElementIsPage() && activeElement != mainMenuPage)
                {
                    ClosePage(true, false) ;

                    // should open all the previous dialogs (after the curretnt page)
                    List<UI_Dialog> dialogsList = new List<UI_Dialog>();
                    UI_Page lastPage = null;
                    foreach (UI_PageElement obj in allOpenElements)
                    {
                        if (obj is UI_Page)
                        {
                            lastPage = (UI_Page)obj;
                        }
                        if (obj is UI_Dialog)
                        {
                            dialogsList.Add(obj as UI_Dialog);
                        }
                    }

                    OpenPage(lastPage, false);

                    foreach (var item in dialogsList)
                    {
                        OpenDialog(item, true);
                    }
                }
                else
                {
                    CloseDialog(false , true);
                    OverlaysActivation(GetCurrentActiveElement());
                }
            }
        }
    }

    private bool CurrentActiveElementIsPage()
    {
        return GetCurrentActiveElement() is UI_Page;
    }

}
