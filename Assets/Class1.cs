using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class PeoPle
{
    public int age;
}

public class BusPass
{
    /// <summary>
    /// 是否正在乘车
    /// </summary>
    public bool isTakingBus;

    /// <summary>
    /// 上车站
    /// </summary>
    public int startIndex;

    /// <summary>
    /// 下车站
    /// </summary>
    public int endIndex;

    /// <summary>
    /// 刷卡时间
    /// </summary>
    public float swipeTime;

    public BusPass()
    {
        isTakingBus = false;
        startIndex = -1;
        endIndex = -1;
        swipeTime = -1;
    }

    //index为刷卡车站，endpos为终点站
    public void OnSwipingCard(int index, int endPos, float curTime)
    {
        //处于乘车状态时
        if (isTakingBus)
        {
            if (startIndex != -1)
            {
                //同一站重复刷卡
                if (index == startIndex && curTime - swipeTime < 15f)
                {
                    swipeTime = curTime;
                    return;
                }
                //上次乘车没刷下车
                else if (index <= startIndex || curTime - swipeTime > 10800f)
                {
                    Cost(startIndex, endPos);
                    isTakingBus = true;
                    startIndex = index;
                    endIndex = -1;
                    swipeTime = curTime;
                }
                //正常下车
                else
                {
                    Cost(startIndex, index);
                    startIndex = -1;
                    endIndex = -1;
                    isTakingBus = false;
                    swipeTime = -1;
                }
            }
        }
        //未处于乘车状态时
        else
        {
            startIndex = index;
            swipeTime = curTime;
            isTakingBus = true;
            swipeTime = curTime;
        }
    }

    //车费计算，start为上车站，end为下车站
    public int Cost(int start, int end)
    {
        return end - start;
    }
}

public class Test
{
    private void RemoveAllUnderage(List<PeoPle> peopleList)
    {
        for (int i = peopleList.Count - 1; i >= 0; i--)
        {
            if (peopleList[i].age < 18)
            {
                peopleList.Remove(peopleList[i]);
            }
        }
    }

    private int FindTargetNum(int[] nums, int target)
    {
        int left = 0;
        int right = nums.Length - 1;

        while (left <= right)
        {
            int mid = left + (right - left) / 2;
            if (nums[mid] == target)
            {
                return mid;
            }
            else if (nums[mid] < target)
            {
                left = mid + 1;
            }
            else if (nums[mid] > target)
            {
                right = mid - 1;
            }
        }
        return -1;
    }
}
