using Godot;
using System;

public class WinZone : Area2D
{

  public override void _Ready()
  {
  }

  public void OnBodyEntered(Node body)
  {
    if (body is Crouton bodyMob)
    {
      GD.Print("Win");
      GetTree().ReloadCurrentScene();
    }
  }
}
