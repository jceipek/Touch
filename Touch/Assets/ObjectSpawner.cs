using UnityEngine;
using System.Collections;

public class ObjectSpawner : MonoBehaviour {

    [SerializeField]
    GameObject _toSpawn;
    [SerializeField]
    SteamVR_TrackedObject[] _trackedObjects;
    [SerializeField]
    float _separation = 0.4f;

	void Update () {
        for (int i = _trackedObjects.Length - 1; i >= 0; i--) {
            var device = SteamVR_Controller.Input((int)_trackedObjects[i].index);
            var pos = _trackedObjects[i].GetComponent<PickUpController>()._pickupTransform.position;
            if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger) && (transform.position - pos).magnitude < _separation) {
                Instantiate(_toSpawn, pos, Quaternion.identity);
            }
        }
	}

    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, _separation);
    }
}
