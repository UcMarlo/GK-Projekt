using System;
using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    Vector3 oldPosition;
    HexComponent[] hexes;
    private HexMap hexMap;
    float moveSpeed = 3.5f;
    //CAMERA SCROLL
    public float maxiumumCameraScroll = 15.0f;
    public float minimumCameraScroll = 2.0f; 
    Vector3 cameraTargetOffset;
    

    // Use this for initialization
    void Start() {
        oldPosition = this.transform.position;
        hexMap = GameObject.FindObjectOfType<HexMap>();
        updateMapPositions();
        UpdateWaterPosition();
    }

    // Update is called once per frame
    void Update() {
        updateMapPositions();
        handleKeyboardInput();
        handleMouseInput();
        UpdateWaterPosition();
    }

    private void updateMapPositions() {
        if(oldPosition != this.transform.position) {
            oldPosition = this.transform.position;
            if(hexes == null) {
                hexes = GameObject.FindObjectsOfType<HexComponent>();
            }

            foreach(HexComponent hex in hexes) {
                hex.UpdatePosition();
                if (hex.Hex.Unit != null) 
                    hex.Hex.Unit.UnitComponent.UpdatePosition();
            }
        }
    }

    private void handleKeyboardInput() {
        Vector3 translate = new Vector3(
            Input.GetAxis("Horizontal"),
            0,
            Input.GetAxis("Vertical"));

        this.transform.Translate( translate * moveSpeed * Time.deltaTime * (1 + this.transform.position.y * 0.5f), Space.World);
    }

    private void handleMouseInput()
    {
        float scrollDistance = Input.GetAxis("Mouse ScrollWheel");
        Vector3 hitPosition = MouseToGroundPlane(Input.mousePosition);
        Vector3 direction = hitPosition - Camera.main.transform.position;

        Vector3 position = Camera.main.transform.position;

        if (scrollDistance > 0 || position.y < (maxiumumCameraScroll - 0.1f))
        {
            cameraTargetOffset += direction * scrollDistance;
        }

        Vector3 lastCamPosition = Camera.main.transform.position;
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position,
            Camera.main.transform.position + cameraTargetOffset, Time.deltaTime * 5f);
            cameraTargetOffset -= Camera.main.transform.position - lastCamPosition;

        position = Camera.main.transform.position;

        if (position.y < minimumCameraScroll)
        {
            position.y = minimumCameraScroll;
        } else if (position.y > maxiumumCameraScroll)
        {
            position.y = maxiumumCameraScroll;
        }
        Camera.main.transform.position = position;

        Camera.main.transform.rotation = Quaternion.Euler(
            Mathf.Lerp(30,75, Camera.main.transform.position.y / maxiumumCameraScroll),
            Camera.main.transform.rotation.eulerAngles.y,
            Camera.main.transform.rotation.eulerAngles.z 
        );
    }

    private void UpdateWaterPosition()
    {
        Vector3 newWaterPosition = hexMap.GetHexGOAt(hexMap.MapColumns / 2, hexMap.MapRows / 2).transform.position;
        newWaterPosition.y = hexMap.WaterYPos;
        hexMap.WaterInstance.transform.position = newWaterPosition;
    }
    
    private Vector3 MouseToGroundPlane(Vector3 mousePosition)
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(mousePosition);
        if (mouseRay.direction.y >= 0)
        {
            return Vector3.zero;
        }
        float rayLenght = (mouseRay.origin.y / mouseRay.direction.y);
        return mouseRay.origin - (mouseRay.direction * rayLenght);
    }

    void ScrollInput()
    {
        float scrollDistanceInput = Input.GetAxis("Mouse ScrollWheel");
        
    }
}
