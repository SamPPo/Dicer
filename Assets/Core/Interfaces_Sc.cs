using PublicVariables;
using UnityEngine;


public interface IClickable
{
    void OnClick(Controller_Sc c);
}

public interface ISlot
{
    void FillSlot();
    void TakeFromSlot();
    bool GetIsFilled();
    FaceType GetFaceType();
}

public class Interfaces_Sc : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
