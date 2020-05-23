using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAction : MonoBehaviour
{
    public enum ObjectType {SpeedBoost, Ramp};
    public ObjectType objectType;

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            switch (objectType)
            {
                case ObjectType.SpeedBoost:
                    coll.gameObject.GetComponent<Whaleburt>().SpeedBoost();
                    break;
                case ObjectType.Ramp:
                    coll.gameObject.GetComponent<Whaleburt>().Trick();
                    break;
            }            
        }
    }
}
