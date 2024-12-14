using Godot;
using System.Collections.Generic;

namespace Game.Entities.ObjectInventory;

public partial class ObjectInventory : Node2D
{
	public List<ObjectItem> Items;

	[Export] public Godot.Collections.Array<ObjectItem> InitialObjects;

	public override void _Ready()
	{
		Items = new List<ObjectItem>();
		Items.AddRange(InitialObjects);
	}
	
	public void AddItem(ObjectItem item)
	{
		Items.Add(item);
	}

	public void RemoveItemAt(int index)
	{
		Items.RemoveAt(index);
	}
}
