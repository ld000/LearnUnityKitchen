using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour {
    
    [SerializeField] private GameObject hasProgessGameObject;
    [SerializeField] private Image barImage;

    private IHasProgess hasProgess;

    private void Start() {
        hasProgess = hasProgessGameObject.GetComponent<IHasProgess>();
        if (hasProgess == null) {
            Debug.LogError("hasProgess is null");
        }

        hasProgess.OnProgressChanged += HasProgess_OnProgressChanged;

        barImage.fillAmount = 0f;

        Hide();
    }

    private void HasProgess_OnProgressChanged(object sender, IHasProgess.OnProgressChangedEventArgs e) {
        barImage.fillAmount = e.progressNormalized;

        if (e.progressNormalized == 0f || e.progressNormalized >= 1f) {
            Hide();
        } else {
            Show();
        }
    }

    private void Hide() {
        gameObject.SetActive(false);
    }

    private void Show() {
        gameObject.SetActive(true);
    }

}
