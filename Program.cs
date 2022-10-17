using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Health_System
{
    internal class Program
    {
        static bool initState;
        static Random rnd;

        static string name;
        //Health system variables
        static int health;
        static int defaultHealth;
        static int maxHealth;
        //Weapon system variables
        static int weaponNum;
        static string weaponName;
        static int weaponDamage;
        //Armour system variables
        static int armour;
        static int defaultArmour;
        static int maxArmour;
        //Experience system variables
        static int level;
        static int maxLevel;
        static int exp;
        static int maxExp;
        //Lives system variables
        static int lives;
        static int defaultLives;
        static int maxLives;

        static OrderedDictionary Enemy;

        static void Main()
        {
            InitValues();
            ShowHud();

            Console.ReadKey(true); 
        }

        static void InitValues()
        {
            initState = true;

            rnd = new Random();

            defaultHealth = 100;
            health = defaultHealth;
            maxHealth = 150;

            weaponNum = 0;
            GetWeapon(weaponNum);

            defaultLives = 3;
            lives = defaultLives;
            maxLives = 9;

            defaultArmour = 100;
            armour = defaultArmour;
            maxArmour = 150;

            level = 1;
            maxLevel = 10;
            exp = 0;
            maxExp = 50000;

            name = GetPlayerName();

            initState = false;

            Enemy = new OrderedDictionary();
        }

        static string GetPlayerName()
        {
            Console.WriteLine("What is your name?");
            return Console.ReadLine();
        }

        static void ShowHud()
        {
            Console.Clear();

            Console.WriteLine("{ Totally Real Game That Exists }");
            Console.WriteLine("| Name: " + name + " |");
            Console.WriteLine("| Level: " + level + " | Exp: " + exp + " |");
            Console.WriteLine("| Health: " + health +" | Armour: " + armour + " |");
            Console.WriteLine("| Weapon: " + weaponName + " | Strength: " + weaponDamage + " |");
            Console.WriteLine("| Currently fighting: " + Enemy["name"]);
        }

        static void GetWeapon(int randomNum = 99)
        {
            if (randomNum == 99)
            {
                randomNum = rnd.Next(0,27);
            }
            weaponNum = randomNum;

            OrderedDictionary Weapons = new OrderedDictionary()
            {
                {"dagger", 10},
                {"longsword", 25},
                {"battleaxe", 30},
                {"mace", 25},
                {"zweihander", 40},
                {"longbow", 30},
                {"crossbow", 35},
                {"sling", 10},
                {"shortsword", 15},
                {"shortbow", 20},
                {"flail", 30},
                {"cudgel", 5},
                {"warhammer", 25},
                {"trident", 20},
                {"spear", 15},
                {"pistol", 25},
                {"musket", 40},
                {"slingshot", 15},
                {"hatchet", 10},
                {"boomerang", 15},
                {"claymore", 35},
                {"harpoon", 20},
                {"quarterstaff", 15},
                {"katana", 25},
                {"shuriken", 10},
                {"bomb", 50},
                {"lance", 35}
            };

            string[] weaponNames = new string[27];
            int[] weaponDamageValues = new int[27];
            Weapons.Keys.CopyTo(weaponNames, 0);
            Weapons.Values.CopyTo(weaponDamageValues, 0);
            weaponName = weaponNames[weaponNum];
            weaponDamage = weaponDamageValues[weaponNum];

            if (!initState)
            {
                Console.WriteLine(name + " found a " + weaponName + "!");
            }
        }

        static string GetHealthStatus()
        {
            if (health >= 1 && health <= 10)
                return "critical condition";
            else if (health >= 11 && health <= 25)
                return "quite poor condition";
            else if (health >= 26 && health <= 50)
                return "rough shape";
            else if (health >= 51 && health <= 75)
                return "mild discomfort";
            else if (health >= 76 && health <= 99)
                return "tip top shape";
            else if (health >= 101 && health <= 150)
                return "overcharge";
            else if (health >= 151)
            {
                health = 150;
                return "error: invalid health value, reducing health to " + maxHealth;
            }   
            else if (health <= 0)
                return "dead";

            health = defaultHealth;
            return "error: invalid health value, setting health to " + defaultHealth;
        }

        static string GetArmourStatus()
        {
            if (armour >= 1 && armour <= 10)
                return "barely holding together";
            else if (armour >= 11 && armour <= 25)
                return "covered in cracks";
            else if (armour >= 26 && armour <= 50)
                return "quite damaged";
            else if (armour >= 51 && armour <= 75)
                return "lightly battered";
            else if (armour >= 76 && armour <= 99)
                return "in perfect condition";
            else if (armour >= 101 && armour <= 150)
                return "stronger than steel";
            else if (health >= 151)
            {
                health = 150;
                return "error: invalid armour value, reducing armour to " + maxArmour;
            }
            else if (health <= 0)
                return "destroyed";

            armour = defaultArmour;
            return "error: invalid armour value, setting armour to " + defaultArmour;
        }

        static void CheckIfLevelUp()
        {
            int[] expToLevelUpByLevel =
            {
                100,
                300,
                800,
                1600,
                3000,
                6000,
                12500,
                25000,
                50000
            };
            int levelToBecome = 0;

            foreach (int expAmount in expToLevelUpByLevel)
            {
                if (exp >= Array.IndexOf(expToLevelUpByLevel, expAmount))
                    levelToBecome++;
                else
                    break;
            }

            if (levelToBecome != 0)
            {
                LevelUp();
            }
        }

        static void LevelUp()
        {
            level++;
            Console.WriteLine(name + " gained a level!");
            Console.WriteLine(name + " is now level " + level);
        }

        static void TakeDamage(int enemyDamage)
        {
            if (enemyDamage < 0)
            {
                Console.WriteLine("error: invalid damage value, setting damage to 0");
                enemyDamage = 0;
            }

            if (armour > 0)
            {
                armour -= enemyDamage;
                Console.WriteLine(name + " took " + enemyDamage + " damage!");
                Console.WriteLine(name + "'s armour is " + GetArmourStatus() + "!");

                if (armour < 0)
                {
                    health += armour;
                    armour = 0;
                    Console.WriteLine(name + " is in " + GetHealthStatus() + "!");
                }
            }
            else
            {
                health -= enemyDamage;
                Console.WriteLine(name + " took " + enemyDamage + " damage!");
                Console.WriteLine(name + " is in " + GetHealthStatus() + "!");

                //DeathCheck();
            }
        }

        static void Heal(int healAmount)
        {
            if (healAmount < 0)
            {
                Console.WriteLine("error: invalid heal value, setting heal amount to 0");
                healAmount = 0;
            }

            Console.WriteLine(name + " healed " + healAmount + "!");
            Console.WriteLine(name + " is in " + GetHealthStatus() + "!");
        }

        static OrderedDictionary DecideEnemy()
        {
            OrderedDictionary[] Enemies = new OrderedDictionary[]
            {
                new OrderedDictionary()
                {
                    {"name", "goblin"},
                    {"health", 30},
                    {"attack", 15},
                    {"showAt", 1},
                    {"dontShowAt", 3}
                },
                new OrderedDictionary()
                {
                    {"name", "skeleton"},
                    {"health", 20},
                    {"attack", 20},
                    {"showAt", 1},
                    {"dontShowAt", 3}
                },
                new OrderedDictionary()
                {
                    {"name", "troll"},
                    {"health", 40},
                    {"attack", 25},
                    {"showAt", 2},
                    {"dontShowAt", 4}
                }
            };

            int enemyShowAt = 99;
            int enemyDontShowAt = 99;
            while (enemyShowAt <= level && enemyDontShowAt >= level)
            {
                Enemy = Enemies[rnd.Next(Enemy.Count)];
                enemyShowAt = (int)Enemy["showAt"];
                enemyDontShowAt = (int)Enemy["dontShowAt"];
            }
            return Enemy;
        }

        static void EnemyAttack()
        {
            Console.WriteLine("The " + (string)Enemy["name"] + " attacks!");
            TakeDamage((int)Enemy["attack"]);
        }

        static void EnemyTakeDamage(int playerDamage)
        {
            if (playerDamage < 0)
            {
                Console.WriteLine("error: invalid damage value, setting damage to 0");
                playerDamage = 0;
            }
            health -= playerDamage;
            Console.WriteLine((string)Enemy["name"] + " took " + playerDamage + " damage!");

            EnemyDeathCheck();
        }

        static void EnemyDeathCheck()
        {
            if ((int)Enemy["health"] <= 0)
                EnemyDie();
        }

        static void EnemyDie()
        {

        }

        static void PlayerAttack()
        {
            Console.WriteLine(name + " attacks!");
            EnemyTakeDamage(weaponDamage);
        }

        static void DeathCheck()
        {
            if (health <= 0)
            {
                Die();
                GameOverCheck();
            }
        }

        static void Die()
        {
            Console.WriteLine(name + " died!");
            lives--;
            Console.WriteLine(lives + "lives left");
        }

        static void GameOverCheck()
        {
            if (lives <= 0)
            {
                GameOver();
            }
        }

        static void GameOver()
        {
            Console.WriteLine("GAME OVER");
            Console.ReadKey(true);
            Main();
        }
    }
}