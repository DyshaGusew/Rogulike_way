
        public class Hero
        {
            public int[] coordinates;
            public double StaticHealht = 100;
            public double NowHealht = 100;
            public double StaticStamina = 100;
            public double NowStamina = 100;
            public double damage = 40;
            public int level = 1;
            public int expirience = 0;
            public double boost = 1.0;

            public void CheckAndLevelUp()
            // В том числе восполняет хп и стамину
            {
                if (expirience >= 100)
                {
                    this.level++;
                    this.boost = this.boost + 0.1;
                    this.StaticHealht = this.StaticHealht * this.boost;
                    this.NowHealht = this.StaticHealht;
                    this.StaticStamina = this.StaticStamina * this.boost;
                    this.NowStamina = this.StaticStamina;
                    this.damage = this.damage * this.boost;
                    this.expirience = 0;
                }
            }

        }
        /*
        public class Monsters
        {
            public int[] coordinates;
            public string name;
            public Monsters()
            {
                coordinates = new int[2] { 0, 0 };
            }

            public Monsters(int x, int y) : this()
            {
                coordinates = new int[2] { x, y };
            }
        }
        */
        public class Monsters
        {
            public string name;   //Я создал для тестирования, можно удалить
            public int[] coordinates;
            public double StaticHealht;
            public double NowHealht;
            public double damage;
            public int level;
            public int expirience; // При смерти моба можно передавать его опыт герою
            public double boost;

            public Monsters(double _health, double _damage)
            // Этот конструктор нужен для ручного задания боссов (тк боссы уникальны, то не вижу смысла прописывать ему наследование)
            // Наследования не включают в себя конструктор от класса-наследника
            {
                this.StaticHealht = _health;
                this.NowHealht = this.StaticHealht;
                this.damage = _damage;
                this.level = 666;
                this.boost = 1.0;
                this.expirience = 666;
            }

            public void CheckLevelAndUpStats()
            // Бустит статы монстров при их объявлении
            {
                if (this.level > 1)
                {
            this.boost = 1.0 + 0.1 * (this.level - 1);
                    this.StaticHealht = this.StaticHealht * this.boost;
                    this.NowHealht = this.StaticHealht * this.boost;
                    this.damage = this.damage * this.boost;
                }
            }
        }
/*
        public class Goblins : Monsters
        {
            public double StaticHealht = 50;
            public double damage = 20;
            public int expirience = 30;

            public Goblins(int _level)
            {
                this.level = _level;
                Goblins.CheckLevelAndUpStats();
            }
        }

        public class Ghost : Monsters
        {
            public double StaticHealht = 20;
            public double damage = 40;
            public int expirience = 50;

            public Ghost(int _level)
            {
                this.level = _level;
                Ghost.CheckLevelAndUpStats();
            }
        }

        public class Knight : Monsters
        {
            public double StaticHealht = 120;
            public double damage = 30;
            public int expirience = 50;

            public Knight(int _level)
            {
                this.level = _level;
                Knight.CheckLevelAndUpStats();
            }
        }

        public class Skeleton : Monsters
        {
            public double StaticHealht = 50;
            public double damage = 30;
            public int expirience = 40;

            public Skeleton(int _level)
            {
                this.level = _level;
                Skeleton.CheckLevelAndUpStats();
            }
        }

        public class Rat : Monsters
        {
            public double StaticHealht = 10;
            public double damage = 10;
            public int expirience = 10;

            public Rat(int _level)
            {
                this.level = _level;
                Rat =.CheckLevelAndUpStats();
            }
        }
*/