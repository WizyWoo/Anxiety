using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDrawing : MonoBehaviour
{

    public List<Vector2> LevelPosPoints;
    public EdgeCollider2D LevelCollider;
    public LineRenderer LevelBorder;
    public string LevelParentName;
    private int vertice;
    private int border;
    private GameObject levelParent;

    private void Start()
    {

        LevelPosPoints = new List<Vector2>();
        vertice = 0;
        border = 0;

        levelParent = new GameObject(LevelParentName);

    }

    private void FixedUpdate()
    {

        if(Input.GetMouseButton(0))
        {

            Vector2 tempVector = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            LevelPosPoints.Add(tempVector);
            LevelBorder.positionCount = vertice + 1;
            LevelBorder.SetPosition(vertice, tempVector);
            LevelCollider.SetPoints(LevelPosPoints);

            vertice++;

        }

    }

    private void Update()
    {

        if(Input.GetKeyDown(KeyCode.G))
        {

            LevelCollider.SetPoints(LevelPosPoints);
            vertice = 0;

            GameObject tempLevelBorder = new GameObject("LevelBorder " + border);
            LineRenderer tempLineRenderer = tempLevelBorder.AddComponent<LineRenderer>();
            EdgeCollider2D tempEdgeCollider = tempLevelBorder.AddComponent<EdgeCollider2D>();

            tempLineRenderer.positionCount = LevelPosPoints.Count;
            tempLineRenderer.material = LevelBorder.material;
            tempLineRenderer.colorGradient = LevelBorder.colorGradient;
            tempLineRenderer.widthCurve = LevelBorder.widthCurve;

            Vector3[] tempV3Positions = new Vector3[LevelPosPoints.Count];
            for (int i = 0; i < LevelPosPoints.Count; i++)
            {
                
                tempV3Positions[i] = LevelPosPoints[i];

            }
            tempLineRenderer.SetPositions(tempV3Positions);

            tempEdgeCollider.SetPoints(LevelPosPoints);

            border++;

            LevelPosPoints.Clear();
            LevelCollider.SetPoints(new List<Vector2>());
            LevelBorder.SetPositions(new Vector3[0]);

            tempLevelBorder.transform.SetParent(levelParent.transform);

        }
        else if(Input.GetKeyDown(KeyCode.Backspace))
        {

            vertice = 0;
            LevelPosPoints.Clear();
            LevelCollider.SetPoints(new List<Vector2>());
            LevelBorder.SetPositions(new Vector3[0]);

        }

    }

}
