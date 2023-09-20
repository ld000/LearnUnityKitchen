using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour {

    [SerializeField] KitchenObjectSO kitchenObjectSO;
    [SerializeField] Transform counterTopPoint;
    
    public void Interact() {
        Debug.Log("Interacted with ClearCounter");
        Transform kitchenObjectSOTransform = Instantiate(kitchenObjectSO.prefab, counterTopPoint);
        kitchenObjectSOTransform.localPosition = Vector3.zero;
    }

}
