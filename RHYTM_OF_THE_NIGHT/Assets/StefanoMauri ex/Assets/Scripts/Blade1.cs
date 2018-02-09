using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blade1 : MonoBehaviour
{

    public GameObject bladeTrailPrefab;

    Vector2 previousPosition;

    GameObject currentBladeTrail;

    Rigidbody2D rb;
    Camera cam;
    CircleCollider2D circleCollider;

    void Start ()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();

        StartCutting();
    }

    // Update is called once per frame
    void Update ()
    {
        Vector2 newPosition = cam.ScreenToViewportPoint(Input.mousePosition);
        rb.position = newPosition;

        float velocity = (newPosition - previousPosition).magnitude * Time.deltaTime;

        previousPosition = newPosition;
    }

    void StartCutting ()
    {
        currentBladeTrail = Instantiate(bladeTrailPrefab, transform);
        previousPosition = cam.ScreenToWorldPoint(Input.mousePosition);
        circleCollider.enabled = false;
    }
}
