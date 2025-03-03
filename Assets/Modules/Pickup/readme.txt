系统概述
这个系统实现了一个基于射线检测的物体拾取和放下功能。当玩家通过鼠标点击指向某个可拾取的物体时，系统会根据物体的属性判断是否允许拾取，并在满足条件时将物体拾取到指定的手持位置。同时，系统还提供了放下物体的功能，允许玩家将手持的物体放置到合适的表面上。此外，系统还支持动态生成与拾取相关的 UI 提示，以及在特定情况下隐藏或显示这些 UI 提示。

使用方法
将pickupmethod.cs 脚本和 PickupController.cs 脚本附加到摄像机上。设置 holdPosition 为手持物体的目标位置，putdownUI 为放下提示的 UI 预制体，handSphere（可选）为手上的圆球预制体。设置 pickupUIPrefab 为拾取 UI 的预制体，并将 PickupController 组件的引用拖拽到 pickupmethod 脚本的 pickupController 字段中。
在场景中创建或选择需要可拾取的物体，并为其添加 ObjectData 组件（或确保其名称与 ObjectDataManager 中的可拾取数据匹配）。设置物体的 canBePickedUp 属性为 true，以便允许其被拾取。
（可选）在场景中添加一个 Canvas，用于显示拾取和放下相关的 UI 提示。

模块文件名
Assets/Scripts/PickupController.cs
Assets/Scripts/pickupmethod.cs

脚本功能介绍
1. PickupController.cs
功能：作为系统的主控制器，负责管理物体的拾取和放下逻辑。
核心逻辑：
使用射线检测鼠标指向的对象，判断其是否可被拾取。
如果检测到可拾取的物体，将其拾取到指定的手持位置，并禁用其物理模拟。
提供放下物体的功能，允许玩家将手持的物体放置到合适的表面上，并恢复其物理模拟。
支持动态生成隐藏的圆球（可选功能），用于在未拾取物体时显示手的位置。
编辑器参数：
holdPosition：手持物体的目标位置。
putdownUI：放下提示的 UI 预制体。
handSphere：手上的圆球预制体（可选）。
2. pickupmethod.cs
功能：负责检测鼠标点击的物体，并触发拾取或放下的逻辑。同时管理拾取 UI 的显示和隐藏。
核心逻辑：
使用射线检测鼠标点击的对象，判断其是否可被拾取。
如果检测到可拾取的物体，显示拾取 UI，并允许玩家通过点击按钮来拾取物体。
如果玩家手持物体且点击了特定的物体（如“小车”），则触发放下的逻辑。
动态管理拾取和放下 UI 的显示和隐藏，确保其在适当的情况下显示或隐藏。
编辑器参数：
pickupUIPrefab：拾取 UI 的预制体。
pickupController：PickupController 组件的引用。