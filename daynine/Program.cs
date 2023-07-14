using System;

namespace ReverseInt {
    
    public class Reverse {

        public static int Flip(int num) {
            char holdPos;
            bool negativeFlag = false;
            string maxInt = "2147483647";
            int k = 0;

            if (num < Int32.MinValue || num > Int32.MaxValue) {
                return 0;
            }

            if (num < 0) {
                negativeFlag = true;
                num = num * -1;
            }

            char[] outarry = Array.ConvertAll(num.ToString().ToArray(), x=>x);

            for (int j = 0; j < outarry.Length / 2; j++) {
                holdPos = outarry[outarry.Length - j - 1];
                outarry[outarry.Length - j - 1] = outarry[j];
                outarry[j] = holdPos;

                // Console.WriteLine(holdPos);
            }

            string str = new string(outarry);
            Console.WriteLine(str);
            if (str.Length == 10) {
                while (!(str[k] < maxInt[k])) {

                if (str[k] == maxInt[k]) {
                k++;
                }
                if (str[k] > maxInt[k]) {
                    Console.Beep();
                    return 0;
                }
                
            }
            }

            
            
            if (str.Length > 10) {
                return 0;
            }            

            if (negativeFlag) 
            return -1 * Convert.ToInt32(str);
            return Convert.ToInt32(str);
        }


        public static void Main() {
            Console.WriteLine(Flip(-2147483412));
        }
    }
}