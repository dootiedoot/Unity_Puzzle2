using UnityEngine;
using System.Collections;
using PicaVoxel;

public class IslandsClickBoom : MonoBehaviour
{
    public Exploder Exploder;

	void Start () {
	    
	}
	
	void Update () {
        // Left click
	    if (Input.GetMouseButtonDown(0))
	    {
            // Cast a ray from the camera position outward
	        Ray r = new Ray(GetComponent<Camera>().transform.position,GetComponent<Camera>().transform.forward);
            Debug.DrawRay(r.origin, r.direction*100f, Color.red, 10f);
	        bool exploded = false;
            // Test points along the ray until a voxel is found
            for (float d = 0; d < 50f; d += 0.05f)
            {
                // Loop through all the Volumes in the scene
	            foreach (GameObject o in GameObject.FindGameObjectsWithTag("PicaVoxelVolume"))
	            {
	                Volume pvo = o.GetComponent<Volume>();
	            
                    // Attempt to get a voxel at this point on the ray (returns null if no voxel)
	                Voxel? v = pvo.GetVoxelAtWorldPosition(r.GetPoint(d));
	                if (v.HasValue && v.Value.Active)
	                {
                        // We have a voxel, and it's active so move the exploder to this point and explode it
	                    Exploder.transform.position = r.GetPoint(d);
                        Exploder.Explode();
	                    exploded = true;
	                    break;
	                }
	            }
                if (exploded) break;
            }

	    }

        // Right click
	    if (Input.GetMouseButtonDown(1))
	    {
            // Cast a ray from the camera position outward
            Ray r = new Ray(GetComponent<Camera>().transform.position, GetComponent<Camera>().transform.forward);
            Debug.DrawRay(r.origin, r.direction * 100f, Color.red, 10f);
            bool found = false;
            // Test points along the ray until a voxel is found
            for (float d = 0; d < 50f; d += 0.05f)
            {
                // Loop through all the Volumes in the scene
                foreach (GameObject o in GameObject.FindGameObjectsWithTag("PicaVoxelVolume"))
                {
                    Volume pvo = o.GetComponent<Volume>();

                    // Attempt to get a voxel at this point on the ray (returns null if no voxel)
                    Voxel? v = pvo.GetVoxelAtWorldPosition(r.GetPoint(d));
                    if (v.HasValue && v.Value.Active)
                    {
                        // We have a voxel, and it's active, but we want to build a voxel so let's back up the ray a step
                        Vector3 buildPos = r.GetPoint(d - 0.05f);

                        // Check that there is a voxel here, and it is inactive
                        Voxel? v2 = pvo.GetVoxelAtWorldPosition(buildPos);
                        if (v2.HasValue && !v2.Value.Active)
                        {
                            // Set this voxel to active, with a random color!
                            pvo.SetVoxelAtWorldPosition(buildPos, new Voxel()
                            {
                                State = VoxelState.Active,
                                Color = new Color(Random.Range(0f,1f),Random.Range(0f,1f), Random.Range(0f,1f)),
                                Value = 128
                            });    
                        }

                        found = true;
                        break;
                    }
                }
                if (found) break;
            }
	    }
	}
}
