using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class TabButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private TabGroup tabOwner;
    [SerializeField] private GameObject content;

    private RectTransform rectTransform;

    //private bool buttonActive;

    private Vector2 startPos;
    private Vector2 goalPos;

    private bool hasMoved;

    private Image background; public Image Background { 
        get
        {
            if (background == null)
            {
                background = GetComponent<Image>();
            }
            return background;
        }
        set 
        { 
            if (background == null)
            {
                background = GetComponent<Image>();
            }
            background = value;
        } }

    public void OnPointerClick(PointerEventData eventData)
    {
        tabOwner.OnTabClick(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tabOwner.OnTabEnter(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tabOwner.OnTabExit(this);
    }

    public void MoveToward(Vector2 displacement)
    {
        goalPos = startPos + displacement;

        hasMoved = true;
    }

    public void SetContentActive(bool flag)
    {
        if (content != null)
        {
            content.SetActive(flag);
        }
        else
        {
            Debug.Log(this.name + " has no Content...");
        }
    }

    public void SetOwner(TabGroup group)
    {
        tabOwner = group;
    }

    // Start is called before the first frame update
    void Start()
    {
        background = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();

        startPos = rectTransform.anchoredPosition;
        goalPos = startPos;

        hasMoved = false;

        tabOwner.Subscribe(this);
    }

    private void FixedUpdate()
    {
        //Debug.Log(rectTransform.position);
        if (hasMoved)
        {
            rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, goalPos, 0.1f);
        }

        //transfo.position = Vector3.Lerp(transform.position, goalPos, 0.1f);
    }
}
