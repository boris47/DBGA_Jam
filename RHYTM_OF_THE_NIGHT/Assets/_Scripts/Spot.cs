using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Spot : MonoBehaviour, IPointerClickHandler {

	#region Public 

	public bool big;

	#endregion 

	public void OnPointerClick(PointerEventData eventData)
	{
		if (eventData.button == PointerEventData.InputButton.Left) 
		{
			
			Debug.Log ("Left click");

			if (gameObject.tag == "blue") {

				Debug.Log ("Corretto");

			} 
			else 
			{

				Debug.Log ("Errato");

			}

		} 
		else if (eventData.button == PointerEventData.InputButton.Middle) 
		{
			Debug.Log ("Middle click");
		}
		else if (eventData.button == PointerEventData.InputButton.Right) 
		{
			Debug.Log ("Right click");

			if (gameObject.tag == "red") {

				Debug.Log ("Corretto");

			} 
			else 
			{

				Debug.Log ("Errato");

			}
		}
	}
}
