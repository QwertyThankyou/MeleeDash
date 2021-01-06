using System;

namespace DefaultNamespace
{
    [Serializable]
    public static class Bank
    {
        // Массив отражает статус скина, при 0 - закрыт, 1 - открыт
        public static int[] skins = new int[]
        {
            1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
        };
        public static int health = 10;
        public static int healthCost = 100;
        public static int damage = 1;
        public static int damageCost = 1000;
        public static int money = 1002;
        public static int currentSkin = 0;
        public static bool isDone = false;
    }
}