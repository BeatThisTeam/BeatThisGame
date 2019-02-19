using UnityEngine;

public class CanvasRotation : MonoBehaviour {

    float centerOffset;

    private void Start() {

        centerOffset = transform.position.z;
    }

    public void RotateCanvas (int camPos) {

        switch (camPos) {
            case 0:
                gameObject.transform.localPosition = new Vector3(0, gameObject.transform.position.y, centerOffset);
                gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
                break;

            case 1:
                gameObject.transform.localPosition = new Vector3(-centerOffset, gameObject.transform.position.y, 0);
                gameObject.transform.eulerAngles = new Vector3(0, -90, 0);
                break;

            case 2:
                gameObject.transform.localPosition = new Vector3(0, gameObject.transform.position.y, -centerOffset);
                gameObject.transform.eulerAngles = new Vector3(0, -180, 0);
                break;

            case 3:
                gameObject.transform.localPosition = new Vector3(centerOffset, gameObject.transform.position.y, 0);
                gameObject.transform.eulerAngles = new Vector3(0, -270, 0);
                break;
        }
	}
}
