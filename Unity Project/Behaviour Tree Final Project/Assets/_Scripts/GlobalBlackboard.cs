using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalBlackboard : MonoBehaviour
{
    Vector3 playerLastSighting;
    public Guardblackboard[] guardBlackboards;

    private void OnValidate()
    {
        guardBlackboards = FindObjectsOfType<Guardblackboard>();
    }
}
