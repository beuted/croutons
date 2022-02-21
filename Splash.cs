using Godot;
using System;

public class Splash : Particles2D
{
  private float _lifetime = 0.5f;
  private float _time = 0;
  private bool _start = false;

  public override void _Process(float delta)
  {

    if (!Emitting)
      _start = true;
    if (_start)
      _time += delta;

    if (_time >= _lifetime)
      QueueFree();
  }


  // Called when the node enters the scene tree for the first time.
  public override void _Ready()
  {
    Emitting = true;
  }
}
