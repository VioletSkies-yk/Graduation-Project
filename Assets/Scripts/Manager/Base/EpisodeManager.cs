using Assets.Scripts.GamePlay.GameLogic;
using Assets.Scripts.GamePlay.UI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class EpisodeManager : OsSingletonMono<EpisodeManager>
{
    private Queue<LevelStage> _levelQueue;
}
