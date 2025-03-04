using UnityEngine;
using System.Collections;

public class CharacterLocator : MonoBehaviour
{
    // 设置角色位置
    public void SetCharacterPosition(Transform target)
    {
        transform.position = target.position;
        transform.rotation = target.rotation;
    }

    // 平滑移动到目标位置
    public IEnumerator SmoothMove(Transform target, float speed)
    {
        while (transform.position != target.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, target.rotation, 360 * Time.deltaTime);
            yield return null;

        }
    }
}