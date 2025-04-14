using System.Collections;  // 引入 System.Collections 命名空间，用于使用集合相关的类和接口
using System.Collections.Generic;  // 引入 System.Collections.Generic 命名空间，用于使用泛型集合类
using UnityEngine;  // 引入 UnityEngine 命名空间，Unity 游戏开发的核心命名空间
using UnityEngine.EventSystems;  // 引入 UnityEngine.EventSystems 命名空间，用于处理事件系统相关功能
using UnityEngine.UI;  // 引入 UnityEngine.UI 命名空间，用于使用 UI 相关的组件
using System.Linq;  // 引入 System.Linq 命名空间，用于使用 LINQ 查询功能
using Unity.VisualScripting;  // 引入 Unity.VisualScripting 命名空间，用于可视化脚本功能


public class Assemble : MonoBehaviour
{
    public pickupmethod pickupmethod1;  // 声明一个 public 的 pickupmethod 类型的变量
    public lingjiandate lingjiandate1;  // 声明一个 public 的 lingjiandate 类型的变量
    public GameObject dizuo, chelun, tulun1, tulun2;  // 声明 public 的 GameObject 类型的变量，分别表示底座、车轮、凸轮1和凸轮2
    public GameObject chaixieUI, chelunpart, tulunpart, dizuopart;  // 声明 public 的 GameObject 类型的变量，用于显示 UI
    public xiaochedate xiaochedate1;  // 声明一个 public 的 xiaochedate 类型的变量
    public PickupController pickupController;  // 声明一个 public 的 PickupController 类型的变量
    public List<GameObject> HideGameObjects = new List<GameObject>();  // 声明一个 public 的 GameObject 列表，用于存储隐藏的游戏对象
    public bool haveassmble = false;
    public CarController carController;
    void Start()
    {
        // 此方法在脚本实例被启用时调用，当前为空
    }

    void Update()
    {
        haveassmble = pickupmethod1.istulun && pickupmethod1.ischelun && pickupmethod1.isdizuo;
        carController.isRunning = haveassmble;
        //判断是否组装完成
        // 此方法在每一帧调用，当前为空
    }

    /// <summary>
    /// 组装指定的汽车部件
    /// </summary>
    /// <param name="car">要组装的汽车部件的游戏对象</param>
    public void assemble(GameObject car)
    {
        car.transform.parent = null;  // 将汽车部件的父对象设置为 null
        car.transform.position = pickupmethod1.spawnPosition;  // 设置汽车部件的位置为生成位置
        car.transform.rotation = pickupmethod1.spawnRotation;  // 设置汽车部件的旋转为生成旋转

        switch (car.name)
        {
            case "金属凸轮":
                if (!pickupmethod1.istulun)  // 如果凸轮未安装
                {
                    tulun1.SetActive(true);  // 激活凸轮1
                    tulun2.SetActive(true);  // 激活凸轮2
                    tulunpart.SetActive(true);  // 激活凸轮部分的 UI
                    xiaochedate1.totalweight += lingjiandate1.C[3].weight;  // 增加总重量
                    xiaochedate1.totalwending += lingjiandate1.C[3].wending;  // 增加总稳定性
                    pickupmethod1.istulun = true;  // 标记凸轮已安装
                    car.SetActive(false);  // 隐藏当前汽车部件
                    pickupController.heldObject = null;  // 清空拾取控制器当前持有对象
                }
                break;
            case "塑料凸轮":
                if (!pickupmethod1.istulun)  // 如果凸轮未安装
                {
                    tulun1.SetActive(true);  // 激活凸轮1
                    tulun2.SetActive(true);  // 激活凸轮2
                    tulunpart.SetActive(true);  // 激活凸轮部分的 UI
                    xiaochedate1.totalweight += lingjiandate1.C[2].weight;  // 增加总重量
                    xiaochedate1.totalwending += lingjiandate1.C[2].wending;  // 增加总稳定性
                    pickupController.heldObject = null;  // 清空拾取控制器当前持有对象
                    pickupmethod1.istulun = true;  // 标记凸轮已安装
                    car.SetActive(false);  // 隐藏当前汽车部件
                }
                break;
            case "金属车轮":
                if (!pickupmethod1.ischelun)  // 如果车轮未安装
                {
                    car.transform.rotation = pickupmethod1.spawnRotation;  // 设置车轮的旋转为生成旋转
                    chelun.SetActive(true);  // 激活车轮
                    pickupmethod1.ischelun = true;  // 标记车轮已安装
                    chelunpart.SetActive(true);  // 激活车轮部分的 UI
                    xiaochedate1.totalweight += lingjiandate1.C[1].weight;  // 增加总重量
                    xiaochedate1.totalwending += lingjiandate1.C[1].wending;  // 增加总稳定性
                    pickupController.heldObject = null;  // 清空拾取控制器当前持有对象
                    car.SetActive(false);  // 隐藏当前汽车部件
                }
                break;
            case "塑料车轮":
                if (!pickupmethod1.ischelun)  // 如果车轮未安装
                {
                    chelun.SetActive(true);  // 激活车轮
                    pickupmethod1.ischelun = true;  // 标记车轮已安装
                    chelunpart.SetActive(true);  // 激活车轮部分的 UI
                    xiaochedate1.totalweight += lingjiandate1.C[0].weight;  // 增加总重量
                    xiaochedate1.totalwending += lingjiandate1.C[0].wending;  // 增加总稳定性
                    pickupController.heldObject = null;  // 清空拾取控制器当前持有对象
                    car.SetActive(false);  // 隐藏当前汽车部件
                }
                break;
            case "金属底座":
                if (!pickupmethod1.isdizuo)  // 如果底座未安装
                {
                    dizuo.SetActive(true);  // 激活底座
                    pickupmethod1.isdizuo = true;  // 标记底座已安装
                    dizuopart.SetActive(true);  // 激活底座部分的 UI
                    xiaochedate1.totalweight += lingjiandate1.C[5].weight;  // 增加总重量
                    xiaochedate1.totalwending += lingjiandate1.C[5].wending;  // 增加总稳定性
                    pickupController.heldObject = null;  // 清空拾取控制器当前持有对象
                    car.SetActive(false);  // 隐藏当前汽车部件
                }
                break;
            case "塑料底座":
                if (!pickupmethod1.isdizuo)  // 如果底座未安装
                {
                    dizuo.SetActive(true);  // 激活底座
                    pickupmethod1.isdizuo = true;  // 标记底座已安装
                    dizuopart.SetActive(true);  // 激活底座部分的 UI
                    xiaochedate1.totalweight += lingjiandate1.C[4].weight;  // 增加总重量
                    xiaochedate1.totalwending += lingjiandate1.C[4].wending;  // 增加总稳定性
                    pickupController.heldObject = null;  // 清空拾取控制器当前持有对象
                    car.SetActive(false);  // 隐藏当前汽车部件
                }
                break;
        }
        pickupController.heldObject = null;  // 清空拾取控制器当前持有对象
    }

