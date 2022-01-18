using Godot;
using System;

public class HUD : Node
{
	private string IconPath = "CanvasLayer/Control/VBoxContainer/Pizza";
	public Player Player { get; set; }
	public TextureRect Pizza { get; set; }

	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.Print("HUD Ready");
		// get_tree().get_nodes_in_group("my_group")
		var grp = GetTree().GetNodesInGroup("Player");
		GD.Print("grp =", grp);
		var player = (Player)GetTree().GetNodesInGroup("Player")[0];
		if (player != null)
		{
			Player = player;
		}
		else
		{
			GD.PrintErr("Cannot find Player");
		}

		//var pizza = (TextureRect)GetTree().GetNodesInGroup("ItemIcons")[0];
		var pizza = GetNode<TextureRect>(new NodePath(IconPath));
		if (pizza != null)
		{
			//Pizza = (TextureRect)GetTree().GetNodesInGroup("ItemIcons")[0];
			Pizza = pizza;
		}
		else
		{
			GD.PrintErr("Cannot find Pizza Texture");
		}
	}

	//  // Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		Pizza.Visible = this.Player.Inventory.HasItem("pizza");
	}
}
