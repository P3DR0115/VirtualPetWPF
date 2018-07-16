using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualPetWPF
{
    public class Skill
    {
        public string skillName; // The name of the skill
        public int levelRequired; // The level required to learn the skill
        public int staminaCost; // Stamina it costs to do the skillx
        public int successRate;
        public int XPEarned; // Experience Points earned for successfully executing the skill.
        public int maxXP; // Max XP reward for skill
        public int temp;

        public void Execute(Pet subject, int levelReq, int stamCost, int successChance, int maxReward)
        {

         Random rnd = new Random();
            // Begin Checks for skill Execution
            if(subject.level >= levelReq)
            {
                if (subject.stamina >= stamCost)
                {
                    subject.stamina -= stamCost;
                    temp = rnd.Next(99);
                    if (temp <= successChance)
                    {
                        // Successfully executed skill
                        XPEarned = 0; // Reset just in case;
                        XPEarned = rnd.Next(maxReward) + 1;
                        subject.experiencePoints += XPEarned;
                        subject.levelUp();

                        if (XPEarned == maxReward)
                            subject.Gems += 5; // Small Reward for flawless Execution
                        
                    }
                }
            }
        }

    }

    public class Speak : Skill
    {
        public Speak()
        {
            skillName = "Speak";
            levelRequired = 2;
            staminaCost = 5;
            successRate = 90;
            maxXP = 15;
        }
    }

    public class Sit : Skill
    {
        public Sit()
        {
            skillName = "Sit";
            levelRequired = 0;
            staminaCost = 2;
            successRate = 95;
            maxXP = 5;
        }
    }

    public class Shake : Skill
    {
        public Shake()
        {
            skillName = "Shake";
            levelRequired = 4;
            staminaCost = 15;
            successRate = 85;
            maxXP = 20;
        }
    }

    public class FileTaxes : Skill
    {
        public FileTaxes()
        {
            // lololol
            skillName = "File Taxes";
            levelRequired = 15;
            staminaCost = 35;
            successRate = 30;
            maxXP = 100;
        }
    }

    public class Roll : Skill
    {

        public Roll()
        {
            skillName = "Roll";
            levelRequired = 5;
            staminaCost = 20;
            successRate = 60;
            maxXP = 35;
        }
    }
} // namespace
