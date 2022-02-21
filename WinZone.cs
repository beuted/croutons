using Godot;
using System;

public class WinZone : Area2D
{
  private PackedScene _splash;
  private Particles2D _confetti1;
  private Particles2D _confetti2;
  private Particles2D _confetti3;
  private Particles2D _confetti4;
  private Label _restartLabel;

  public override void _Ready()
  {
    _splash = ResourceLoader.Load<PackedScene>("res://Splash.tscn");
    _confetti1 = GetTree().CurrentScene.GetNode<Particles2D>("confettis/Confetti");
    _confetti2 = GetTree().CurrentScene.GetNode<Particles2D>("confettis/Confetti2");
    _confetti3 = GetTree().CurrentScene.GetNode<Particles2D>("confettis/Confetti3");
    _confetti4 = GetTree().CurrentScene.GetNode<Particles2D>("confettis/Confetti4");
    _restartLabel = GetTree().CurrentScene.GetNode<Label>("CanvasLayer/Label");

  }

  public void OnBodyEntered(Node body)
  {
    if (body is Crouton bodyCrouton)
    {
      GD.Print("Win");

      var splash = _splash.Instance<Splash>();
      splash.Position = bodyCrouton.Position * 0.2f + new Vector2(-50, -200);
      GD.Print(bodyCrouton.Position);
      splash.Scale = Vector2.One * 20;
      splash.ZIndex = 20;
      AddChild(splash);
      //GetTree().ReloadCurrentScene();
      _confetti1.Emitting = true;
      _confetti2.Emitting = true;
      _confetti3.Emitting = true;
      _confetti4.Emitting = true;

      _restartLabel.Visible = true;

      body.QueueFree();
    }
  }
}
