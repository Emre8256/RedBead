using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LinesDrawer : MonoBehaviour
{
    public GameObject linePrefab;
    public LayerMask cantDrawOverLayer;
    int cantDrawOverLayerIndex;
    [SerializeField]
    private GameObject leftstaron,midstaron,rightstaron;
    public static bool puskurmebaslasinMi = false;

    public Slider slider;
    
    

    [Space(30f)]
    public Gradient lineColor;
    public float linePointsMinDistance;
    public float lineWidth;
    

    Line currentLine;

    Camera cam;
    Vector3 previousMousePosition;

    void Start()
    {
        cam= Camera.main;
        cantDrawOverLayerIndex = LayerMask.NameToLayer("CantDrawOver");
        previousMousePosition = Input.mousePosition; // Baþlangýçta fare pozisyonunu kaydet
    }

    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            BeginDraw();      
        if (currentLine != null)
            Draw();        
        if (Input.GetMouseButtonUp(0))
            EndDraw();

        Vector3 currentMousePosition = Input.mousePosition;
        if (currentLine != null && currentMousePosition != previousMousePosition)
            slider.value -= 0.5f;

        // Fare pozisyonunu güncelle
        previousMousePosition = currentMousePosition;

        starControl();

    }

    void BeginDraw()
    {
        
        currentLine= Instantiate(linePrefab,this.transform).GetComponent<Line>();

        currentLine.UsePhysics(false);
        currentLine.SetLineColor(lineColor);
        currentLine.SetPointsMinDistance(linePointsMinDistance);
        currentLine.SetLineWidth(lineWidth);

    }

    void Draw()
    {
        Vector2 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition)-transform.position; 
        RaycastHit2D hit = Physics2D.CircleCast(mousePosition, lineWidth / 3f, Vector2.zero, 1f, cantDrawOverLayer);

        if (hit)
            EndDraw();
        else
        {
            currentLine.AddPoint(mousePosition);
        } 
    }

    void EndDraw()
    {
        
        if(currentLine != null)
        {
            if(currentLine.pointsCount <2 )
            {
                Destroy(currentLine.gameObject);
            }
            else
            {
                currentLine.gameObject.layer = cantDrawOverLayerIndex;
                currentLine.UsePhysics(true);
                currentLine = null;
                puskurmebaslasinMi = true;
            }
        }
    }

    void starControl()
    {
        if (slider.value < 200)
            rightstaron.SetActive(false);
        if(slider.value <100)
            midstaron.SetActive(false);      
    }
}
