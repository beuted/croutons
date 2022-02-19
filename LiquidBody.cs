using Godot;
using System;
using System.Collections.Generic;

// https://youtu.be/RXIRkou021U?t=888
public class LiquidBody : Node2D
{
  [Export] private float K = 0.03f;
  [Export] private float D = 0.03f;
  [Export] private float Spread = 0.0002f;
  [Export] private int NbPasses = 8;
  [Export] private float DistanceBetweenSprings = 22;
  [Export] private int NbSprings = 8;
  [Export] private float Depth = 100;


  private float TargetHeight;
  private float Bottom;

  private List<LiquidSpring> Springs = new List<LiquidSpring>();
  private PackedScene _liquidSpring;
  private Polygon2D _liquidPolygon;

  // Called when the node enters the scene tree for the first time.
  public override void _Ready()
  {
    TargetHeight = GlobalPosition.y;
    Bottom = TargetHeight + Depth;

    // Init the springs with their positions
    var _liquidSpring = ResourceLoader.Load<PackedScene>("res://LiquidSpring.tscn");
    _liquidPolygon = GetNode<Polygon2D>("LiquidPolygon");

    var waterLength = DistanceBetweenSprings * NbSprings;
    for (var i = 0; i < NbSprings; i++)
    {
      var x = DistanceBetweenSprings * i;
      var spring = _liquidSpring.Instance<LiquidSpring>();
      spring.Initialize(x);

      AddChild(spring);
      Springs.Add(spring);

    }

    // Random splash to test
    Splash(2, 5);
  }

  public override void _PhysicsProcess(float delta)
  {
    foreach (var i in Springs)
    {
      (i as LiquidSpring).LiquidUpdate(K, D);
    }

    var LeftDeltas = new List<float>();
    var RightDeltas = new List<float>();

    for (var i = 0; i < Springs.Count; i++)
    {
      LeftDeltas.Add(0);
      RightDeltas.Add(0);
    }

    // Several passes
    for (var j = 0; j < NbPasses; j++)
    {
      // Each springs takes part of the velocity of its neighboors
      for (var i = 0; i < Springs.Count; i++)
      {
        if (i > 0)
        {
          LeftDeltas[i] = Spread * ((Springs[i] as LiquidSpring).Height - (Springs[i - 1] as LiquidSpring).Height);
          (Springs[i - 1] as LiquidSpring).Velocity += LeftDeltas[i];
        }
        if (i < Springs.Count - 1)
        {
          RightDeltas[i] = Spread * ((Springs[i] as LiquidSpring).Height - (Springs[i + 1] as LiquidSpring).Height);
          (Springs[i + 1] as LiquidSpring).Velocity += RightDeltas[i];
        }
      }
    }

    // Redraw Polygon
    DrawLiquidPolygon();
  }

  public void Splash(int index, float speed)
  {
    if (index >= 0 && index <= Springs.Count)
      (Springs[index] as LiquidSpring).Velocity += speed;
  }

  public void DrawLiquidPolygon()
  {
    var surfacePoints = new List<Vector2>();
    for (var i = 0; i < Springs.Count; i++)
    {
      surfacePoints.Add(Springs[i].Position);
    }
    var firstIndex = 0;
    var lastIndex = Springs.Count - 1;

    surfacePoints.Add(new Vector2(surfacePoints[lastIndex].x, Bottom));
    surfacePoints.Add(new Vector2(surfacePoints[firstIndex].x, Bottom));

    _liquidPolygon.Polygon = surfacePoints.ToArray();

  }
}
