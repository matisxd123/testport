﻿namespace EzEvade_Port.Utils
{
    using System;
    using System.Collections.Generic;
    using Aimtec;

    public static class DelayAction
    {
        public delegate void Callback();

        public static List<Action> ActionList = new List<Action>();

        static DelayAction()
        {
            Game.OnUpdate += GameOnOnGameUpdate;
        }

        private static void GameOnOnGameUpdate()
        {
            for (var i = ActionList.Count - 1; i >= 0; i--)
            {
                if (ActionList[i].Time <= Environment.TickCount)
                {
                    try
                    {
                        if (ActionList[i].CallbackObject != null)
                        {
                            ActionList[i].CallbackObject();
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }

                    ActionList.RemoveAt(i);
                }
            }
        }

        public static void Add(int time, Callback func)
        {
            var action = new Action(time, func);
            ActionList.Add(action);
        }

        public struct Action
        {
            public Callback CallbackObject;
            public int Time;

            public Action(int time, Callback callback)
            {
                Time = time + (int) Environment.TickCount;
                CallbackObject = callback;
            }
        }
    }
}