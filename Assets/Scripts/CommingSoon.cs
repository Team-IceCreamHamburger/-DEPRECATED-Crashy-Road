using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommingSoon : MonoBehaviour
{
    public GameObject selectButton;


    void Update() {
        if (gameObject.activeInHierarchy && gameObject.tag == "CommingSoon") {
            SelectBtnState(false);
        }
        else if (gameObject.activeInHierarchy && gameObject.tag == "Launch") {
            SelectBtnState(true);
        }
    }


    private void SelectBtnState(bool state) {
        selectButton.SetActive(state);
    }
}
