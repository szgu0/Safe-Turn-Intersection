using PathCreation;
using UnityEngine;

public class error_path : MonoBehaviour
{
    public PathCreator path1;
    public PathCreator path2;
    public PathCreator path3;
    public PathCreator path4;
    public PathCreator path5;

    public PathCreator error_path1;
    public PathCreator error_path2;
    public PathCreator error_path3;
    public PathCreator error_path4;
    public PathCreator error_path5;

    public float RightSpeedKMHR;
    public float CarSpeedKMHR;

    private PathCreator currentPath; // 用于跟踪当前路径
    private e6 carFollowPath; // 用于存储 E6CarFollowPath 实例

    // Start is called before the first frame update
    void Start()
    {
        currentPath = path1; // 假设初始路径是 path1
        currentPath.gameObject.SetActive(true);

        // 获取 E6CarFollowPath 实例
        carFollowPath = GetComponent<e6>();
        if (carFollowPath == null)
        {
            Debug.LogError("e6 component not found on the GameObject.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (carFollowPath != null)
        {
            // 获取 E6CarFollowPath 中的变量值
            RightSpeedKMHR = carFollowPath.rightCarSpeedKMHr;
            CarSpeedKMHR = carFollowPath.CarSpeedKMHr;

            if (RightSpeedKMHR < CarSpeedKMHR)
            {
                SwitchToErrorPath();
            }
        }
    }

    private void SwitchToErrorPath()
    {
        if (currentPath == path1)
        {
            SetCurrentPath(path1, error_path1);
        }
        else if (currentPath == path2)
        {
            SetCurrentPath(path2, error_path2);
        }
        else if (currentPath == path3)
        {
            SetCurrentPath(path3, error_path3);
        }
        else if (currentPath == path4)
        {
            SetCurrentPath(path4, error_path4);
        }
        else if (currentPath == path5)
        {
            SetCurrentPath(path5, error_path5);
        }
    }

    private void SetCurrentPath(PathCreator oldPath, PathCreator newPath)
    {
        if (oldPath != null)
        {
            oldPath.gameObject.SetActive(false); // 关闭旧路径
        }
        currentPath = newPath;
        currentPath.gameObject.SetActive(true); // 激活新路径
    }
}
