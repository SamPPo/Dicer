using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using PublicVariables;
using System.Linq;

public class Location_Sc : MonoBehaviour
{
    [SerializeField]
    private List<Slot_Sc> slots;

    public void CheckIfAllSlotsFilled()
    {
        int i = slots.Count(slot => slot.GetIsFilled());
        if (i == slots.Count)
        {
            //Implement location trigger action here
        }
    }


}
