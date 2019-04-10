using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {

    public bool IsSpecial = false;
    public bool Enabled = true;

    public Material NormalMaterial;
    public Material SpecialMaterial;

	// Use this for initialization
	void Start ()
    {
        SetMaterial();
        Enabled = true;
	}

    private void OnValidate()
    {
        SetMaterial();
    }

    void SetMaterial()
    {
        var targetMaterial = IsSpecial ? SpecialMaterial : NormalMaterial;

        var renderer = GetComponent<Renderer>();
        renderer.material = targetMaterial;
    }

    private void OnMouseDrag()
    {
        if (FindObjectOfType<GameController>().isPlaying == false) return;

        var cameraPosition = FindObjectOfType<Camera>().transform.position;
        // camera position minus position of the block makes vector which block shoud follow
        var direction = cameraPosition - transform.position;

        // normalize because of various camera position (we need the same force for all blocks)
        direction = direction.normalized;
        GetComponent<Rigidbody>().AddForce(direction * 35f);
    }
}
