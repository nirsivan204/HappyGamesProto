using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugMGR : MonoBehaviour
{
    [SerializeField] FlexibleColorPicker colorPicker;
    [SerializeField] SkinnedMeshRenderer[] customers;
    [SerializeField] Light[] lights;
    Vector3[] lightsOriginalAngle;

    private void Start()
    {
        lightsOriginalAngle = new Vector3[lights.Length];
        for (int i =0; i < lights.Length; i++)
        {
            lightsOriginalAngle[i] = lights[i].transform.eulerAngles;
        }
    }

    private void Update()
    {
        foreach(SkinnedMeshRenderer customer in customers)
        {
            customer.material.color = colorPicker.color;
        }
    }

    public void dayNight(float value)
    {
        for (int i = 0; i < lights.Length; i++)
        {
            lights[i].transform.localEulerAngles = lightsOriginalAngle[i] + Vector3.right * value;
        }
    }
}
