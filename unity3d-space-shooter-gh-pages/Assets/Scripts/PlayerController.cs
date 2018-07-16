using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary {
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : Singleton<PlayerController> {

	public float speed;
	public float tilt;
	public Boundary boundary;

	public GameObject shot;
	public Transform shotSpawn;

	public float fireRate;
	private float nextFire;

    private Rigidbody rb;
    private AudioSource som;

    private Quaternion startRot;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        som = gameObject.GetComponent<AudioSource>();
        startRot = transform.rotation;
    }

	void Update() {
		if (Input.GetMouseButton(0) && Time.time > nextFire) {

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                Transform objectHit = hit.transform;
                Debug.Log(objectHit.gameObject.name);
                GameObject _shot = new GameObject();
                _shot.transform.position = Vector3.zero;
                _shot = Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
                _shot.transform.position = Vector3.MoveTowards(_shot.transform.position, objectHit.transform.position, 2);

                Vector3 targetDir = objectHit.transform.position - transform.position;
                Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, 10, 0.0f);
                transform.rotation = Quaternion.LookRotation(newDir);

                Vector3 targetDirShot = objectHit.transform.position - _shot.transform.position;
                Vector3 newDirShot = Vector3.RotateTowards(transform.forward, targetDir, 10, 0.0f);
                _shot.transform.rotation = Quaternion.LookRotation(newDir);

                nextFire = Time.time + fireRate;
                som.Play();
            }
		}
	}

	void FixedUpdate() {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
		rb.velocity = movement * speed;

		rb.position = new Vector3
		(
			Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
			0,
			Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
		);
	}
    
    public void RotateBack()
    {
        transform.rotation = startRot;
    }

}
