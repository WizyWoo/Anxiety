using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowStalker : MonoBehaviour
{

    public float Current, T;
    public Transform PlayerTransform;
    public Transform[] Shadows;
    private Dictionary<Transform, Vector2> shadowDictionary;

    private void Start()
    {

        shadowDictionary = new Dictionary<Transform, Vector2>();

        foreach (Transform t in Shadows)
        {
            
            shadowDictionary.Add(t, t.position);
            
        }

    }

    private void Update()
    {

        Current = Mathf.Lerp(0, 1, T);

        foreach (Transform t in Shadows)
        {
            
            shadowDictionary.TryGetValue(t, out Vector2 tempVector);
            t.position = Vector2.Lerp(tempVector + new Vector2(transform.position.x, transform.position.y), transform.position, Current);
            t.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, Current + 0.1f);

        }

    }

}