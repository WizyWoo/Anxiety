using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowStalker : MonoBehaviour
{

    public float Current, T;
    public Transform PlayerTransform;
    private Vector2 ViewportCorner;

    private void Update()
    {

        ViewportCorner = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Current = Mathf.Lerp(0, 1, T);

        transform.position = Vector3.Lerp(ViewportCorner, PlayerTransform.position, Current);

    }

}