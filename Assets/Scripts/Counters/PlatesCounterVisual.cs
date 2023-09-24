using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour {
    
    [SerializeField] private PlatesCounter platesCounter;
    [SerializeField] private Transform plateVisualPrefab;
    [SerializeField] private Transform counterTopPoint;

    private List<GameObject> plateVisualList = new();

    private void Start() {
        platesCounter.OnPlateSpawned += PlatesCounter_OnPlateSpawned;
        platesCounter.OnPlateRemoved += PlatesCounter_OnPlateRemoved;
    }

    private void PlatesCounter_OnPlateSpawned(object sender, System.EventArgs e) {
        Transform plateVisualTransform = Instantiate(plateVisualPrefab, counterTopPoint);

        float plateOffsetY = .1f;
        plateVisualTransform.localPosition = new Vector3(0f, plateOffsetY * plateVisualList.Count, 0f);

        plateVisualList.Add(plateVisualTransform.gameObject);
    }

    private void PlatesCounter_OnPlateRemoved(object sender, System.EventArgs e) {
        GameObject plateVisualGameObject = plateVisualList[plateVisualList.Count - 1];
        plateVisualList.Remove(plateVisualGameObject);
        Destroy(plateVisualGameObject);
    }

}
