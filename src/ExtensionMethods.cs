using System;
using System.Linq;
using Godot;

public static class ExtensionMethods {
    public static Vector2 Denormalized(this Vector2 source, Vector2 previousSource, float slice = Mathf.Pi / 3){
        // var previousAngle = Mod(Mathf.Atan2(previousSource.Y, previousSource.X), 2 * Mathf.Pi);
    	// var angle = Mod(Mathf.Atan2(source.Y, source.X), 2 * Mathf.Pi);

        // GD.Print("old: " + previousAngle + " destination: " + angle);

        // var angleDiff = previousAngle - angle;
        // GD.Print("angle diff: " + angleDiff);
        // float newAngle = previousAngle;
        // if (Mathf.Abs(angleDiff) > slice / 2) {
        //     if(angleDiff < 0) {
        //         float distance = angleDiff;
        //         while (true) {
        //             GD.Print("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
        //             newAngle += Mathf.Pi / 4;
        //             var tmpDiff = Math.Abs(newAngle - angle);
        //             if (tmpDiff > distance) {
        //                 newAngle -= Mathf.Pi / 4;
        //                 break;
        //             }
        //             distance = tmpDiff;
        //         }
        //     } else {
        //         float distance = angleDiff;
        //         while (true) {
        //             newAngle -= Mathf.Pi / 4;
        //             var tmpDiff = Math.Abs(newAngle - angle);
        //             if (tmpDiff > distance) {
        //                 newAngle += Mathf.Pi / 4;
        //                 break;
        //             }
        //             distance = tmpDiff;
        //         }
        //     }
        // }

        Vector2 closestDirection = Game.Directions.Keys.First(k => k == previousSource);
        float maxDotProduct = source.Dot(closestDirection.Normalized());

        foreach (Vector2 dir in Game.Directions.Keys) {
            float dotProduct = source.Dot(dir.Normalized());
            if (dotProduct > maxDotProduct) {
                maxDotProduct = dotProduct;
                closestDirection = dir;
            }
        }

        // GD.Print(newAngle);

        // return (int)(Mod(newAngle, 2 * Mathf.Pi) / (Mathf.Pi / 4)) switch {
        //     0 => Vector2.Right,
        //     1 => Vector2.Down + Vector2.Right,
        //     2 => Vector2.Down,
        //     3 => Vector2.Down + Vector2.Left,
        //     4 => Vector2.Left,
        //     5 => Vector2.Up + Vector2.Left,
        //     6 => Vector2.Up,
        //     7 => Vector2.Up + Vector2.Right,
        //     _ => throw new Exception(),
        // };

        return closestDirection;
    }

    public static float Mod(float x, float m) {
        return (x%m + m)%m;
    }
}