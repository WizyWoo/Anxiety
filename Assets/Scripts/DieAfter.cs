using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieAfter : MonoBehaviour
{

    public float Delay;

    private void Start() => Destroy(gameObject, Delay);

}
