using Normal.Realtime;
using System.Collections.Generic;
using UnityEngine;

public class UnownedPlayerInitializer : MonoBehaviour
{
    [SerializeField] private RealtimeView _realtimeView = null;
    [SerializeField] private Camera _camera = null;
    [SerializeField] private CharacterController _controller = null;
    [SerializeField] private List<GameObject> _disabledObjects = new List<GameObject>();

    private bool _isOwned = false;

    void Start()
    {
        if (_realtimeView == null) { return; }

        _isOwned = _realtimeView.isOwnedLocallyInHierarchy;

        if (_isOwned == false)
        {
            DisableUnownedComponents();
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

        foreach (var item in _disabledObjects)
        {
            item.SetActive(false);
        }
    }
}
