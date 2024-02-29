using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBoundary : MonoBehaviour
{
    public delegate void HitWall();
    public static HitWall wallEvent;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerExit2D(Collider2D collision) {
        wallEvent();
    }
}
