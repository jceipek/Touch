using UnityEngine;
using System.Collections;

public class PickUpController : MonoBehaviour {

    [SerializeField] SteamVR_TrackedObject _trackedObject;
    SteamVR_Controller.Device _device;
    [SerializeField]
    float _pickupRadius;
    bool _isTriggerPressed;
    public Transform _pickupTransform;

    void Enable()
    {
        //_trackedObject = GetComponent<SteamVR_TrackedObject>();
       
    }

	// Use this for initialization
	void Start () {
         
    }

    const int MAX_CARRY = 1;
    Collider[] _pickupResults = new Collider[MAX_CARRY];
    CanBePickedUp[] _carried = new CanBePickedUp[MAX_CARRY];

	// Update is called once per frame
	void Update () {
        _device = SteamVR_Controller.Input((int)_trackedObject.index);
        _isTriggerPressed = _device.GetPress(SteamVR_Controller.ButtonMask.Trigger);
        int overlapCount = Physics.OverlapSphereNonAlloc(_pickupTransform.position, _pickupRadius, _pickupResults);
        for (int i = overlapCount - 1; i >= 0 && _isTriggerPressed; i--)
        {
            var currOverlap = _pickupResults[i];
            var canBePickedUp = currOverlap.GetComponent<CanBePickedUp>();
            if (canBePickedUp != null)
            {
                _carried[i] = canBePickedUp;
                //Debug.Log("something picked up");
            }
            //Debug.Log("stuff");
        }

        for (int i = _carried.Length - 1; i >= 0 && _isTriggerPressed; i--)
        {
            if (_carried[i] != null)
            {
                _carried[i].transform.position = _pickupTransform.position;
                //Debug.Log("moving a thing");
            }
        }

        for (int i = _carried.Length - 1; i >= 0 && !_isTriggerPressed; i--)
        {
            _carried[i] = null;
            //Debug.Log("dropping a thing");
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(_pickupTransform.position, _pickupRadius);
    }
}
//SteamVR_Controller.Input(deviceIndex).GetPressDown(SteamVR_Controller.ButtonMask.Trigger)