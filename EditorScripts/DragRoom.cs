using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class DragRoom : MonoBehaviour {

    void MatchPointWhenDrag( )
    {
        if(Input.GetMouseButtonDown(0)) {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit) && hit.transform == transform) {
                Debug.Log("hit room");
            }
        }
    }

    public void OnMouseDrag( )
    {
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        Vector3 clickedPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = clickedPosition;

        Debug.Log("mousePosition x : " + mousePosition.x + ", " + "mousePosition y : " + mousePosition.y);
        Debug.Log("clickedPosition x : " + clickedPosition.x + ", " + "clickedPosition y : " + clickedPosition.y);
    }

    //void OnGUI( )
    //{
    //    Debug.Log("on gui");
    //    if(Input.GetButton("Fire1")) {
    //        Debug.Log("fire");
    //    }
    //}
}
