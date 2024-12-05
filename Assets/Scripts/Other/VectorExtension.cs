using System;
using UnityEngine;

public static class VectorExtension
{
    public static Vector3 Clamp(this Vector3 a, Vector3 clampVector) =>
        new Vector3(
            Math.Clamp(a.x, -clampVector.x, clampVector.x),
            Math.Clamp(a.y, -clampVector.y, clampVector.y),
            Math.Clamp(a.z, -clampVector.z, clampVector.z)
        );
}