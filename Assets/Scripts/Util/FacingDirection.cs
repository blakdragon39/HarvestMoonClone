using UnityEngine;

public enum FacingDirection {
    Down, Up, Left, Right
}

public static class FacingDirectionExtensions {
    public static Vector3 Vector(this FacingDirection dir) {
        switch (dir) {
            case FacingDirection.Down:
                return Vector3.down;
            case FacingDirection.Up:
                return Vector3.up;
            case FacingDirection.Left:
                return Vector3.left;
            case FacingDirection.Right:
                return Vector3.right;
        }
        
        return Vector3.zero;
    }
}