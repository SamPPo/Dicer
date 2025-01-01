using System.Collections;
using UnityEngine;

public class Controller_Sc : MonoBehaviour
{

    private Ray ray;
    private Vector3 floorHit;

    // Update is called once per frame
    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastFloor();

        if (Input.GetMouseButtonDown(1))
        {
            ClickUnderCursor();
        }

        if (Input.GetMouseButtonDown(0))
        {
            PickupUnderCursor();
        }
    }

    void ClickUnderCursor()
    {
        var clicked = Raycast();
        if (clicked != null)
        {
            var clickable = clicked.GetComponent<MonoBehaviour>() as IClickable;
            clickable.OnClick(this);
        }
    }

    void PickupUnderCursor()
    {
        var clicked = Raycast();
        if (clicked != null)
        {
            StartCoroutine(MoveObject(clicked));
        }

    }

    IEnumerator MoveObject(GameObject obj)
    {
        while (Input.GetMouseButton(0))
        {
            obj.transform.position = floorHit + Vector3.up * 2;
            yield return null;
        }
    }

    GameObject Raycast()
    {
        if (Physics.Raycast(ray, out RaycastHit hit, 100))
        {
            if (hit.collider.gameObject.GetComponent<MonoBehaviour>() is IClickable)
            {
                return hit.collider.gameObject;
            }
        }
        return null;
    }

    void RaycastFloor()
    {
        int layer = 1 << 6;
        if (Physics.Raycast(ray, out RaycastHit hit, 100, layer))
        {
            floorHit = hit.point;
        }
    }
}
