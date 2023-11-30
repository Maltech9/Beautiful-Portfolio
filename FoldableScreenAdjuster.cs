using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoldableScreenAdjuster : MonoBehaviour
{
    // Start is called before the first frame update

    public Canvas canvas;
    public CanvasScaler.ScaleMode scaleMode;
    public Vector2 referenceResolution;
    public CanvasScaler.ScreenMatchMode screenMatchMode;
    public float matchWidthOrHeight;
    //public float matchmode = CanvasScaler.

    void LateUpdate()
    {
        CanvasScaler canvasScaler = canvas.GetComponent<CanvasScaler>();
        float ratio;

        float fold = 5 / 4; //1.25
        float flip = 22 / 9; // 2.44
        float premium = 19.5f /9; // 2.17
        float normal = 16 / 9; // 1.8

        ratio = Screen.width / Screen.height;

        if(Mathf.Abs(ratio - fold) < .2 || Mathf.Abs((1/fold) - ratio) < .2)
        {
            canvasScaler.matchWidthOrHeight = 0;
        }
        if (Mathf.Abs(ratio - flip) < .2 || Mathf.Abs((1 / flip) - ratio) < .2)
        {
            canvasScaler.matchWidthOrHeight = 0.55f;
        }
        if (Mathf.Abs(ratio - premium) < .2 || Mathf.Abs((1 / premium) - ratio) < .2)
        {
            canvasScaler.matchWidthOrHeight = 1;
        }
        else
        {

        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
