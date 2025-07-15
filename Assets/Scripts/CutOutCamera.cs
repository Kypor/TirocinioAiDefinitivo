using UnityEngine;

public class CutOutCamera : MonoBehaviour
{
    [SerializeField]
    private Transform targetObject;

    [SerializeField]
    private LayerMask wallMask;

    private Camera mainCamera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        mainCamera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 cutOutPos = mainCamera.WorldToViewportPoint(targetObject.position);
        cutOutPos.y /= (Screen.width / Screen.height);

        Vector3 offset = targetObject.position - transform.position;
        RaycastHit[] hitObjects = Physics.RaycastAll(transform.position, offset, offset.magnitude, wallMask);

        for (int i = 0; i < hitObjects.Length; ++i)
        {
            Material[] materials = hitObjects[i].transform.GetComponent<Renderer>().materials;

            for (int m = 0; m < materials.Length; ++m)
            {
                materials[m].SetVector("_CutoutPos", cutOutPos);
                materials[m].SetFloat("_CutoutSize", 0.2f);
                materials[m].SetFloat("_FalloffSize", 0.05f);
            }
        }
    }
}
