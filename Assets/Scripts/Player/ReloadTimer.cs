using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReloadTimer : MonoBehaviour {

    private void PlayReloadAnim(float time) {
        LeanTween.value(gameObject, UpdateCallback, 0, 1, time);
    }

    private void UpdateCallback(float value) {
        gameObject.GetComponent<Image>().fillAmount = value;
    }
}
