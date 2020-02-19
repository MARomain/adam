using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class EditShader : MonoBehaviour
{
    public Material outlineMaterial;

    public Slider depthSlider;
    public Slider normalSlider;
    public Slider colorSlider;
    public Slider lineThickness;

    public float depthDefaultValue;
    public float normalDefaultValue;
    public float colorDefaultValue;
    public float thicknessDefaultValue;
    // Start is called before the first frame update
    void Start()
    {
        outlineMaterial = GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        outlineMaterial.SetFloat("Outline Thickness",lineThickness.value);
        outlineMaterial.SetFloat("Depth Sensitivity",depthSlider.value);
        outlineMaterial.SetFloat("Normals Sensitivity",normalSlider.value);
        outlineMaterial.SetFloat("Color Sensitivity",colorSlider.value);


        if(Keyboard.current[Key.R].wasReleasedThisFrame)
        {
            outlineMaterial.SetFloat("OutlineThickness", lineThickness.value);
            outlineMaterial.SetFloat("DepthSensitivity", depthSlider.value);
            outlineMaterial.SetFloat("NormalsSensitivity", normalSlider.value);
            outlineMaterial.SetFloat("ColorSensitivity", colorSlider.value);
        }
    }
}
