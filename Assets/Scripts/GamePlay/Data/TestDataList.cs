using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.GamePlay.Data
{
    public class TestDataList
    {
        public class Sheet1Data
        {
            /// <summary>	
            /// 关卡ID
            /// </summary>
            public int ID;

            /// <summary>	
            /// 奖励类型
            /// </summary>
            public int RewardType;

            /// <summary>	
            /// 最小奖励
            /// </summary>
            public float MinReward;

            /// <summary>	
            /// 最大奖励
            /// </summary>
            public float MaxReward;

            /// <summary>	
            /// 测试数组
            /// </summary>
            public int[] GroupTest;

            /// <summary>	
            /// 测试字符串数组
            /// </summary>
            public string[] GroupStringTest;

            public string MaxString;

        }

        public class Sheet1DataList
        {
            public List<Sheet1Data> StoreItemDic = new List<Sheet1Data>();

            //public List<TerritoryStoreData> StoreItems = new List<TerritoryStoreData>();
            public void Init(List<Sheet1> dataArray)
            {
                StoreItemDic.Clear();

                for (int i = 0; i < dataArray.Count; i++)
                {
                    Sheet1 item = dataArray[i];

                    Sheet1Data data = new Sheet1Data();

                    data.ID = item.ID;

                    data.MaxReward = item.MaxReward;
                    data.MinReward = item.MinReward;
                    data.RewardType = item.RewardType;
                    data.GroupTest = item.GroupTest;
                    data.GroupStringTest = item.GroupStringTest;
                    data.MaxString = item.MaxString;

                    StoreItemDic.Add(data);
                }
            }
        }
        public class Hero1Data
        {
            /// <summary>	
            /// 角色ID
            /// </summary>
            public int ID;

            /// <summary>	
            /// 血量数值
            /// </summary>
            public float HP;

            /// <summary>	
            /// 测试数组
            /// </summary>
            public int[] GroupTest;

        }
        public class Hero1DataList
        {
            public List<Hero1Data> HeroDataList = new List<Hero1Data>();

            public void Init(List<Hero1> dataArray)
            {
                HeroDataList.Clear();

                for (int i = 0; i < dataArray.Count; i++)
                {
                    Hero1 item = dataArray[i];

                    Hero1Data data = new Hero1Data();

                    data.ID = item.ID;

                    data.HP = item.HP;
                    data.GroupTest = item.GroupTest;

                    HeroDataList.Add(data);
                }
            }
        }
    }
}
