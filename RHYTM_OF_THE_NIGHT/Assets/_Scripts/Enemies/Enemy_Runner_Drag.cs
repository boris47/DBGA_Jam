using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System;

public class Enemy_Runner_Drag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    private RectTransform m_DraggingPlane = null;


    public void OnBeginDrag(PointerEventData eventData)
    {
        m_DraggingPlane = transform as RectTransform;
    }



    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.pointerEnter != null && eventData.pointerEnter.transform as RectTransform != null)
            m_DraggingPlane = eventData.pointerEnter.transform as RectTransform;

        Vector3 globalMousePos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(m_DraggingPlane, eventData.position, eventData.pressEventCamera, out globalMousePos))
        {
            transform.position = globalMousePos;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    { 
        if (collision.transform.name == "DragPoint1"|| collision.transform.name == "DragPoint2")
        {
            transform.parent.GetComponent<EnemyRunner>().OnKill();
            // muore, spawn next enemy

        }
    }

}
