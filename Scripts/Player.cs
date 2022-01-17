using Godot;
using System.Collections.Generic;
using System.Linq;

public class Player : KinematicBody2D
{
    private Vector2 CurrentVelocity = Vector2.Zero;

    private AnimatedSprite animatedSprite;

    public List<Item> Inventory { get; set; } = new List<Item>();
    public bool IsHidden = false;

    [Export]
    public float MoveSpeed { get; set; } = 50f;

    [Export]
    public float MoveMultipler { get; set; } = 1.5f;

    public bool CanMove = true;
    public bool IsRunning = false;

    public void AddItem(string name, int amt)
    {
        for (int i = 0; i < amt; i++)
        {
            Inventory.Add(new Item(name));
        }
    }
    public void RemoveItem(string name)
    {
        Inventory.RemoveItemIfExists(name);
    }

    private void LockMovement()
    {
        GD.Print("Locking Movement");
        this.CanMove = false;
    }

    private void UnlockMovement()
    {
        GD.Print("Unlocking Movement");
        this.CanMove = true;
    }

    public void OnExaminablePlayerInteracting()
    {
        GD.Print("OnExaminablePlayerInteracting");
        this.LockMovement();
    }

    public void OnExaminablePlayerInteractingComplete()
    {
        GD.Print("OnExaminablePlayerInteractingComplete");
        this.UnlockMovement();
    }

    private void AnimationCheck(Vector2 velocity)
    {
        if (velocity.Length() > 0)
        {
            if (velocity.x < 0)
            {
                animatedSprite.Animation = AnimationConstants.Right;
                animatedSprite.Play();
            }
            if (velocity.x > 0)
            {
                animatedSprite.Animation = AnimationConstants.Left;
                animatedSprite.Play();
            }
            if (velocity.y < 0)
            {
                animatedSprite.Animation = AnimationConstants.Up;
                animatedSprite.Play();
            }
            if (velocity.y > 0)
            {
                animatedSprite.Animation = AnimationConstants.Down;
                animatedSprite.Play();
            }
        }
        else
        {
            animatedSprite.Stop();
        }
    }

    private Vector2 MoveCheck(bool isRunning = false) =>
        isRunning ?
            GDUtils.GetTopDownMovementInput(MoveSpeed * MoveMultipler) :
            GDUtils.GetTopDownMovementInput(MoveSpeed);

    private void PlayerInteractingCallback()
    {
    }

    public override async void _Ready()
    {
        GD.Print("Player Ready called");
        animatedSprite = GetNode<AnimatedSprite>("CollisionShape2D/AnimatedSprite");
    }

    public void DialogListenerCallback(string val)
    {
        GD.Print("DialogListenerCallback called with arg ", val);
    }

    public override void _PhysicsProcess(float delta)
    {
        IsRunning = Input.IsActionPressed(InputAction.Run);

        if (CanMove)
        {
            var currentVelocity = MoveCheck(IsRunning);
            AnimationCheck(currentVelocity);
            this.MoveAndSlide(MoveCheck(IsRunning));
        }
    }
}