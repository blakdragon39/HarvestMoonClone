using UnityEngine;

public enum FacingDirection {
    Down, Up, Left, Right
}

public static class FacingDirectionExtensions {
    public static Vector3 FacingTileVector(this FacingDirection dir) {
        switch (dir) {
            case FacingDirection.Left:
                return Vector3.left;
            case FacingDirection.Right:
                return Vector3.right;
            case FacingDirection.Down:
                return new Vector3(0f, -0.5f, 0);
            case FacingDirection.Up:
                return new Vector3(0f, 0.5f, 0);
        }
        
        return Vector3.zero;
    }
}