using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fruit : MonoBehaviour {

	public GameObject fruitSlicedPrefab;
	public float startForce = 15f;

	Rigidbody2D rb;

	void Start ()
	{
		rb = GetComponent<Rigidbody2D>();
		rb.AddForce(transform.up * startForce, ForceMode2D.Impulse);
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.tag == "Blade")
		{
			Vector3 direction = (col.transform.position - transform.position).normalized;

			Quaternion rotation = Quaternion.LookRotation(direction);

			GameObject slicedFruit = Instantiate(fruitSlicedPrefab, transform.position, rotation);
			Destroy(slicedFruit, 3f);
			Destroy(gameObject);

			GameObject.FindGameObjectWithTag ("Score").GetComponent<Manager> ().score += 1;
			int value = GameObject.FindGameObjectWithTag ("Score").GetComponent<Manager> ().score;
			GameObject.FindGameObjectWithTag ("Score").GetComponent<Text>().text = "Score: " + value;

		}
	}

}
