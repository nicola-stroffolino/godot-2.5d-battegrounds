using System;
using Godot;
using Godot.Collections;

public partial class Game : Node {
    public static Dictionary<Vector2, string> Directions { get; set; } = new() {
        { Vector2.Up, "u" },
        { Vector2.Down, "d" },
        { Vector2.Left, "l" },
        { Vector2.Right, "r" },
        { Vector2.Up + Vector2.Right, "ur" },
        { Vector2.Up + Vector2.Left, "ul" },
        { Vector2.Down + Vector2.Right, "dr" },
        { Vector2.Down + Vector2.Left, "dl" },
    };

    public static Vector2 DirectionFromIndex(int idx) {
		return idx switch {
            0 => Vector2.Right,
            1 => Vector2.Down + Vector2.Right,
            2 => Vector2.Down,
            3 => Vector2.Down + Vector2.Left,
            4 => Vector2.Left,
            5 => Vector2.Up + Vector2.Left,
            6 => Vector2.Up,
            7 => Vector2.Up + Vector2.Right,
            _ => throw new Exception(),
        };
	}
}
