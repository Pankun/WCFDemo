using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SongDAO
{
    public class DAOUtl
    {
        static readonly Random random = new Random();
        public string Escape(ref string sz)
        {
            string functionReturnValue = null;
            if (sz != null)
            {
                functionReturnValue = sz.Replace("\"", "\"\"");
            }
            else
            {
                functionReturnValue = sz;
            }
            return functionReturnValue;
        }

        public string Gen_Key(int digits)
        {
            //Create and define array:
            string output = null;
            string num = null;
            string[] char_array = new string[51];

            char_array[0] = "0";
            char_array[1] = "1";
            char_array[2] = "2";
            char_array[3] = "3";
            char_array[4] = "4";
            char_array[5] = "5";
            char_array[6] = "6";
            char_array[7] = "7";
            char_array[8] = "8";
            char_array[9] = "9";
            char_array[10] = "A";
            char_array[11] = "B";
            char_array[12] = "C";
            char_array[13] = "D";
            char_array[14] = "E";
            char_array[15] = "F";
            char_array[16] = "G";
            char_array[17] = "H";
            char_array[18] = "I";
            char_array[19] = "J";
            char_array[20] = "K";
            char_array[21] = "L";
            char_array[22] = "M";
            char_array[23] = "N";
            char_array[24] = "O";
            char_array[25] = "P";
            char_array[26] = "Q";
            char_array[27] = "R";
            char_array[28] = "S";
            char_array[29] = "T";
            char_array[30] = "U";
            char_array[31] = "V";
            char_array[32] = "W";
            char_array[33] = "X";
            char_array[34] = "Y";
            char_array[35] = "Z";

           
            output = "";
            //Loop through and create the output based on the the variable passed to the function for the length of the key.
            while (output.Length < digits)
            {
                num = char_array[random.Next(0, 36)];
                output = output + num;
            }
            //Set return
            return output;
        }
    }
}
