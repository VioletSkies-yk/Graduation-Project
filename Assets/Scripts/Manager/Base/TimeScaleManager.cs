using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum TimeScaleEnum
{
    UITweenEffects,
    MusouEffect,
    KillEffect,
    LevelWinEffect,
    WeakEffect,
    BreakArmorEffect,
    BossCloseupEffect,
    CurtainEffect,
    BossAppearEffect,
    AnimationCamera,
    TimeSlowDown,
    HitStop,
    SimpleMissileHitStop,
    GamePause,
    TimeScaleListener,
    ActionEventListener,
    TutorialPanel,
    TutorialTrigger,
    EndEffect,
}

public class TimeScaleConfig
{
    public TimeScaleEnum _type;
    public float _scaleValue;
    public float _scaleLifeTime;
    public float _scaleProcessTime;
    public System.Action _finishCallBack;
}

public class TimeScaleManager : OsSingletonMono<TimeScaleManager>
{
    List<TimeScaleConfig> _timeScaleList = new List<TimeScaleConfig>();

    public void AddTimeScale(TimeScaleEnum type, float value, float lifeTime = float.MaxValue, System.Action callBack = null)
    {
        //AppLogger.Debug("[Tracy] AddTimeScale type : {0} value : {1}",type,value);

        if (_timeScaleList.Count > 0)
        {
            for (int i = 0; i < _timeScaleList.Count; i++)
            {
                TimeScaleConfig lastconfig = _timeScaleList[i];
                if (null != lastconfig._finishCallBack)
                    lastconfig._finishCallBack();
            }
        }
        _timeScaleList.Clear();
        TimeScaleConfig newTimeScale = new TimeScaleConfig();
        newTimeScale._type = type;
        newTimeScale._scaleValue = value;
        newTimeScale._scaleLifeTime = lifeTime;
        newTimeScale._finishCallBack = callBack;
        _timeScaleList.Add(newTimeScale);
        //#if TAI_DEVELOPMENT_BUILD || UNITY_EDITOR
        //		if (!CheatManager._isTimeScaleCheat)
        //#endif
        Time.timeScale = newTimeScale._scaleValue;
    }

    public void StopTimeScale(TimeScaleEnum type)
    {
        if (_timeScaleList.Count > 0)
        {
            TimeScaleConfig config = _timeScaleList.Find((obj) => obj._type == type);
            if (null != config)
            {
                config._scaleLifeTime = 0;
            }
        }
    }

    public void StopAllTimeScale()
    {
        for (int i = 0; i < _timeScaleList.Count; ++i)
        {
            if (_timeScaleList[i] != null)
            {
                _timeScaleList[i]._scaleLifeTime = 0f;
            }
        }
    }

    void Update()
    {
        if (_timeScaleList.Count > 0)
        {
            TimeScaleConfig config = _timeScaleList[_timeScaleList.Count - 1];
            if (config._scaleProcessTime >= config._scaleLifeTime)
            {
                //#if TAI_DEVELOPMENT_BUILD || UNITY_EDITOR
                //				if (!CheatManager._isTimeScaleCheat)
                //#endif
                Time.timeScale = 1.0f;
                if (null != config._finishCallBack)
                {
                    config._finishCallBack();
                }
                _timeScaleList.RemoveAt(_timeScaleList.Count - 1);
            }
            else
            {
                config._scaleProcessTime += Time.unscaledDeltaTime;
            }
        }
    }
}

