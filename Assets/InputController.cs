using UnityEngine;

public class InputController : MonoBehaviour
{
    [Range(5, 15)]
    public int MoveSpeed = 0;

    public float BoundaryDis = 4f;

    public Transform Target;

    private Vector3 m_pos;

    private void Start()
    {
        m_pos = Target.position;
        //Screen.lockCursor = true;
    }

    void LateUpdate()
    {
        float GetAxis = Input.GetAxis("Mouse X");

        if (Target.position.x > BoundaryDis && GetAxis > 0)
            return;
        if (Target.position.x < -BoundaryDis && GetAxis < 0)
            return;

        Target.Translate(GetAxis * MoveSpeed * Time.deltaTime, 0, 0);
    }
}