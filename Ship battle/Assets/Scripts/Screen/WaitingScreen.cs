using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingScreen : MonoBehaviour {

    public static WaitingScreen screen;
	void Start () {
        screen = this;
        gameObject.SetActive(false);
    }
}
