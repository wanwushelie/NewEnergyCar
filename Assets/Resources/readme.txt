Resources 文件夹有特殊的系统功能，里面的资源会被打包到 resources.assets 文件中，运行时可以通过 Resources.Load 方法动态加载。
假设我们正在开发一个简单的跑酷游戏，玩家控制一个角色在不断生成的赛道上奔跑，同时躲避障碍物和收集道具。

├── Resources
│   ├── Models
│   │   └── ObstacleModel.prefab  (动态加载的障碍物模型)
│   ├── Prefabs
│   │   └── ObstaclePrefab.prefab (动态加载的障碍物预制体)
├── Models
│   ├── CharacterModel.fbx        (角色模型)
│   ├── TrackModel.fbx            (赛道模型)
├── Prefabs
│   ├── CharacterPrefab.prefab    (角色预制体)
│   ├── TrackPrefab.prefab        (赛道预制体)

Models/CharacterModel.fbx 和 Models/TrackModel.fbx：
	这些模型是静态的，直接用于构建游戏的核心场景和角色。
	它们在开发过程中被直接引用到预制体或场景中。

Resources/Models/ObstacleModel.prefab：
	这是一个动态加载的模型，用于在运行时生成障碍物。
	它通过 Resources.Load 方法加载，避免一开始就占用内存。