using UnityEngine;
using System.Collections;

//Script Objective: Read a page from the book

public class LogBook : MonoBehaviour 
{
	public Transform page;

	//Main Camera Variable
	public Transform pageNode;

	public void Interact()
	{
		Transform pageReveal = Instantiate (page, pageNode.position, pageNode.rotation) as Transform;
		pageReveal.transform.parent = pageNode;
	}
}
