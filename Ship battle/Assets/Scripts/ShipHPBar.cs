using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipHPBar : MonoBehaviour {

    public Ship ship;
    public Image barBackground;
    public Image barColor;
    public Transform barScale;
	
	void Update () {
        float hpLevel = ship.CurrentHealth / ship.MaximumHealth;
        if (hpLevel == 0f)
        {
            gameObject.SetActive(false);
            return;
        }

        float r, g;
        if (hpLevel > 0.5f)
        {
            g = 1f;
            r = (1f - hpLevel) * 2f;
        }
        else
        {
            g = hpLevel * 2f;
            r = 1f;
        }

        Vector3 scale = barScale.localScale;
        scale.x = hpLevel;
        barScale.localScale = scale;

        barColor.color = new Color(r, g, 0f);
        barBackground.color = new Color(r*0.25f, g*0.25f, 0f);
    }
}
