using Godot;
using System;

//https://www.youtube.com/watch?v=RXIRkou021U
public class LiquidSpring : Node2D
{
  public float Velocity = 0;
  public float Force = 0;
  public float Height = 0;
  public float TargetHeight = 0;

  // Called when the node enters the scene tree for the first time.
  public override void _Ready()
  {
  }

  public override void _PhysicsProcess(float delta)
  {
  }

  public void LiquidUpdate(float sprintConstant, float dampling)
  {
    Height = Position.y;
    var x = Height - TargetHeight;
    var loss = -dampling * Velocity;

    // hooke's law:
    Force = -sprintConstant * x + loss;
    Velocity += Force;
    Position = new Vector2(Position.x, Position.y + Velocity);
  }

  public void Initialize(float x)
  {
    Height = Position.y;
    TargetHeight = Position.y;
    Velocity = 0;
    Position = new Vector2(x, Position.y);
  }
}
