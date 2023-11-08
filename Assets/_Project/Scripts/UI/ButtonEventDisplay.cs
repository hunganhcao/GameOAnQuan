using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;


[RequireComponent(typeof(Button))]
public class ButtonEventDisplay : MonoBehaviour
{
    [Header("Cusytom")]
    [SerializeField] public GameObject objHightlight;
    [SerializeField] public GameObject objPressed;
    [SerializeField] public GameObject objOver;
    [SerializeField] public GameObject objDisable;
    [SerializeField] private Button btn;
    private bool _listionEvent = true;

    public UnityEvent onClick => btn.onClick;
    public bool interactable
    {
        get => btn.interactable;
        set
        {
            btn.interactable = value;
            if (value)
            {
                SetOneState(objHightlight);
            }
            else
            {
                SetOneState(objDisable);
            }
        }
    }
    public void SetNormal()
    {
        interactable = true;
        SetOneState(objHightlight);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (interactable && _listionEvent)
            SetOneState(objPressed);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (interactable && _listionEvent)
            SetOneState(objHightlight);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (interactable && _listionEvent) SetOneState(objOver);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (interactable && _listionEvent) SetOneState(objHightlight);
    }
    protected void Awake()
    {
        if (objPressed == null) objPressed = objHightlight;
        if (objOver == null) objOver = objHightlight;
        if (interactable && _listionEvent)
            SetOneState(objHightlight);
        else
            SetOneState(objDisable);
    }
    private void SetOneState(GameObject obj)
    {
        HideAll();
        if (obj != null)
            obj.SetActive(true);
    }
    private void HideAll()
    {
        if (objHightlight != null)
            objHightlight.SetActive(false);
        if (objPressed != null)
            objPressed.SetActive(false);
        if (objOver != null)
            objOver.SetActive(false);
        if (objDisable != null)
            objDisable.SetActive(false);
    }
    public void RegisterOnClick(Action onclick)
    {
        btn.RegisterOnClick(onclick);
    }
}
