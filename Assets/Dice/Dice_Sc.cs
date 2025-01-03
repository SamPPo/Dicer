using NUnit.Framework;
using PublicVariables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Dice_Sc : MonoBehaviour, IClickable
{
    private Controller_Sc controller;

    [SerializeField]
    private List<GameObject> faces;
    private GameObject topFace;

    private ISlot slot;


    public void OnClick(Controller_Sc c)
    {
        controller = controller != null ? controller : c;
        GiveRollForce();
    }

    public void PickedUp()
    {
        slot?.TakeFromSlot();
        slot = null;
    }

    IEnumerator DiceMoving()
    {
        yield return new WaitForSeconds(0.1f);
        while (this.GetComponent<Rigidbody>().linearVelocity.magnitude != 0)
        {
            yield return null;
        }
        SetTopFace();
    }

    void GiveRollForce()
    {
        gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * Random.Range(650,800));
        var v = Random.insideUnitSphere.normalized;
        gameObject.GetComponent<Rigidbody>().AddTorque(v * Random.Range(100,150));
        v = Quaternion.AngleAxis(90, Vector3.right) * v;
        gameObject.GetComponent<Rigidbody>().AddTorque(v * Random.Range(100, 150));
        StartCoroutine(DiceMoving());
    }

    public void SetSlot(ISlot s)
    {
        slot = s;
    }

    void SetTopFace()
    {
        float delta = 0;
        foreach (GameObject face in faces)
        {
            var d = face.transform.position.y;
            if (d > delta)
            {
                topFace = face;
                delta = d;
            }
        }
    }

    public FaceType GetTopFaceType()
    {
        if (topFace == null) { SetTopFace(); }
        return topFace.GetComponent<Face_Sc>().GetFaceType();
    }

}
