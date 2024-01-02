using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlantPlacementManager : MonoBehaviour
{
    public GameObject[] flowers;

    public XROrigin sessionOrigin;
    public ARRaycastManager raycastManager;
    public ARPlaneManager planeManager;

    private List<ARRaycastHit> raycastHits = new List<ARRaycastHit>();


    private void Update()
    {
        if (Input.GetTouch(0).phase == TouchPhase.Began)
        {
            // Shoot raycast
            bool collision = raycastManager.Raycast(Input.mousePosition, raycastHits, TrackableType.PlaneWithinPolygon);

            // Randomly place objects
            if (collision)
            {
                GameObject obj = Instantiate(flowers[Random.Range(0, flowers.Length - 1)]);
                obj.transform.position = raycastHits[0].pose.position;
            }

            // Disable the planes and the plane manager
            foreach (var plane in planeManager.trackables)
            {
                plane.gameObject.SetActive(false);
            }

            planeManager.enabled = false;
        }
    }

}
