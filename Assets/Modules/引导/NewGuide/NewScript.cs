using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewScript : View
{
    public NoviceGuidePanel guidePanel;
    // Start is called before the first frame update
    void Start()
    {
        // MenuPanel.isGuide = true;

        // 开始引导 执行第一步
        guidePanel.ExcuteStep(0);
        Hide();
    }

}
