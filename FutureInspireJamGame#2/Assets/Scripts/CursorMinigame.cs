using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorMinigame : MonoBehaviour
{
    [SerializeField] private GameObject cursor;
    [SerializeField] private GameObject zone;

    [SerializeField] private GameObject cloudPrefab;

    [SerializeField] private Vector2 zoneBoundary;

    [SerializeField] private float initialZoneSize;
    [SerializeField] private float zoneSizeDecrementFactor;
    [SerializeField] private float initialZoneSpeed;
    [SerializeField] private float zoneSpeedIncrementFactor;
    [SerializeField] private float initialMinCloudGeneration;
    [SerializeField] private float initialMaxCloudGeneration;
    [SerializeField] private float cloudGenerationDecrementFactor;

    private Rigidbody2D cursorRB;

    private Vector3 zoneTarget;

    private bool outOfRange;

    // Start is called before the first frame update
    void Start()
    {
        cursorRB = cursor.GetComponent<Rigidbody2D>();
        zone.transform.localScale = Vector3.one * initialZoneSize;
        zoneTarget = new Vector3(Random.Range(initialZoneSize / 2 - zoneBoundary.x, zoneBoundary.x - initialZoneSize / 2), Random.Range(initialZoneSize / 2 - zoneBoundary.y, zoneBoundary.y - initialZoneSize / 2), 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (outOfRange) return;
        cursorRB.MovePosition(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        zone.transform.position = Vector3.MoveTowards(zone.transform.position, zoneTarget, initialZoneSpeed * Time.deltaTime);
        Vector2 zonePosition = new Vector2(zone.transform.position.x, zone.transform.position.y);
        if (Vector2.Distance(zonePosition, new Vector2(zoneTarget.x, zoneTarget.y)) < 0.1f) zoneTarget = new Vector3(Random.Range(initialZoneSize / 2 - zoneBoundary.x, zoneBoundary.x - initialZoneSize / 2), Random.Range(initialZoneSize / 2 - zoneBoundary.y, zoneBoundary.y - initialZoneSize / 2), 0);
        outOfRange = Vector2.Distance(cursorRB.position, zonePosition) > initialZoneSize / 2;
    }
}
