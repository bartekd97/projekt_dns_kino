using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CannonsHUD : MonoBehaviour {

    public GameObject leftBar;
    public GameObject rightBar;
    public GameObject topBar;
    public GameObject bottomBar;

    private float leftLevel = 0f;
    private float rightLevel = 0f;
    private float topLevel = 0f;
    private float bottomLevel = 0f;
   
    public void SetLeftLevel( float level )
    {
        if (leftLevel == level)
            return;
        leftLevel = level;
        Image img = leftBar.GetComponent<Image>();
        if (level == 1f)
            img.color = Color.green;
        else
            img.color = Color.red;
        Vector3 scale = leftBar.transform.localScale;
        scale.y = level;
        leftBar.transform.localScale = scale;
    }

    public void SetRightLevel(float level)
    {
        if (rightLevel == level)
            return;
        rightLevel = level;
        Image img = rightBar.GetComponent<Image>();
        if (level == 1f)
            img.color = Color.green;
        else
            img.color = Color.red;
        Vector3 scale = rightBar.transform.localScale;
        scale.y = level;
        rightBar.transform.localScale = scale;
    }

    public void SetTopLevel(float level)
    {
        if (topLevel == level)
            return;
        topLevel = level;
        Image img = topBar.GetComponent<Image>();
        if (level == 1f)
            img.color = Color.green;
        else
            img.color = Color.red;
        Vector3 scale = topBar.transform.localScale;
        scale.y = level;
        topBar.transform.localScale = scale;
    }

    public void SetBottomLevel(float level)
    {
        if (bottomLevel == level)
            return;
        bottomLevel = level;
        Image img = bottomBar.GetComponent<Image>();
        if (level == 1f)
            img.color = Color.green;
        else
            img.color = Color.red;
        Vector3 scale = bottomBar.transform.localScale;
        scale.y = level;
        bottomBar.transform.localScale = scale;
    }
}
