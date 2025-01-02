using PublicVariables;
using UnityEngine;
using UnityEngine.Rendering;

public class Slot_Sc : MonoBehaviour, ISlot
{
    [SerializeField]
    private Texture2D texture;
    [SerializeField]
    private FaceType faceType;

    private bool isFilled = false;

    public void FillSlot()
    {
        isFilled = true;
    }

    public void TakeFromSlot()
    {
        isFilled = false;
    }

    void Start()
    {
        this.GetComponent<Renderer>().material.mainTexture = texture;
    }

    public bool GetIsFilled()
    {
        return isFilled;
    }

    public FaceType GetFaceType()
    {
        return faceType;
    }
}

