using UnityEngine;
using PublicVariables;

public class Face_Sc : MonoBehaviour
{
    [SerializeField]
    private Texture2D texture;
    [SerializeField]
    private FaceType faceType;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.GetComponent<Renderer>().material.mainTexture = texture;
    }

    public FaceType GetFaceType()
    {
        return faceType;
    }

}
