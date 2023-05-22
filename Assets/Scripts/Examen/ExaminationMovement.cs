
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ExaminationMovement : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject inspectionCircle;
    [SerializeField] private GameObject inspectionCircleAnimated;
    private bool isHovering;
    private InputClick inputClickScript;
    [SerializeField]private RectTransform majorPointTransform;
    [SerializeField]private RectTransform minorPointTransform;

    private void Start() 
    {
        inspectionCircle.transform.position = InputsManager.Instance.ReadMousePostionValue();
        majorPointTransform = transform.GetChild(0).GetComponent<RectTransform>();
        inputClickScript = GetComponent<InputClick>();
        inspectionCircleAnimated.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData ctx)
    {
        inspectionCircle.SetActive(true);
        isHovering = true;
        CursorsManager.instance.HideCursor();
    }

    public void OnPointerExit(PointerEventData ctx)
    {
        inspectionCircle.SetActive(false);
        isHovering = false;
        CursorsManager.instance.DisplayCursor();
    }

    private void Update() 
    {
        if(!isHovering)
        return;
        inspectionCircle.transform.position = InputsManager.Instance.ReadMousePostionValue();

        if(RectTransformUtility.RectangleContainsScreenPoint(majorPointTransform,inspectionCircle.transform.position))
        {
            inspectionCircleAnimated.SetActive(true);
            inputClickScript.informationsPoints = majorPointTransform.GetComponent<IntrestPoint>();
            inspectionCircleAnimated.transform.position = InputsManager.Instance.ReadMousePostionValue();
            inputClickScript.intrestPointType = 1f;
            inputClickScript.enabled = true;
        }
        else if (RectTransformUtility.RectangleContainsScreenPoint(minorPointTransform, inspectionCircle.transform.position))
        {
            inspectionCircleAnimated.SetActive(true);
            inputClickScript.informationsPoints = minorPointTransform.GetComponent<IntrestPoint>();
            inspectionCircleAnimated.transform.position = InputsManager.Instance.ReadMousePostionValue();
            inputClickScript.intrestPointType = 2f;
            inputClickScript.enabled = true;
        }
        else
        {
            inspectionCircleAnimated.SetActive(false);
            inputClickScript.informationsPoints = null;
            inputClickScript.enabled = false;
        }
    }
}
