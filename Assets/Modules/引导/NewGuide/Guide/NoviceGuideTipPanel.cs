using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoviceGuideTipPanel : View
{

    public NoviceGuidePanel guidePanel;

    public void OnBtnSkipClick() {
        // 修改数据
        // PlayerPrefs.SetInt(Const.Novice, 1);
        PlayerPrefs.SetInt("Novice", 1); // 直接使用字符串常量
        // 隐藏自己
        Hide();
    }

    public void OnBtnEnterClick() {

        // MenuPanel.isGuide = true;

        // 开始引导 执行第一步
        guidePanel.ExcuteStep(0);
        Hide();
    }

}
