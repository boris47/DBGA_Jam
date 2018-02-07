using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class Spot : MonoBehaviour, IPointerClickHandler {

	public bool interactable = true;

	

	public void OnPointerClick(PointerEventData eventData)
	{
		if (eventData.button == PointerEventData.InputButton.Left) 
		{
			if (gameObject.tag == "blue" && interactable) 
			{
				transform.parent.GetComponent<StandardEnemy>().OnClick( this );
				print( "Dio merda sono blue" );
			} 
			else 
			{
				transform.parent.GetComponent<StandardEnemy>().OnWrongClick( this );
				print( "Dio merda sono blue" );
			}

		} 
		else if (eventData.button == PointerEventData.InputButton.Right) 
		{
			if (gameObject.tag == "red" && interactable)
			{
				transform.parent.GetComponent<StandardEnemy>().OnClick( this );
				print( "Dio merda sono rosso" );
			} 
			else
			{
				transform.parent.GetComponent<StandardEnemy>().OnWrongClick( this );
				print( "Sbagliato, sono rosso" );
			}
		}
	}
		
		
}
