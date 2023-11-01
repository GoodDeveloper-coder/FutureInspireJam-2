using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameManager : MonoBehaviour
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

    private Rigidbody2D zoneRB;

    private Vector2 zoneTarget;

    // Start is called before the first frame update
    void Start()
    {
        zoneRB = zone.GetComponent<Rigidbody2D>();
        zone.transform.localScale = Vector3.one * initialZoneSize;
        zoneTarget = new Vector2(Random.Range(initialZoneSize / 2 - zoneBoundary.x, zoneBoundary.x - initialZoneSize / 2), Random.Range(initialZoneSize / 2 - zoneBoundary.y, zoneBoundary.y - initialZoneSize / 2));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        zoneRB.MovePosition(zoneRB.position + (zoneTarget - zoneRB.position).normalized * initialZoneSpeed * Time.deltaTime);
        if ((zoneTarget - zoneRB.position).magnitude < 0.1f) zoneTarget = new Vector2(Random.Range(initialZoneSize / 2 - zoneBoundary.x, zoneBoundary.x - initialZoneSize / 2), Random.Range(initialZoneSize / 2 - zoneBoundary.y, zoneBoundary.y - initialZoneSize / 2));
    }
}
