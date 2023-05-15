using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretPointer
{
    public Transform gunPivot;



public void PointGun(Vector3 _targetPos, float gunWeight, float _pivotLimit, float _initialRotation)
{
    Vector3 direction = (_targetPos - gunPivot.position).normalized;
    float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

    // Adjust the target angle based on the initial rotation
    float adjustedTargetAngle = targetAngle - _initialRotation;

    // Normalize the adjusted target angle to be within the range of -180 to 180 degrees
    if (adjustedTargetAngle > 180f)
        adjustedTargetAngle -= 360f;
    else if (adjustedTargetAngle < -180f)
        adjustedTargetAngle += 360f;

    // Determine the clamped angle within the turret's firing arc
    float clampedAngle = Mathf.Clamp(adjustedTargetAngle, -_pivotLimit, _pivotLimit);

    // Calculate the final target rotation
    float finalRotation = clampedAngle + _initialRotation;
    Quaternion targetRotation = Quaternion.Euler(0, 0, finalRotation);

    // Apply the rotation using Lerp
    gunPivot.rotation = Quaternion.Lerp(gunPivot.rotation, targetRotation, gunWeight * Time.deltaTime);
}


}
