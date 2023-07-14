using System;

namespace MorsePractice {

    public class Morse {

        morseStruct[] morseCode;
        public Morse() {
            this.morseCode = new morseStruct[37];
            this.morseCode[0] =  new morseStruct('a', " .-");
            this.morseCode[1] =  new morseStruct('b', " -...");
            this.morseCode[2] =  new morseStruct('c', " -.-.");
            this.morseCode[3] =  new morseStruct('d', " -..");
            this.morseCode[4] =  new morseStruct('e', " .");
            this.morseCode[5] =  new morseStruct('f', " ..-.");
            this.morseCode[6] =  new morseStruct('g', " --.");
            this.morseCode[7] =  new morseStruct('h', " ....");
            this.morseCode[8] =  new morseStruct('i', " ..");
            this.morseCode[9] =  new morseStruct('j', " .---");
            this.morseCode[10] = new morseStruct('k', " -.-");
            this.morseCode[11] = new morseStruct('l', " .-..");
            this.morseCode[12] = new morseStruct('m', " --");
            this.morseCode[13] = new morseStruct('n', " -.");
            this.morseCode[14] = new morseStruct('o', " ---");
            this.morseCode[15] = new morseStruct('p', " .--.");
            this.morseCode[16] = new morseStruct('q', " --.-");
            this.morseCode[17] = new morseStruct('r', " .-.");
            this.morseCode[18] = new morseStruct('s', " ...");
            this.morseCode[19] = new morseStruct('t', " -");
            this.morseCode[20] = new morseStruct('u', " ..-");
            this.morseCode[21] = new morseStruct('v', " ...-");
            this.morseCode[22] = new morseStruct('w', " .--");
            this.morseCode[23] = new morseStruct('x', " -..-");
            this.morseCode[24] = new morseStruct('y', " -.--");
            this.morseCode[25] = new morseStruct('z', " --..");
            this.morseCode[26] = new morseStruct(' ', "       ");
            this.morseCode[27] = new morseStruct('1', " .----");
            this.morseCode[28] = new morseStruct('2', " ..---");
            this.morseCode[29] = new morseStruct('3', " ...--");
            this.morseCode[30] = new morseStruct('4', " ....-");
            this.morseCode[31] = new morseStruct('5', " .....");
            this.morseCode[32] = new morseStruct('6', " -....");
            this.morseCode[33] = new morseStruct('7', " --...");
            this.morseCode[34] = new morseStruct('8', " ---..");
            this.morseCode[35] = new morseStruct('9', " ----.");
            this.morseCode[36] = new morseStruct('0', " -----");

        }

        struct morseStruct {
            public char eng;
            public string morse;

            public morseStruct(char _eng, string _morse) {
                this.eng = _eng;
                this.morse = _morse;
            }
        };
        // public IDictionary<char, string> morseCode = new Dictionary<char, string>(){
        //     {'a', ".-"}, {'b', "-..."}, {'c', "-.-."}, {'d', "-.."}, {'e', "."},
        //     {'f', "..-."}, {'g', "--."}, {'h', "...."}, {'i', ".."}, {'j', ".---"},
        //     {'k', "-.-"}, {'l', ".-.."}, {'m', "--"}, {'n', "-."}, {'o', "---"},
        //     {'p', ".--."}, {'q', "--.-"}, {'r', ".-."}, {'s', "..."}, {'t', "-"},
        //     {'u', "..-"}, {'v', "...-"}, {'w', ".--"}, {'x', "-..-"}, {'y', "-.--"},
        //     {'z', "--.."}, {' ', ""},

        //     {'1', ".----"}, {'2', "..---"}, {'3', "...--"}, {'4', "....-"}, {'5', "....."},
        //     {'6', "-...."}, {'7', "--..."}, {'8', "---.."}, {'9', "----."}, {'0', "-----"},
        // };

        public string engConvert(string str){ 
            string translation = "";
            for (int i = 0; i < str.Length; i++) {
                
                // translation += this.morseCode.eng[str[i]];
                translation += this.charToMorse(str[i]);
            }
            return translation;
        }

        public string morseConvert(string str) {
            str += " ";
            string translation = "";
            string subTranslation = " ";
            int i = 1;
            while (i < str.Length){
                subTranslation = " ";
                while (str[i] != ' '){
                    subTranslation += str[i];
                    i++;
                }
                i++;
                translation += this.morseToChar(subTranslation);
            }
            
            return cleanUp(translation);
        }

        public string charToMorse(char engChar) {
            for (int i = 0; i < 36; i++) {
                if (engChar == this.morseCode[i].eng) {
                    return this.morseCode[i].morse;
                }
            }
            return "";
            
        }

        public char morseToChar(string morseStr) {
            for (int i = 0; i < 36; i++){
                if (morseStr == this.morseCode[i].morse) {
                    return this.morseCode[i].eng;
                }
            }
            return '@';
        }

        public string cleanUp(string str) {
            for (int i = 0; i < str.Length; i++) {
                if (str[i] == '@'){
                    if (str[i..(i+7)] == "@@@@@@@") {
                        str = str[..i] + " " + str[(i+7)..];
                }
                }
            }
            return str;
        }

        public string languageSelect(string str) {
            if ((str[..2] == " -") ||(str[..2] == " .")) {
                return this.morseConvert(str);
            }
            return this.engConvert(str);
        }
    }

    public static class MorsePractice {

        public static void Main(){
            Morse morse = new Morse();
            Console.WriteLine(morse.languageSelect(" .... --- .-.. -.--        -.-. .... . . --.. .. - ...        .-- .... .- -        -.. ---        .-- .        -.. ---"));
            // Console.WriteLine(morse.morseConvert(" --. .-. . . - .. -. --. ...        --. .. .-. .-..        .- -. -..        .-- . .-.. -.-. --- -- .        - ---        -- -.--        .-- --- .-. .-.. -.."));
            // int i = 0;
            // foreach (var key in morse.morseCode.Keys) {
            //     Console.WriteLine($"this.morseCode[{i}] = new morseStruct('{key}', "{morse.morseCode[key]}");");
            //     i++;
            // }
        }
    }
}
