using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchHandler : MonoBehaviour
{
    void Start()
    {
        addPhysicsRaycaster();
    }

    void addPhysicsRaycaster()
    {
        PhysicsRaycaster physicsRaycaster = FindObjectOfType<PhysicsRaycaster>();
        if (physicsRaycaster == null)
        {
            Camera.main.gameObject.AddComponent<PhysicsRaycaster>();
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray raycast = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit raycastHit;
            if (Physics.Raycast(raycast, out raycastHit))
            {
                Debug.Log("Something Hit," + raycastHit.collider.gameObject);
                IClikable clickable = raycastHit.collider.gameObject.GetComponent(typeof(IClikable)) as IClikable;
                if (clickable != null)
                {
                    clickable.OnClick();

                }
            }
        }
    }

}
