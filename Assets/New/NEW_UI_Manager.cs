using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NEW_UI_Manager : MonoBehaviour
{

    [SerializeField] private List<NEW_UI_Page_Base> allPages;
    [SerializeField] private List<NEW_UI_Dialog_Base> allDialogs;

    [SerializeField] private GameObject loadingOverlayPage;

    [SerializeField] private List<NEW_UI_Page_Element> allElementsStack;

    public AlertDialog alertDialog;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenPage(NEW_UI_Page_Base page)
    {
        // disable the current page
    }


    public NEW_UI_Page_Base GetCurrentActivePage()
    {
        for (int i = allElementsStack.Count - 1; i >= 0; i--)
        {
            if (allElementsStack[i] is NEW_UI_Page_Base)
            {
                return allElementsStack[i] as NEW_UI_Page_Base;
            }
        }

        return null;
    }
}
