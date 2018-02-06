using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Spot : MonoBehaviour, IPointerClickHandler {

	#region Public 

	public CanvasGroup cGroup;
	public int points;

	#endregion 

	public void OnPointerClick(PointerEventData eventData)
	{
		if (eventData.button == PointerEventData.InputButton.Left) 
		{
			
			Debug.Log ("Left click");

			if (gameObject.tag == "blue" && cGroup.alpha >= 1) 
			{

				Debug.Log ("Corretto");

			} 
			else 
			{

				Debug.Log ("Errato");

			}

		} 
		else if (eventData.button == PointerEventData.InputButton.Right) 
		{
			Debug.Log ("Right click");

			if (gameObject.tag == "red" && cGroup.alpha >= 1) {

				Debug.Log ("Corretto");

			} 
			else 
			{

				Debug.Log ("Errato");

			}
		}
	}
		
}
