using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameCursor : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool outOfRange;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        rb.MovePosition(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        //rb.MovePosition(Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()));
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.tag == "Zone") outOfRange = true;
    }

    public bool GetOutOfRange()
    {
        return outOfRange;
    }
}
