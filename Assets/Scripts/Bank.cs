﻿using System;

namespace DefaultNamespace
{
    [Serializable]
    public static class Bank
    {
        // Массив отражает статус скина, при 0 - закрыт, 1 - открыт
        public static int[] skins = new int[]
        {
            1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
            1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1
        };
        public static int health = 1;
        public static int damage = 1;
        public static int money = 0;
        public static int currentSkin = 14;
        public static bool isDone = false;
    }
}