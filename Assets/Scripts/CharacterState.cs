using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class CharacterState : MonoBehaviour
{
    private Vector2 velocity;
    private BoxCollider2D hitBox;

    // Start is called before the first frame update
    void Start()
    {
        hitBox = GetComponent<BoxCollider2D>();
        Cursor.lockState = CursorLockMode.Confined;
        WallBoundary.wallEvent += test;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 lastPos = transform.position;
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        velocity = (Vector2)transform.position - lastPos;
    }

    public void test() {
        Debug.Log("I hit the wall!");
    }
}
