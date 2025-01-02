using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;

public class Controller_Sc : MonoBehaviour
{
    private Ray ray;                                // Ray used for raycasting
    private Vector3 floorHit;                       // Position where the ray hits the floor
    private Vector3 upOffset = Vector3.up * 2f;     // Offset to lift objects above the floor
    private GameObject pickup;                      // Object that was picked up


    void Update()
    {
        // Create a ray from the camera to the mouse position
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Check for right mouse button click
        if (Input.GetMouseButtonDown(1))
        {
            ClickUnderCursor();
        }

        // Check for left mouse button click
        if (Input.GetMouseButtonDown(0))
        {
            PickupUnderCursor();
        }
    }
    // Handle clicking on objects under the cursor
    void ClickUnderCursor()
    {
        var clicked = RaycastForInterface<IClickable>();
        if (clicked != null)
        {
            // Get the IClickable component and call OnClick
            var clickable = clicked.GetComponent<MonoBehaviour>() as IClickable;
            clickable?.OnClick(this);
        }
    }

    // Handle picking up objects under the cursor
    void PickupUnderCursor()
    {
        var clicked = RaycastForInterface<IClickable>();
        if (clicked != null)
        {
            pickup = clicked;
            pickup.GetComponent<Dice_Sc>().PickedUp();
            // Start coroutine to move the object
            StartCoroutine(MoveObject());
        }
    }

    // Coroutine to move the object while the left mouse button is held down
    IEnumerator MoveObject()
    {
        var floorHitPos = Vector3.zero;
        ISlot slotMatch;

        while (pickup != null)
        {
            // Local function to raycast to the floor and check for slots
            Vector3 RaycastForMatchingSlots(GameObject obj)
            {
                slotMatch = null;
                int layer = 1 << 6; // Layer mask for the floor
                if (Physics.Raycast(ray, out RaycastHit hit, 100, layer))
                {
                    floorHitPos = hit.point;
                    if (hit.collider.gameObject.GetComponent<MonoBehaviour>() is ISlot slot)
                    {
                        // Check if the slot is not filled and matches the dice face type
                        if (!slot.GetIsFilled() && slot.GetFaceType() == obj.GetComponent<Dice_Sc>().GetTopFaceType())
                        {
                            slotMatch = slot;
                            floorHitPos = hit.transform.position;
                            return hit.transform.position;
                        }
                            
                    }
                }
                return Vector3.zero;
            }

            // Get the position of the slot or default to zero
            var slotPos = RaycastForMatchingSlots(pickup);
            if (slotPos != Vector3.zero)
            {
                floorHit = slotPos;
                upOffset = Vector3.up * 0.7f; // Lower offset when hovering on acceptable slot
            }
            else
            {
                upOffset = Vector3.up * 2f; // Default offset
            }

            // Move the object to the new position
            pickup.transform.position = floorHitPos + upOffset;
            RaycastForMatchingSlots(pickup);

            if (Input.GetMouseButtonUp(0))
            {
                slotMatch?.FillSlot();
                pickup.GetComponent<Dice_Sc>().SetSlot(slotMatch);
                pickup = null;
            }

            yield return null;
        }
    }

    // Perform a raycast and return the clicked object if it implements IClickable
    GameObject RaycastForInterface<T>() where T : class
    {
        if (Physics.Raycast(ray, out RaycastHit hit, 100))
        {
            if (hit.collider.gameObject.GetComponent<MonoBehaviour>() is T)
            {
                return hit.collider.gameObject;
            }
        }
        return null;
    }
}
