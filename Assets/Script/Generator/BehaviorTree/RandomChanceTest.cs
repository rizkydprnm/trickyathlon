using UnityEngine;

namespace BehaviorTree
{
    public class RandomChanceTest : Node
    {
        [Tooltip("The chance of success, between 0 and 1."), Range(0f, 1f), SerializeField]
        private float chance = 0.5f;

        [Tooltip("If test fails, the chance should go up by this amount. Defaults to zero."), Range(0f, 1f), SerializeField]
        private float chanceIncrease = 0;

        [Tooltip("If test succeeds, the chance should go down by this amount. Defaults to zero."), Range(0f, 1f), SerializeField]
        private float chanceDecrease = 0;

        public override bool Execute(ref GeneratorData data)
        {
            // Generate a random value between 0 and 1
            float randomValue = (float)data.Randomizer.NextDouble();

            // Check if the random value is less than or equal to the chance
            if (randomValue <= chance)
            {
                // chance -= chanceDecrease;
                chance = Mathf.Clamp(chance - chanceDecrease, 0f, 1f);
                return true;
            }
            else
            {
                chance = Mathf.Clamp(chance + chanceIncrease, 0f, 1f);
                return false;
            }
        }
    }
}