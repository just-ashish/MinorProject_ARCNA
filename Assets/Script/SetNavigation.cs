using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class SetNavigation : MonoBehaviour
{
    [SerializeField]
    private TMP_Dropdown navigationTargetDropDown;
    [SerializeField]
    private List<Target> navigationTargetObjects = new List<Target>();

    private NavMeshPath path;
    private LineRenderer line;
    private Vector3 targetPostion = Vector3.zero;

    private bool lineToggle = false;

    private void Start()
    {
        path = new NavMeshPath();
        line = transform.GetComponent<LineRenderer>();
        line.enabled = lineToggle;
    }

    // Update is called once per frame
    private void Update()
    { 
        if (lineToggle && targetPostion != Vector3.zero){
            NavMesh.CalculatePath(transform.position, targetPostion, NavMesh.AllAreas, path);
            line.positionCount = path.corners.Length;
            line.SetPositions(path.corners);
        }
    }

    public void SetCurrentNavigationTarget(int selectedValue){
        targetPostion = Vector3.zero;
        string selectedText = navigationTargetDropDown.options[selectedValue].text;
        Target currentTarget = navigationTargetObjects.Find(x => x.Name.Equals(selectedText));
        if (currentTarget != null){
            targetPostion = currentTarget.PositionObject.transform.position;
        }
    }

    public void ToggleVisibility(){
        lineToggle =! lineToggle;
        line.enabled = lineToggle;
    }
}
