using Normal.Realtime;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class PlayerInitializer : MonoBehaviour
{
    [SerializeField] private RealtimeView _realtimeView = null;
    [SerializeField] private Camera _camera = null;
    [SerializeField] private CharacterController _controller = null;
    [SerializeField] private AudioListener _listener = null;
    [SerializeField] private Transform _offset = null;
    [SerializeField] private List<TrackedPoseDriver> _poseDrivers = new List<TrackedPoseDriver>();
    [SerializeField] private List<GameObject> _disabledObjects = new List<GameObject>();

    private bool _isOwned = false;

    void Start()
    {
        if (_realtimeView == null) { return; }

        _isOwned = _realtimeView.isOwnedLocallyInHierarchy;

        if (_isOwned == false)
        {
            if (_offset != null)
            {
                _offset.localPosition = Vector3.zero;
            }
            DisableUnownedComponents();
        }
        else
        {
            _realtimeView.RequestOwnershipOfSelfAndChildren();
        }
    }

    private void DisableUnownedComponents()
    {
        if (_camera != null)
        {
            _camera.enabled = false;
        }

        if (_controller != null)
        {
            _controller.enabled = false;
        }

        if (_listener != null)
        {
            _listener.enabled = false;
        }

        foreach (var item in _poseDrivers)
        {
            item.enabled = false;
        }

        foreach (var item in _disabledObjects)
        {
            item.SetActive(false);
        }
    }
}
