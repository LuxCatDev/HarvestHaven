using Godot;
using System;

namespace Game.Entities.Objects;

public enum ObjectCategory
{
	Object,
	Natural,
}

[GlobalClass]
public partial class GameObject : Resource
{
	[Export] public string Name;
	[Export] public PackedScene Scene;
	[Export] public ObjectCategory Category;
	
	public GameObject() : this("", null, ObjectCategory.Object) { }

	public GameObject(string name, PackedScene scene, ObjectCategory category)
	{
		Name = name;
		Scene = scene;
		Category = category;
	}
}
