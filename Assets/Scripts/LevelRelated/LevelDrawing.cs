using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDrawing : MonoBehaviour
{

    public GameObject currentLineOrigin;
    public GameObject regLine;
    private GameObject chosenLineType;
    public List<Vector2> points;
    public List<Vector3> pointsV3;
    public Vector2[] pointVectors;
    public Vector3[] pointVector3s;

    void Start()
    {

        chosenLineType = regLine;

    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {

            points.Clear();
            pointsV3.Clear();

            currentLineOrigin = Instantiate(chosenLineType, Vector3.zero, Quaternion.identity);

        }

        EdgeCollider2D eC = currentLineOrigin.GetComponent<EdgeCollider2D>();
        LineRenderer eLR = currentLineOrigin.GetComponent<LineRenderer>();

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {

            points.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            pointsV3.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10));

        }
        else
        {

            pointVectors = points.ToArray();
            pointVector3s = pointsV3.ToArray();

            eC.points = pointVectors;
            eLR.positionCount = pointVector3s.Length;
            eLR.SetPositions(pointVector3s);

        }

    }
}
