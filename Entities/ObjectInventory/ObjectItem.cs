using Godot;
using System;
using Game.Entities.Objects.Placeable;

namespace Game.Entities.ObjectInventory;

[GlobalClass]
public partial class ObjectItem : Resource
{
	[Export]
	public Placeable Object { get; set; }
}
