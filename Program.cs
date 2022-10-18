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
        //Lives system variables
        static int lives;
        static int defaultLives;
        static int maxLives;

        static OrderedDictionary Enemy;

        static void Main()
        {
            InitValues();
            ShowHud();

            DebugSequence();

            Console.ReadKey(true);
        }

        static void DebugSequence()
        {
            Console.WriteLine("Testing spawning a random enemy");
            SpawnEnemy();
            ShowHud();
            Console.WriteLine("Testing default enemy attack");
            EnemyAttack();
            ShowHud();
            Console.WriteLine("Testing negative 420 damage to player");
            TakeDamage(-420);
            ShowHud();
            Console.WriteLine("Testing default player attack");
            PlayerAttack();
            ShowHud();
            Console.WriteLine("Testing negative 69 damage to enemy");
            EnemyTakeDamage(-69);
            ShowHud();
            Console.WriteLine("Testing enemy death");
            EnemyDie();
            ShowHud();
            Console.WriteLine("Spawning super strong enemy for testing purposes");
            Enemy = new OrderedDictionary()
            {
                {"name", "super goblin"},
                {"health", 99999},
                {"attack", 300}
            };
            Console.ReadKey(true);
            ShowHud();
            Console.WriteLine("Testing player being killed by an enemy");
            EnemyAttack();
            ShowHud();
            Console.WriteLine("Testing heal item");
            HealItem();
            ShowHud();
            Console.WriteLine("Testing heal values of negative 666 for health and armour");
            Heal(-666);
            ShowHud();
            ArmourHeal(-666);
            ShowHud();
            Console.WriteLine("Testing healing the absurd amount of 500 to armour");
            ArmourHeal(500);
            ShowHud();
            Console.WriteLine("Testing picking up a life item");
            LifeItem();
            ShowHud();
            Console.WriteLine("Testing picking up a random weapon");
            GetWeapon();
            ShowHud();
            Console.WriteLine("Testing game over; result should be restarting of debug sequence");
            GameOver();
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
            Console.WriteLine("| Lives: " + lives);
            Console.WriteLine("| Health: " + health + " | Status: " + GetHealthStatus() + " |");
            Console.WriteLine("| Armour: " + armour + " | Status: " + GetArmourStatus() + " |");
            Console.WriteLine("| Weapon: " + weaponName + " | Strength: " + weaponDamage + " |");
            Console.WriteLine("| Currently fighting: " + Enemy["name"] + " |");

            Console.ReadKey(true);
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
                Console.ReadKey(true);
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
                return "great shape";
            else if (health == 100)
                return "tip top shape";
            else if (health >= 101 && health <= 150)
                return "overcharge";
            else if (health >= 151)
            {
                health = 150;
                return "Error: invalid health value, reducing health to " + maxHealth;
            }
            else if (health <= 0)
                return "a better place";

            health = defaultHealth;
            return "Error: invalid health value, setting health to " + defaultHealth;
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
                return "in near perfect condition";
            else if (armour == 100)
                return "undamaged";
            else if (armour >= 101 && armour <= 150)
                return "stronger than steel";
            else if (health >= 151)
            {
                armour = 150;
                return "Error: invalid armour value, reducing armour to " + maxArmour;
            }
            else if (armour <= 0)
                return "destroyed";

            armour = defaultArmour;
            return "Error: invalid armour value, setting armour to " + defaultArmour;
        }

        static void TakeDamage(int enemyDamage)
        {
            Console.WriteLine("Debug: " + name + " is about to take " + enemyDamage + " damage");
            Console.ReadKey(true);

            if (enemyDamage < 0)
            {
                Console.WriteLine("Error: invalid damage value, setting damage to 0");
                enemyDamage = 0;
                Console.ReadKey(true);
            }

            if (armour > 0)
            {
                armour -= enemyDamage;
                Console.WriteLine(name + " took " + enemyDamage + " damage!");
                Console.ReadKey(true);
                Console.WriteLine(name + "'s armour is " + GetArmourStatus() + "!");

                if (armour < 0)
                {
                    health += armour;
                    armour = 0;
                    Console.WriteLine(name + " is in " + GetHealthStatus() + "!");
                }
                Console.ReadKey(true);
            }
            else
            {
                health -= enemyDamage;
                Console.WriteLine(name + " took " + enemyDamage + " damage!");
                Console.WriteLine(name + " is in " + GetHealthStatus() + "!");
                Console.ReadKey(true);
            }

            DeathCheck();
        }

        static void Heal(int healAmount)
        {
            Console.WriteLine("Debug: " + name + " is about to heal " + healAmount + " health");
            Console.ReadKey(true);

            if (healAmount < 0)
            {
                Console.WriteLine("Error: invalid heal value, setting heal amount to 0");
                healAmount = 0;
                Console.ReadKey(true);
            }

            Console.WriteLine(name + " healed " + healAmount + "!");

            health += healAmount;
            if (health > maxHealth)
            {
                Console.WriteLine("Maximum health reached! Reducing health to " + maxHealth);
                Console.ReadKey(true);

                health = maxHealth;
                Console.ReadKey(true);
            }
 
            Console.WriteLine(name + " is in " + GetHealthStatus() + "!");
            Console.ReadKey(true);
        }

        static void HealItem()
        {
            Console.WriteLine(name + " picked up a health item!");
            Console.ReadKey(true);
            Heal(50);
        }

        static void ArmourHeal(int healAmount)
        {
            Console.WriteLine("Debug: " + name + " is about to heal " + healAmount + " armour");
            Console.ReadKey(true);

            if (healAmount < 0)
            {
                Console.WriteLine("Error: invalid heal value, setting heal amount to 0");
                healAmount = 0;
                Console.ReadKey(true);
            }

            Console.WriteLine(name + " healed " + healAmount + " armour!");
            Console.ReadKey(true);

            armour += healAmount;
            if (armour > maxArmour)
            {
                Console.WriteLine("Maximum armour reached! Reducing armour to " + maxArmour);

                armour = maxArmour;
                Console.ReadKey(true);
            }

            Console.WriteLine(name + "'s armour is " + GetArmourStatus() + "!");
            Console.ReadKey(true);
        }

        static void ArmourHealItem()
        {
            Console.WriteLine(name + " picked up an armour item!");
            Console.ReadKey(true);
            ArmourHeal(50);
        }

        static OrderedDictionary DecideEnemy()
        {
            OrderedDictionary[] Enemies = new OrderedDictionary[]
            {
                new OrderedDictionary()
                {
                    {"name", "goblin"},
                    {"health", 30},
                    {"attack", 15}
                },
                new OrderedDictionary()
                {
                    {"name", "skeleton"},
                    {"health", 20},
                    {"attack", 20}
                },
                new OrderedDictionary()
                {
                    {"name", "troll"},
                    {"health", 40},
                    {"attack", 25}
                }
            };

            Enemy = Enemies[rnd.Next(Enemies.Count())];
            return Enemy;
        }

        static void EnemyAttack()
        {
            Console.WriteLine("The " + Enemy["name"] + " attacks!");
            Console.ReadKey(true);
            TakeDamage((int)Enemy["attack"]);
        }

        static void EnemyTakeDamage(int playerDamage)
        {

            Console.WriteLine("Debug: the " + Enemy["name"] + " is about to take " + playerDamage + " damage");
            Console.ReadKey(true);

            if (playerDamage < 0)
            {
                Console.WriteLine("Error: invalid damage value, setting damage to 0");
                playerDamage = 0;
                Console.ReadKey(true);
            }
            
            int currentHealth = (int)Enemy[""];
            currentHealth -= playerDamage;
            Enemy["health"] = currentHealth;
            //for some reason, int casts cannot be subtracted from, and the enemy's health cannot be subtracted from but instead must be set with an = with no other operands involved

            Console.WriteLine("The " + Enemy["name"] + " took " + playerDamage + " damage!");
            Console.ReadKey(true);

            EnemyDeathCheck();
        }

        static void EnemyDeathCheck()
        {
            if ((int)Enemy["health"] <= 0)
                EnemyDie();
        }

        static void EnemyDie()
        {
            Console.WriteLine("The " + Enemy["name"] + " died!");
            Console.ReadKey(true);

            Enemy = new OrderedDictionary();
        }

        static void DropItem()
        {
            switch (rnd.Next(0, 11))
            {
                case 0:
                case 1:
                case 2:
                case 3:
                    break;
                case 4:
                case 5:
                    HealItem();
                    break;
                case 6:
                case 7:
                    ArmourHealItem();
                    break;
                case 8:
                    LifeItem();
                    break;
                case 9:
                case 10:
                    GetWeapon();
                    break;
            }
        }

        static void LifeItem()
        {
            Console.WriteLine(name + " picked up a life item!");
            Console.ReadKey(true);
            LifeUp(1);
        }

        static void LifeUp(int lifeIncrease)
        {
            lives += lifeIncrease;
            
            if (lives > maxLives)
            {
                Console.WriteLine("Maximum lives reached! Lives set to " + maxLives);
                lives = maxLives;
                Console.ReadKey(true);
            }
            Console.WriteLine(name + " now has " + lives + " lives");
            Console.ReadKey(true);
        }

        static void SpawnEnemy()
        {
            if (Enemy == null)
                Console.ReadKey(true);
            
            Enemy = DecideEnemy();
            Console.WriteLine("A " + Enemy["name"] + " appeared!");
            Console.ReadKey(true);
        }

        static void PlayerAttack()
        {
            Console.WriteLine(name + " attacks!");
            Console.ReadKey(true);
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
            Console.ReadKey(true);
            lives--;
            Console.WriteLine(name + " has " + lives + " lives left");
            Console.ReadKey(true);
            GameOverCheck();
            Respawn();
        }

        static void Respawn()
        {
            health = defaultArmour;
            armour = defaultArmour;
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
            Console.Clear();
            Main();
        }
    }
}