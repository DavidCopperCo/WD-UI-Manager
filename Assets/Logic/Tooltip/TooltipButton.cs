using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IPointerExitHandler
{
    public string message;

    public float paddingXSize;
    public float paddingYSize;

    //< for hoplding touch on button >
    public void OnPointerDown(PointerEventData eventData)
    {
        TooltipScreenSpaceUI.instance.targetObject = gameObject;
        TooltipScreenSpaceUI.instance.textPaddingXSize = paddingXSize;
        TooltipScreenSpaceUI.instance.textPaddingYSize = paddingYSize;
        TooltipScreenSpaceUI.instance.ShowTooltip(message);
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {

    }
    
    // use this for hoverover 
    //public void OnPointerEnter(PointerEventData eventData)
    //{
    //    TooltipManager.instance.SetAndShowTooltop(message);
    //}

    public void OnPointerExit(PointerEventData eventData)
    {
    }

}
