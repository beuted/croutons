using Godot;
using System;

public class Main : Node2D
{
  // Called when the node enters the scene tree for the first time.
  public override void _Ready()
  {
    OS.WindowFullscreen = true;
  }

  // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _Process(float delta)
  {
    if (Input.IsActionJustPressed("Restart"))
    {
      GD.Print("restart");
      GetTree().ReloadCurrentScene();
    }

    if (Input.IsActionJustPressed("Exit"))
    {
      GetTree().Quit();
    }

    if (Input.IsActionJustPressed("ToggleFullScreen"))
    {
      OS.WindowFullscreen = !OS.WindowFullscreen;
    }
  }
}
