using System;
using System.Linq;

namespace StatisticsPractice {

    class Dice {
        float diceNumber;
        float sides;

        public Dice (float _diceNumber = 1, float _sides = 1){
            this.diceNumber = _diceNumber;
            this.sides = _sides;
            getMean();
            getMedian();
        }

        // public int[] medianPrep() {

        // }
        public float getMedian() {
            int intSides = (int)this.sides;
            float median;
            int middleIndex;
            int[] values = Enumerable.Range(1, (intSides)).ToArray();
            // gives an array of incrementing numbers from 1 to the number of sides of user's die. [1,2,3,4,5,6]
            if (values.Length % 2 == 0) {
                middleIndex = values.Length / 2;
                median = ((float)values[middleIndex - 1] + (float)values[middleIndex]) / 2;
                Console.WriteLine(median);
                return median;
            } else {
                middleIndex = values.Length / 2;
                median = values[middleIndex];
                return median;
            }

        }

        // public float getMode() {

        // }

        public float getMean() {
            /*Returns the average roll from the number of dice and sides provided
            mean - The calculated average from the user's dice*/
            float mean = ((this.sides + 1)/2) * this.diceNumber;
            Console.WriteLine(mean);
            return mean;
        }
    }

    class StatsPractice {
        public static void Main (){
            Dice newDie = new Dice(1, 8);
        }
    }
}