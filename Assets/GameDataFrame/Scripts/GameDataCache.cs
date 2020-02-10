using Excel.Log.Logger;
using UnityEngine;

namespace GameDataFrame
{
    /// <summary>
    /// 可以用来缓存载入的数据，避免每次都从json或者bundle里获取
    /// </summary>
    public class GameDataCache : Singleton<GameDataCache>
    {
		[HideInInspector]
		public VideoDataSet videoDataSet;
		[HideInInspector]
		public VideoSourceDataSet videoSourceDataSet;
		[HideInInspector]
		public ChapterDataSet chapterDataSet;
		[HideInInspector]
		public OptionActionDataSet optionActionDataSet;
		[HideInInspector]
		public QTEActionDataSet QTEActionDataSet;
		[HideInInspector]
		public OptionDataSet optionDataSet;
		[HideInInspector]
		public ConditionDataSet conditionDataSet;
		[HideInInspector]
		public GameVariableSet gameVariableSet;
		[HideInInspector]
		public CharacterDataSet characterDataSet;
		[HideInInspector]
        public ExtraVideoDataSet extraVideoDataSet;
        [HideInInspector]
        public ExtraVideoConditionDataSet extraVideoConditionDataSet;

        //[HideInInspector]
        //public VideoDataSet videoDataSet;
        public void Load()
        {
            videoDataSet = new VideoDataSet();
			videoDataSet.Load();
			videoSourceDataSet = new VideoSourceDataSet();
			videoSourceDataSet.Load();
			chapterDataSet = new ChapterDataSet();
			chapterDataSet.Load();
			optionActionDataSet = new OptionActionDataSet();
			optionActionDataSet.Load();
			QTEActionDataSet = new QTEActionDataSet();
			QTEActionDataSet.Load();
			optionDataSet = new OptionDataSet();
			optionDataSet.Load();
			conditionDataSet = new ConditionDataSet();
			conditionDataSet.Load();
			gameVariableSet = new GameVariableSet();
			gameVariableSet.Load(); 
			// extraVideoDataSet = new ExtraVideoDataSet(); 
			// extraVideoDataSet.Load();
   //          extraVideoConditionDataSet = new ExtraVideoConditionDataSet();
   //          extraVideoConditionDataSet.Load();
            characterDataSet = new CharacterDataSet();
            characterDataSet.Load();
        }



        public void Clear()
        {
	        videoDataSet = null;
	        videoSourceDataSet = null;
	        chapterDataSet = null;
	        optionActionDataSet = null;
	        QTEActionDataSet = null;
	        optionDataSet = null;
	        conditionDataSet = null;
	        gameVariableSet = null;
            extraVideoDataSet = null;
            extraVideoConditionDataSet = null;
            chapterDataSet = null;
            Resources.UnloadUnusedAssets();
        }
    }
}