    /// <summary>
    /// 拆卸底座
    /// </summary>
    public void Disassemblydizuo()
    {
        if (pickupmethod1.isdizuo)  // 如果底座已安装
        {
            GameObject foundObject = HideGameObjects.FirstOrDefault(obj => obj.name.Contains("底座"));  // 查找包含 "dizuo" 的游戏对象
            foundObject.SetActive(true);  // 激活找到的游戏对象
            //lingjiandate lingjiandate = foundObject.GetComponent<lingjiandate>();  // 获取找到的游戏对象的 lingjiandate 组件
            int i = 0;
            for (i = 0; i < 6; i++)  // 遍历查找匹配的部件信息
            {
                if (foundObject.name == lingjiandate1.C[i].gameobjectname)
                {
                    xiaochedate1.totalweight -= lingjiandate1.C[i].weight;  // 减少总重量
                    xiaochedate1.totalwending -= lingjiandate1.C[i].wending;  // 减少总稳定性
                    break;
                }
            }
            dizuo.SetActive(false);  // 隐藏底座
            HideGameObjects.Remove(foundObject);  // 从隐藏对象列表中移除找到的对象
        }
        else
        {
            // 底座未安装，不做处理
        }
        pickupmethod1.isdizuo = false;  // 标记底座未安装
    }

    /// <summary>
    /// 拆卸车轮
    /// </summary>
    public void Disassemblychelun()
    {
        if (pickupmethod1.ischelun)  // 如果车轮已安装
        {
            GameObject foundObject = HideGameObjects.FirstOrDefault(obj => obj.name.Contains("车轮"));  // 查找包含 "chelun" 的游戏对象
            //lingjiandate lingjiandate = foundObject.GetComponent<lingjiandate>();  // 获取找到的游戏对象的 lingjiandate 组件
            foundObject.SetActive(true);  // 激活找到的游戏对象
            int i = 0;
            for (i = 0; i < 6; i++)  // 遍历查找匹配的部件信息
            {
                if (foundObject.name == lingjiandate1.C[i].gameobjectname)
                {
                    xiaochedate1.totalweight -= lingjiandate1.C[i].weight;  // 减少总重量
                    xiaochedate1.totalwending -= lingjiandate1.C[i].wending;  // 减少总稳定性
                    break;
                }
            }
            chelun.SetActive(false);  // 隐藏车轮
            HideGameObjects.Remove(foundObject);  // 从隐藏对象列表中移除找到的对象
        }
        else
        {
            // 车轮未安装，不做处理
        }
        pickupmethod1.ischelun = false;  // 标记车轮未安装
    }
    public void Disassemblytulun()
    {
        if (pickupmethod1.istulun)  // 如果凸轮已安装
        {
            GameObject foundObject = HideGameObjects.FirstOrDefault(obj => obj.name.Contains("凸轮"));  // 查找包含 "tulun" 的游戏对象
            if (foundObject != null)
            {
                foundObject.SetActive(true);  // 激活找到的游戏对象
            }
            else
                Debug.LogError("未找到匹配的对象");  // 未找到匹配对象，输出错误信息
           // lingjiandate lingjiandate = foundObject.GetComponent<lingjiandate>();  // 获取找到的游戏对象的 lingjiandate 组件
            int i = 0;
            for (i = 0; i < 6; i++)  // 遍历查找匹配的部件信息
            {
                if (foundObject.name == lingjiandate1.C[i].gameobjectname)
                {
                    xiaochedate1.totalweight -= lingjiandate1.C[i].weight;  // 减少总重量
                    xiaochedate1.totalwending -= lingjiandate1.C[i].wending;  // 减少总稳定性
                    break;
                }
            }
            tulun1.SetActive(false);  // 隐藏凸轮1
            tulun2.SetActive(false);  // 隐藏凸轮2
            HideGameObjects.Remove(foundObject);  // 从隐藏对象列表中移除找到的对象
        }
        else
        {
            // 凸轮未安装，不做处理
        }
        pickupmethod1.istulun = false;  // 标记凸轮未安装
    }
}
