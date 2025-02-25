系统概述
这个系统实现了一个基于射线检测的高亮显示和UI显示功能。当物体出现在玩家屏幕中央时，系统会检测指向的对象，并对该对象进行高亮显示，同时显示与该对象关联的UI。如果视线移开，高亮和UI显示会消失。系统还支持对高亮效果的自定义，例如颜色、闪烁效果等。

使用方法
1.创建一个空物体管理器，将UIController.cs和CentralRayController.cs脚本附加到该对象上。设置uiCanvasPosition为UI Canvas的定位点。
2.将HighlightingEffect.cs脚本附加到Camera上。
3.为需要高亮的对象附加HighlightAndUIShow和HighlightableObject脚本。在HighlightAndUIShow中设置uiPrefab（文件夹中的显示预制体）、showingLayer（showing）和showingTag（UIshowing）。脚本中的不一定有用，需要在编辑器自己手动设置Layer和Tag。

模块文件名：Assets/moudle/Highlight
相关文件：Assets/Plugins/HighlightingSystem（整个文件夹）

脚本功能介绍
1. CentralRayController.cs
功能：作为系统的主控制器，负责检测鼠标指向的对象，并管理高亮和UI显示逻辑。
核心逻辑：
使用屏幕中心的射线检测鼠标指向的对象。
如果检测到新的目标对象，调用其HighlightAndUIShow组件来开启高亮和UI显示。
如果目标对象改变或鼠标移开，则关闭当前高亮对象的高亮和UI显示。
2. HighlightAndUIShow.cs
功能：为每个需要高亮显示的对象提供高亮和UI显示的接口。
核心逻辑：
检查对象是否满足高亮条件（Layer匹配）。
调用HighlightableObject组件来控制高亮效果。
调用UIController来显示或隐藏UI。
编辑器参数：
uiPrefab：与对象关联的UI预制体。
showingLayer：用于高亮显示的Layer。
showingTag：触发UI显示的Tag。
3. UIController.cs
功能：管理UI的显示和隐藏。
核心逻辑：
提供ShowUI方法用于实例化并显示UI。
提供HideUI方法用于销毁当前显示的UI。
编辑器参数：
uiCanvasPosition：UI显示的Canvas定位点。
4. HighlightableObject.cs
功能：为对象提供高亮效果的实现。
核心逻辑：
支持多种高亮模式，包括闪烁、常亮、单帧高亮等。
使用Shader和Material实现高亮效果。
提供多种高亮参数的设置方法。
编辑器参数：
highlightingLayer：用于高亮显示的Layer（默认为Layer 7）。
constantOnSpeed：常亮模式的开启速度。
constantOffSpeed：常亮模式的关闭速度。
transparentCutoff：透明材质的截止值。
5. HighlightingEffect.cs
功能：实现高亮效果的渲染逻辑，包括模糊、下采样等。
核心逻辑：
使用额外的Camera渲染高亮对象到Stencil Buffer。
使用Shader对Stencil Buffer进行模糊处理。
将模糊后的高亮效果与原画面合成。
编辑器参数：
stencilZBufferDepth：Stencil Buffer的深度。
_downsampleFactor：下采样因子。
iterations：模糊迭代次数。
blurMinSpread：模糊最小扩散值。
blurSpread：每次迭代的模糊扩散值。
_blurIntensity：模糊强度。
