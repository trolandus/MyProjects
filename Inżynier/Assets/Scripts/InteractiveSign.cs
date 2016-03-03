using UnityEngine;
using System.Collections;

public class InteractiveSign : MonoBehaviour
{
    public bool isActive = false;
    public Transform playerPos;
    public Transform currentPlayerTransform;
    public Material SilhouetteMaterial;

    private MeshRenderer myRenderer;

	// Use this for initialization
	void Start ()
	{
	    myRenderer = GetComponent<MeshRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
	    if (this.isActive)
	    {
	        ModifyMaterials(SilhouetteMaterial);
            Interract();
	    }
	    else
	    {
	        ModifyMaterials(myRenderer.materials[0]);
	    }
	}

    void ModifyMaterials(Material newMat)
    {
        var mats = myRenderer.materials;
        mats[1] = newMat;
        myRenderer.materials = mats;
    }

    public void Interract()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            GameState.Instance.currentState = States.SHOW_SIGN;
            Camera.main.GetComponent<CameraController2>().GetMyAnimator().SetBool("ShowSign", true);
            currentPlayerTransform.localPosition = playerPos.position;
            currentPlayerTransform.localRotation = playerPos.rotation;
            this.GetComponent<BoxCollider>().enabled = false;
        }

        if (GameState.Instance.currentState == States.SHOW_SIGN && Input.GetKeyDown(KeyCode.Escape))
        {
            GameState.Instance.currentState = States.GAMEPLAY;
            Camera.main.GetComponent<CameraController2>().GetMyAnimator().SetBool("ShowSign", false);
            this.GetComponent<BoxCollider>().enabled = true;
        }
    }
}
