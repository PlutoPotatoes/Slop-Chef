using UnityEngine;



public class Dispenser : MonoBehaviour
{
    [SerializeField] private GameObject dispensedObject;
    
    // Start is called before the first frame update
    public ObjectGrabbable CreateObject()
    {
        GameObject createdObject = Instantiate(dispensedObject, transform.position, transform.rotation);
        createdObject.TryGetComponent(out ObjectGrabbable objectGrabbable);
        return objectGrabbable;
    }
}
