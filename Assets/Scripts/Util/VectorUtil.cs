using UnityEngine;

public static class VectorUtil {

    public static Vector3 ToVector3(this Vector2 vec) {
        return new Vector3(vec.x, vec.y);
    }
}