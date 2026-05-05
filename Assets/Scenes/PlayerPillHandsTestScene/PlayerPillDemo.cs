using UnityEngine;

public class PlayerPillDemo : MonoBehaviour
{
    [SerializeField] private Transform rightArm;
    [SerializeField] private Transform leftArm;

    [SerializeField] private Transform rightArmTarget;
    [SerializeField] private Transform leftArmTarget;

    [SerializeField] private float cylinderRadius = 0.5f;
    [SerializeField] private float shoulderHeight = 0.2f;

    [SerializeField] private float rotationStartOffset = 60f;

    private void Update()
    {
        UpdateShoulder(rightArm, rightArmTarget, true);
        UpdateShoulder(leftArm, leftArmTarget, false);
    }

    private void UpdateShoulder(Transform armRoot, Transform target, bool isRight)
    {
        var center = transform.position;
        var toTarget = target.position - center;
        toTarget.y = 0f;

        if (toTarget.sqrMagnitude < 0.0001f)
            return;

        toTarget.Normalize();

        var localDir = transform.InverseTransformDirection(toTarget);
        var targetAngle = Mathf.Atan2(localDir.x, localDir.z) * Mathf.Rad2Deg;

        var baseAngle = isRight ? 90f : -90f;
        var angleDelta = Mathf.DeltaAngle(baseAngle, targetAngle);
        var inwardDelta = isRight ? -angleDelta : angleDelta;
        var slideFactor = 0f;

        if (inwardDelta > rotationStartOffset)
        {
            var range = 90f - rotationStartOffset;
            slideFactor = Mathf.Clamp01(
                (inwardDelta - rotationStartOffset) / range
            );
        }

        var finalAngle = Mathf.Clamp(Mathf.LerpAngle(baseAngle, targetAngle, slideFactor), -140, 140);
        var localShoulderDir = Quaternion.Euler(0f, finalAngle, 0f) * Vector3.forward;
        var worldDir = transform.TransformDirection(localShoulderDir);

        var desiredPos =
            center +
            worldDir * cylinderRadius +
            Vector3.up * shoulderHeight;

        armRoot.position = Vector3.Lerp(
            armRoot.position,
            desiredPos,
            Time.deltaTime * 15f
        );

        armRoot.LookAt(target);
    }
}
