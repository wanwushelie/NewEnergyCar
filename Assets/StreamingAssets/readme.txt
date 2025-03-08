StreamingAssets 文件夹用于存放需要原封不动打包到游戏安装包中的文件。这些文件在运行时可以通过文件系统路径访问，但不会被 Unity 的资源系统（如 Resources 或 AssetBundle）处理。
常见用途
配置文件：如 JSON、XML 或 CSV 文件，用于存储游戏设置、关卡数据、物品属性等。
初始 AssetBundle：在游戏启动时加载的基础资源包。
静态资源：如视频文件、音频文件等，这些资源在运行时需要直接访问，而不是通过 Unity 的资源系统加载。、

假设你有一个开屏视频 intro.mp4：
1. 使用 StreamingAssets 加载
优点：视频文件原封不动打包到游戏安装包中，不会被压缩。适合大文件（如视频），因为可以直接通过文件路径访问。
缺点：文件路径可能需要特殊处理（如在 Android 上可能需要通过 UnityWebRequest 加载）。文件占用空间较大，因为不会被压缩。

2. 使用 Resources 加载
优点：视频文件会被打包到 resources.assets 文件中，加载时通过 Resources.Load 完成。文件会被压缩，占用空间较小。
缺点：如果资源过多，resources.assets 文件会变得很大，影响加载效率。不适合大文件（如视频），因为加载时需要解压，可能会导致卡顿。

3. 直接放在普通文件夹中（如 Videos）
优点：文件路径简单，直接通过 Application.dataPath 访问。适合大文件，不会被压缩。
缺点：文件路径需要手动拼接，不如 StreamingAssets 那样直接。文件不会被打包到 resources.assets，需要额外管理。