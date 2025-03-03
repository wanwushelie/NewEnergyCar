系统概述
这个系统实现了一个基于 ObjectData 的物体属性管理和动态修改功能。系统通过 ObjectDataManager 维护一个全局的物体属性字典，支持在运行时动态查询和修改物体的属性（如高亮设置、UI 显示、拾取功能等）。此外，系统还提供了一个 TextInputController，允许用户通过输入框或快捷键动态修改物体的属性，并实时反馈修改结果。

核心功能
物体属性管理：通过 ObjectData 脚本定义物体的属性（如是否可以高亮、高亮颜色、是否显示 UI、是否可以被拾取等）。
单例模式管理：ObjectDataManager 使用单例模式，确保全局唯一性，并支持跨场景使用。
动态修改属性：通过 TextInputController 提供的输入框或快捷键，用户可以在运行时动态修改物体的属性。
实时反馈：修改结果会通过反馈文本实时显示，方便用户确认操作是否成功。

使用方法
创建物体属性数据：
在 Unity 编辑器中，右键点击项目面板，选择 Create > Data > ObjectData，创建一个新的 ObjectData 资源。
在 ObjectData 资源中设置物体的属性，如名称、高亮颜色、是否可以高亮、是否显示 UI、是否可以被拾取等。
创建 ObjectDataManager：
在场景中创建一个空物体（Manager），并将 ObjectDataManager.cs 脚本附加到该物体上。
在 ObjectDataManager 的 Inspector 面板中，将所有需要管理的 ObjectData 资源拖拽到 objectDataList 列表中。
UI为DatacontrolUI，直接拖拽到场景canvas下，包含一个 UI 输入框（InputField）和一个文本组件（Text），用于输入指令和显示反馈信息。TextInputController.cs 脚本附加到该物体上。UI 输入框和文本组件分别拖拽到 inputField 和 feedbackText 字段中。

运行场景：
启动场景后，用户可以在输入框中输入指令（如 Cube:canbehighlighted=true），或使用预设的快捷键（如按下键盘上的 1）来动态修改物体的属性。
修改结果会实时显示在反馈文本中。

模块文件名
Assets/Modules/Data（整个文件夹）
Assets/Modules/Data/DataObject（文件夹专门用来存放数据）


脚本功能介绍
1. ObjectData.cs
功能：定义物体的属性数据，包括高亮设置、UI 显示和拾取功能。
核心逻辑：
提供物体的唯一标识名称（objectName）。
设置物体是否可以高亮（canBeHighlighted）以及高亮颜色（highlightColor）。
设置物体是否显示 UI（showUI）以及对应的 UI 预制体（uiPrefab）。
设置物体是否可以被拾取（canBePickedUp）。
编辑器参数：
objectName：物体的唯一标识名称。
canBeHighlighted：是否可以高亮。
highlightColor：高亮时的颜色。
showUI：是否显示 UI。
uiPrefab：对应的 UI 预制体。
canBePickedUp：是否可以被拾取。
2. ObjectDataManager.cs
功能：作为物体属性数据的全局管理器，负责存储和查询物体的属性。
核心逻辑：
使用单例模式确保全局唯一性，并支持跨场景使用。
将 ObjectData 数据存储在字典中，通过物体名称快速查询。
提供 GetData 方法，根据物体名称获取对应的 ObjectData。
支持动态添加和移除物体数据（可选功能）。
编辑器参数：
objectDataList：需要管理的 ObjectData 资源列表。
3. TextInputController.cs
功能：通过输入框或快捷键动态修改物体的属性，并实时反馈修改结果。
核心逻辑：
提供一个输入框，用户可以输入指令（如 Cube:canbehighlighted=true）来修改物体属性。
支持快捷键映射，用户可以通过预设的快捷键快速修改物体属性。
解析输入指令，调用 ObjectDataManager 查询物体数据并修改属性。
将修改结果实时显示在反馈文本中。
编辑器参数：
inputField：绑定的输入框组件。
feedbackText：用于显示反馈信息的文本组件。
快捷键映射：
1：Cube:canbehighlighted=true
2：Cube:canbehighlighted=false
3：Cube:showui=true
4：Cube:showui=false
5：Cube:highlightcolor=#ff0000（红色）
6：Cube:highlightcolor=#00ff00（绿色）
7：Cube:highlightcolor=#0000ff（蓝色）
8：Cube:highlightcolor=#000000（黑色）
9：Cube:highlightcolor=#ffffff（白色）
0：Cube:canbepickedup=true
00：Cube:canbepickedup=false