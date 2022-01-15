using Godot;

namespace Dialog2DayGame
{

    public static class GDUtils
    {
        private const string path = @"X:\Projects\Godot\dialog_2day_game\addons\dialogic\Other\DialogicClass.gd";
        private static Script _dialogic = GD.Load<Script>("res://addons/dialogic/Other/DialogicClass.gd");
        private const string DEFAULT_DIALOG_RESOURCE = "res://addons/dialogic/Nodes/DialogNode.tscn";

        public static Node GetDialog(string timeLine)
        {
            var retval = _dialogic.Call("Start", timeLine);
            return (Node2D)retval;
        }

        public static bool IsInteracting() => Input.IsActionJustPressed(InputAction.Interact);

        public static Vector2 GetTopDownMovementInput(float speed = 1f)
        {
            var velocity = Vector2.Zero;
            if (Input.IsActionPressed(InputAction.Right))
            {
                velocity.x += 1f;
            }
            if (Input.IsActionPressed(InputAction.Left))
            {
                velocity.x -= 1f;
            }
            if (Input.IsActionPressed(InputAction.Up))
            {
                velocity.y -= 1f;
            }
            if (Input.IsActionPressed(InputAction.Down))
            {
                velocity.y += 1f;
            }
            return velocity.Normalized() * speed;
        }
    

    }

    //public static class Node2dExtensions
    //{
    //    public void TransitionScene(this CommonScene currentScene, string newScenePath) 
    //    {
    //        var scene = ResourceLoader.Load(newScenePath);
    //        currentScene.GetChild(0).QueueFree();
    //        currentScene.AddChild(scene.in);
    //    }
    //}
}