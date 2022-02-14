using Godot;
using System;

public class Crouton : RigidBody2D
{
  private Vector2 _startPosition;
  private float _startRotation;

  // Called when the node enters the scene tree for the first time.
  public override void _Ready()
  {
    _startPosition = Position;
    _startRotation = Rotation;
  }

  public void Reset()
  {
    Position = _startPosition;
    Rotation = _startRotation;
  }
}
