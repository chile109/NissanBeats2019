using UnityEngine;

public class InputController : MonoBehaviour
{
    [Range(5, 15)]
    public int MoveSpeed = 0;

    public float BoundaryDis = 4f;

    public Transform Target;

    void FixedUpdate()
    {
        if(GameManager.I.IsOver)
            return;
        
        float GetAxis = -Input.GetAxis("Mouse X");

        if (Target.position.x > BoundaryDis && GetAxis > 0)
            return;
        if (Target.position.x < -BoundaryDis && GetAxis < 0)
            return;

        var tranX = Mathf.Abs(GetAxis * MoveSpeed * Time.deltaTime + Target.position.x) > BoundaryDis ? 0 : GetAxis * MoveSpeed * Time.deltaTime;
        Target.Translate(tranX, 0, 0);
    }
}