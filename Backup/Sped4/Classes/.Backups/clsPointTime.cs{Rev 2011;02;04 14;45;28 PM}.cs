using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Diagnostics;

namespace Sped4.Classes
{
    class clsPointTime
    {
        private struct _PointTime
        {
            internal int X;
            internal DateTime Time;
        }

        private ArrayList PointTimeList = new ArrayList();

        public void AddPointTime(int X, DateTime Time)
        {
            _PointTime PointTime = default(_PointTime);
            PointTime.X = X;
            PointTime.Time = Time;
            PointTimeList.Add(PointTime);
        }

        public void ClearPointTime()
        {
            PointTimeList.Clear();
        }

        public DateTime GetTimeFromPoint(int X)
        {

            _PointTime PointTime = default(_PointTime);
            DateTime ReturnDateTime = default(DateTime);
            int TempX = 0;
            int BestX = 10000000;

            for (int i = 0; i <= PointTimeList.Count - 1; i++)
            {
                if (PointTimeList[i] is _PointTime)
                {
                    PointTime = (_PointTime)PointTimeList[i];
                    TempX = PointTime.X - X;
                    if (TempX < 0)
                    {
                        TempX *= -1;
                    }
                    if (TempX < BestX)
                    {
                        BestX = TempX;
                        ReturnDateTime = PointTime.Time;
                    }
                }
            }

            return ReturnDateTime;

        }


        public int GetPointFromTime(DateTime Time)
        {

            _PointTime PointTime = default(_PointTime);
            int ReturnPoint = -1;
            DateTime BestTime = DateTime.Parse("31.12.9999");
            DateTime LastTimeBig = DateTime.Parse("31.12.9999");
            DateTime LastTimeSmal = DateTime.Parse("31.12.1900");

            for (int i = 0; i <= PointTimeList.Count - 1; i++)
            {
                if (PointTimeList[i] is _PointTime)
                {
                    
                    PointTime = (_PointTime)PointTimeList[i];
                    if (PointTime.Time == Time)
                    {
                      BestTime = PointTime.Time;
                      ReturnPoint = PointTime.X;
                    }
                    else
                    {
                      if (PointTime.Time< Time)
                      {
                        if(Time < PointTime.Time.AddMinutes(29))
                        {
                            BestTime = PointTime.Time;
                            ReturnPoint = PointTime.X;
                        }
                      }
                    }
                }
            }
            if (ReturnPoint == -1)
            {
                if (Time > PointTime.Time)
                {
                    for (int i = 0; i <= PointTimeList.Count - 1; i++)
                    {
                        if (PointTimeList[i] is _PointTime)
                        {
                            PointTime = (_PointTime)PointTimeList[i];
                            if (PointTime.Time > LastTimeSmal)
                            {
                                LastTimeSmal = PointTime.Time;
                                ReturnPoint = PointTime.X;
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 0; i <= PointTimeList.Count - 1; i++)
                    {
                        if (PointTimeList[i] is _PointTime)
                        {
                            PointTime = (_PointTime)PointTimeList[i];
                            if (PointTime.Time < LastTimeBig)
                            {
                                LastTimeBig = PointTime.Time;
                                ReturnPoint = PointTime.X;
                            }
                        }
                    }
                }
            }

            return ReturnPoint;

        }

        public ArrayList GetXArray()
        {

            ArrayList PointList = new ArrayList();
            _PointTime PointTime = default(_PointTime);

            for (int i = 0; i <= PointTimeList.Count - 1; i++)
            {
                if (PointTimeList[i] is _PointTime)
                {
                    PointTime = (_PointTime)PointTimeList[i];
                    PointList.Add(PointTime.X);
                }
            }

            return PointList;

        }

    }
}
