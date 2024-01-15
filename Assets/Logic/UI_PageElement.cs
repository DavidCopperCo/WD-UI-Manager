using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using NaughtyAttributes;
using System.Linq;
using Unity.VisualScripting;


public abstract class UI_PageElement : UI_Element
{
    [SerializeField]
    [NaughtyAttributes.ReadOnly]
    public List<UI_OverlayActivationIndicator> uI_OverlayActivationIndicators = new List<UI_OverlayActivationIndicator>();

    public void SetOverlayIndicators()
    {
        Debug.Log("HICH");
        List<UI_Overlay> allOverlays = null;
        if (UI_Manager.Instance != null)
            allOverlays = UI_Manager.Instance.allOverlays;
        else
            allOverlays = FindObjectOfType<UI_Manager>().allOverlays;
        List<UI_OverlayActivationIndicator> indicators = new List<UI_OverlayActivationIndicator>();
        foreach (var overlay in allOverlays)
        {
            var found = uI_OverlayActivationIndicators.Find(x => x.overlay == overlay);

            if (found != null)
            {
                indicators.Add(found);
            }
            else
            {
                indicators.Add(new(overlay));
            }
        }
        uI_OverlayActivationIndicators = indicators;
    }

}
       
    



[Serializable]
public class UI_OverlayActivationIndicator
{
    [NaughtyAttributes.ReadOnly]
    [NaughtyAttributes.AllowNesting]
    public UI_Overlay overlay;

    [SerializeField]
    public bool active;

    public UI_OverlayActivationIndicator(UI_Overlay overlay)
    {
        this.overlay = overlay;
        active = false;
    }
}


