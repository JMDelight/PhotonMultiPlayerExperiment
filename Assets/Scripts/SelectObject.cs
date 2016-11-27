using UnityEngine;
using System.Collections;
using System;

public class SelectObject : MonoBehaviour {
    public GameObject selectedObject = null;
    public Launcher launcher;
	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                Debug.Log(hit.transform.gameObject.name);
                selectedObject = hit.transform.gameObject;
                launcher.Connect();
                DontDestroyOnLoad(selectedObject);
                selectedObject.AddComponent(Type.GetType("PhotonView"));
                selectedObject.GetPhotonView().
            }
        }
    }
}
