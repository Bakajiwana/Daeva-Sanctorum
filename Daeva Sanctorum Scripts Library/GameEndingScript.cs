using UnityEngine;
using System.Collections;

public class GameEndingScript : MonoBehaviour {

	private bool whiteOut = false;

	public Transform endTrans;
	public float fadeRate = 0.3f;

	// Use this for initialization
	void Start () {
		endTrans.gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
	


		if(whiteOut)
		{
			Color end = endTrans.renderer.material.color;
			end.a += Time.deltaTime * fadeRate; 
			endTrans.renderer.material.color = end;
			
			if(end.a >= 1f)
			{
				Application.LoadLevel (2);//TO THE CREDITS THE END YEEEEEEEEEEEEE
			}
		}
	}

	public void End()
	{
		endTrans.gameObject.SetActive (true);
		whiteOut = true;
	}
}
