using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DentedPixel;
public class StartScence : MonoBehaviour
{
    public GameObject bar;
    public int time;

    void Start()
    {
          AnimateBar();
    }
    void Update()
    {
        
    }

    public void AnimateBar()
    {
        LeanTween.scaleX(bar, 1, time);
    }
}