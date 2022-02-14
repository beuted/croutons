using Godot;
using System;

public class fist : KinematicBody2D
{
  // Declare member variables here. Examples:
  private float _speed = 600;
  // private string b = "text";

  // Called when the node enters the scene tree for the first time.
  public override void _Ready()
  {

  }

  public override void _PhysicsProcess(float delta)
  {
    var mousePos = GetGlobalMousePosition();
    var direction = mousePos - Position;
    var directionDistance = direction.Length();

    //if (directionDistance > 0.1f)
    //{
    var normalizedDirection = direction.Normalized();
    MoveAndSlide(normalizedDirection * Mathf.Min(_speed, directionDistance * 100));
    //}
  }

}
