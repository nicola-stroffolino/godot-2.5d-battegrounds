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
}
