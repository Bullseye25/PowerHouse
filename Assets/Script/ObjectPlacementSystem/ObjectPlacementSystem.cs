using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ObjectPlacementSystem : MonoBehaviour
{
    [SerializeField] private GameObject objectPrefab;

    [SerializeField] private GameObject objectHolder;

    [SerializeField] private bool isPlaced;

    [SerializeField] private ARRaycastManager raycastManager;

    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private void Start()
    {
        isPlaced = false;
        raycastManager = GetComponent<ARRaycastManager>();
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            if (raycastManager.Raycast(Input.GetTouch(0).position, hits, TrackableType.PlaneWithinPolygon))
            {
                var hitPos = hits[0].pose;

                if (!isPlaced)
                {
                    objectHolder = Instantiate(objectPrefab, hitPos.position, hitPos.rotation);
                    isPlaced = true;
                }
                else
                {
                    objectHolder.transform.position = hitPos.position;
                    var lookValue = objectHolder.transform.position - hitPos.position;

                    /*                    objectHolder.transform.LookAt(lookValue);
                                        objectHolder.transform.rotation = Quaternion.Euler(0f, -objectHolder.transform.rotation.eulerAngles.y, objectHolder.transform.rotation.z);
                    */

                    var rotation = Quaternion.Euler(hitPos.rotation.eulerAngles + Quaternion.Euler(0f, -90f, -90f).eulerAngles);
                    objectHolder.transform.rotation = Quaternion.Euler(0, rotation.eulerAngles.y, 0);

                }
            }
        }
    }
}