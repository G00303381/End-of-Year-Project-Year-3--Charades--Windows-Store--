using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingApplication
{
    class globalVar
    {
        public static string Player1;
        public static string Player2;
        public static string Player3;
        public static string Player4;
        public static string Player5;
        public static string Player6;
        public static string Player7;

        public static int NumOfPlayers;
        public static int NumOfPlayersThatDrew;
        public static int Player1Score;
        public static int Player2Score;
        public static int Player3Score;
        public static int Player4Score;
        public static int Player5Score;
        public static int Player6Score;
        public static int Player7Score;

        public static bool isAssigned;
        public static bool isDrawingDone;

        public static List<string> playerList = new List<string>();
        public static List<string> playerListOrdered = new List<string>();
        public static List<string> playerListBackup = new List<string>();

        public static List<string> Words = new List<string>
        {
            "Sun, Knife"
        };

        public static List<string> WordsBackup = new List<string>
        {
            "Sun, Knife"
        };

    }
}
