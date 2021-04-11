using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CLI_Temple
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "CLICraft 1.0.0";
            Console.WriteLine("Welcome to the CLICraft 1.0.0!");
            Console.WriteLine("Press any key to continue....");
            Console.ReadKey(true);
            drawBounds();
            menu();
        }
        const int w = 80;
        const int h = 30;
        public static void drawBounds()
        {
            try
            {
                Console.SetWindowSize(w + 2 + 30, h + 4);
            }
            catch { }
            Console.SetBufferSize(w + 2 + 30, h + 4);
            Console.BackgroundColor = ConsoleColor.DarkGray;
            for (int i = 0; i < h + 2; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write(" ");
                Console.SetCursorPosition(w + 1, i);
                Console.Write(" ");
                Console.SetCursorPosition(w + 30 + 1, i);
                Console.Write(" ");
                Console.SetCursorPosition(0, i);
                if (i == 0) Console.Write(new string(' ', w + 1 + 30));
                if (i == h + 1) Console.Write(new string(' ', w + 1 + 30));
            }

            Console.SetCursorPosition(3, h + 2);
            Console.BackgroundColor = ConsoleColor.Black;

        }
        public static void resetCursor()
        {
            Console.SetCursorPosition(3, h + 2);
        }
        public static void drawMenu(int option_sel)
        {
            clearScreen();
            Console.SetCursorPosition(10, 15);
            Console.Write("###MENU###");
            Console.SetCursorPosition(10, 16);
            Console.Write($"({(option_sel == 0 ? '*' : ' ')}) - New Game");
            Console.SetCursorPosition(10, 17);
            Console.Write($"({(option_sel == 1 ? '*' : ' ')}) - Load Game");
            Console.SetCursorPosition(10, 18);
            Console.Write($"({(option_sel == 2 ? '*' : ' ')}) - Tutorial");
            Console.SetCursorPosition(10, 19);
            Console.Write($"({(option_sel == 3 ? '*' : ' ')}) - Credits");
            Console.SetCursorPosition(10, 20);
            Console.Write($"({(option_sel == 4 ? '*' : ' ')}) - Exit Game");
            Console.SetCursorPosition(10, 21);
            Console.Write("Nawigate with WSAD keys and confirm with enter!");
        }

        public static void menu()
        {
            int option = 0;
            while (true)
            {
                drawMenu(option);
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.S)
                {
                    option++;
                }
                if (key.Key == ConsoleKey.W)
                {
                    option--;
                }
                if (key.Key == ConsoleKey.Enter)
                {
                    BeepAsync(800, 100);
                    switch (option)
                    {
                        case 0: newGame(); break;
                        case 1: loadGame(); break;
                        case 4: exit(); break;
                        case 2: tutorial(); break;
                        case 3: credits(); break;
                    }
                }
                if (option > 4) option = 4;
                if (option < 0) option = 0;
            }

        }

        private static void credits()
        {
            Console.Clear();
            Console.WriteLine("Game created by dommilosz / Milosz_123456#4766");
            Console.WriteLine("Nekuskus#1816");
            Console.WriteLine("micapl12#2237");
            Console.WriteLine("Helped/tested: Stack Overflow, my class");
            Console.WriteLine("ATOS!");
            Console.ReadKey(true);
        }

        private static void BeepAsync(int v1, int v2)
        {
            void beep()
            {
                Console.Beep(v1, v2);
            }

            Thread t = new Thread(beep);
            t.Start();
        }

        private static void newGame()
        {
            Console.Clear();
            Console.WriteLine("Enter your game name (used to save)");
            gameName = Console.ReadLine();
            Console.WriteLine("Enable sandbox? (Y/N)");
            var key = Console.ReadKey(true);
            if (gameName.Length < 1 || File.Exists(gameName)) return;
            timePassed = 0;
            timePassedBefore = 0;
            initGame();
            if (key.Key == ConsoleKey.Y)
            {
                foreach (var item in inventory)
                {
                    item.count = 9999;
                }
            }
            startGame();
        }

        private static void loadGame()
        {
            Console.Clear();
            Console.WriteLine("Detected saves:");
            foreach (var item in Directory.GetFiles("./"))
            {
                if (item.Contains(".clicsave"))
                {
                    Console.WriteLine(item);
                }
            }
            Console.WriteLine("Enter your game name");
            initGame();
            gameName = Console.ReadLine();
            if (gameName == "ATOS?")
            {
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.WriteLine("ATOOOOOOOS!");
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ReadKey(true);
            }if (gameName == "CYBERMANIA")
            {
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.WriteLine("▀▀▀▀▜▀▀▛▜▜▀▛▜▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▜▀▛▀▀▀▜▜▜▜▜▜▜▜▜▜█");
                Console.WriteLine(" ▝▝▞▗▐▐▐▗▚▜▐▗▚▀▞▞▐▝▞▐▝▞▞▘▀▞▚▘▌▌▛▞▘▌▞▗▚▜▐▀▛▛▚ ");
                Console.WriteLine("▝▞▘▚▌▌▌▟▞▙▚▚▚▝▖▖▝▖▘▝▖▘▖▖▘▚▝▖▚▚▐▐▐▗▚▐▝▖▚▚▀▄▞▞▖");
                Console.WriteLine("▘▖▝▖▚▝▟▖▛▟▐▝▖▚▗▝▖▖▘▘▖▘▖▖▘▚▝▝▞▐▐▐▐▞▞▗▚▐▝▞▞▗▝▟▟");
                Console.WriteLine("▖▀▌▛▛▌▄▞█▐▐▟▟▄▄▙▖▄▚▙▄▙▞▛██▜▚▛▞▌▛▛█▙▗▖▚▚▞▞▄▜▞█");
                Console.WriteLine("▞▄▐▀▛▛▟▟▛▛▛▞▞▞▚▚▀▀▘▞▛▌▚▘▘▖▝▗▝▐▐▐▟▟▛▖▄▚▙▜▜▞▙▜▟");
                Console.WriteLine("▞▞▟▜▟▜▙▚▛▜▝▞▗▗▗ ▚▚▀▝▖▞ ▝▖▞▖ ▗▗▗▚▗▜▌▘▚▚▜▀▘▘▌▌▛");
                Console.WriteLine("▄▀▙▛▛█▟▞▛▞▞▞▄▄ ▚▘▚▘▌▞▖▞ ▞▞▐▝▖▖▚▗▚▟▙▀ ▘▘▖▘▖▚▐▄");
                Console.WriteLine("▖▛▟▜▛▙▙▛▙▚▀▚▘▖▘▚▐▗▘▖▞▄▖▌▖▞▗▝▗▘▚▚▘▙▜▞▗▘▄▄▀▀▙▙█");
                Console.WriteLine("▞▟▟▙█▛██▞▞▐▝▝▖▌▙▜▗▗▘▖▖▀▞▄▟▄▞▄▞▖▚▀▞▞▞▌▚▗▗▚▖▙▌█");
                Console.WriteLine("▛▞▟▟▙█▙▟▐▜▀▛▜▝▞▞▞▖▖▞▝▞▝▞▗▗ ▚▗▝▝ ▚▐▐▐▝▖▚▛▛▛▚▛▀");
                Console.WriteLine("▌▛▟▟▟▙▛█▝▖▚▝▗▝▗▚▚  ▝▞▝▞▗▗ ▘▖▖▖▘▘▚▝▞▖▚▝▞▞▙▜▙▛█");
                Console.WriteLine("▛▜▚▙▙▛█▜▚▝▗▗ ▝▗▚▐▝▞▝▖▌▖▌▖▝   ▗▝▝▖▚▚▘▘▘▖█▞▌▞▛█");
                Console.WriteLine("▟▀▞▟▙█▜▛▌▚▘ ▝▝▖▚▘▚▝▝▗▝▖▖▞▝▝▝▝ ▝▝▐▐▐▐▝▖▖▘▌▛▐▝▀");
                Console.WriteLine("▐▛▄▙█▜▜▜▞▖▞▝▝▞▝▖▚▗▝▝▗▗▗▗▗▘▝▖▝▝▝▞▖▚▚▘▚▚▐▐▐▀▌▛▜");
                Console.WriteLine("▙▛█▙█▛▛▛▌▚ ▚▚▝▞▗▘ ▖▘  ▗▗▗▝▖▝▝▖▘▖▚▘▌▘▚▘▐▐▗▚▚▀█");
                Console.WriteLine("▛▟▞▞▞▚▘▌█▝▖▚▝▞▝▞▝▞▝▗ ▘▖▘▖▖▞▗▗▝▗▘▞▐▐▞█▞▞▛▞▞▞▖█");
                Console.WriteLine("█▟▙▙▙███▟▝▝ ▝ ▘▝  ▘ ▘▘ ▘   ▘ ▝ ▘▝▝▟▞▝█▟████▟█");
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ReadKey(true);
            }
            if (!File.Exists(gameName+ ".clicsave"))
            {
                Console.WriteLine("Game do not exist!");
                Console.ReadKey(true);
                return;
            }
            var lines = File.ReadAllLines(gameName+ ".clicsave");
            var inv = lines[0].Split(';');
            foreach (var i in inv)
            {
                var itemData = i.Split('#');
                if (itemData.Length >= 3)
                {
                    item.getItemByName(itemData[0], inventory).count = Convert.ToInt32(itemData[1]);
                    item.getItemByName(itemData[0], inventory).level = Convert.ToInt32(itemData[2]);
                }
            }
            var levelData = lines[1].Split(';');
            for (int i = 0; i < levelData.Length; i++)
            {
                if (i < level.Length)
                    level[i] = levelData[i];
            }
            timePassed = 0;
            if (lines.Length > 2)
                timePassedBefore = Convert.ToInt64(lines[2]);
            startGame();
        }
        static long timePassed;
        private static void startGame()
        {
            gameLoop();
        }

        public static void explodeTnt(int x,int y,int power=5)
        {
            if (level[y][x] != 'O')
                writeToLevel(x, y, ' ');

            if(x>=0&&y>=0&&x<w&&y<h)
            Console.SetCursorPosition(x + 1, y + 1);
            ConsoleWrite(" ", ConsoleColor.Red);
            if (power <= 0) return;
            explodeTnt(x + 1, y, power - 1);
            explodeTnt(x - 1, y, power - 1);
            explodeTnt(x , y+1, power - 1);
            explodeTnt(x , y-1, power - 1);
        }
        public static void initExplodeTnt(int x, int y)
        {
            tntQueue = true;
            tntQueueXY = new int[] { x, y };
        }

        static string[] level = new string[h];
        static Random rnd = new Random();
        public static void createEmptyPlane()
        {
            level = new string[h];
            for (int i = 0; i < level.Length; i++)
            {
                level[i] = new string(' ', w);
            }

            for (int i = 0; i < level.Length; i++)
            {
                for (int i2 = 0; i2 < rnd.Next(0, 2); i2++)
                {
                    generateBlob('S', rnd.Next(0, w + 1), i, 1, 3);
                }
                for (int i2 = 0; i2 < rnd.Next(0, 2); i2++)
                {
                    generateBlob('W', rnd.Next(0, w + 1), i, 1, 3);
                }
                for (int i2 = 0; i2 < rnd.Next(0, 2); i2++)
                {
                    generateBlob('L', rnd.Next(0, w + 1), i, 1, 3);
                }
                for (int i2 = 0; i2 < rnd.Next(0, 2); i2++)
                {
                    generateBlob('I', rnd.Next(0, w + 1), i, 1, 3);
                }
                for (int i2 = 0; i2 < rnd.Next(0, 2); i2++)
                {
                    generateBlob('C', rnd.Next(0, w + 1), i, 1, 3);
                }
                for (int i2 = 0; i2 < rnd.Next(0, 2); i2++)
                {
                    generateBlob('D', rnd.Next(0, w + 1), i, 1, 3);
                }
            }
            writeToLevel(rnd.Next(0, w), rnd.Next(0, h), 'O');
            if (level[h / 2][w / 2] != ' ')
            {
                createEmptyPlane();
            }
        }

        public static int playerX = 0;
        public static int playerY = 0;
        public static int playerR = 0;
        public static bool isInGui()
        {
            if (wantCraft) return true;
            if (wantUse) return true;
            if (doingProgressBar) return true;
            return false;
        }
        public static void playerInputLoop()
        {
            while (true)
            {
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.W && !isInGui()) { playerR = 0; playerMove(); }
                if (key.Key == ConsoleKey.S && !isInGui()) { playerR = 1; playerMove(); }
                if (key.Key == ConsoleKey.A && !isInGui()) { playerR = 2; playerMove(); }
                if (key.Key == ConsoleKey.D && !isInGui()) { playerR = 3; playerMove(); }
                if (key.Key == ConsoleKey.Spacebar) mine();
                if (key.Key == ConsoleKey.C) initCrafting();
                if (key.Key == ConsoleKey.U) initUse();
                if (key.Key == ConsoleKey.F) useBlock();
                if (key.Key == ConsoleKey.B) placeBlock();
                if (key.Key == ConsoleKey.Escape) exitGameInGame();
                if (key.Key == ConsoleKey.S)
                {
                    if (recipe_c < (wantCraft ? recipes.Count : usables.Count) - 1)
                        recipe_c++;

                }
                if (key.Key == ConsoleKey.W)
                {
                    if (recipe_c > 0)
                        recipe_c--;
                }
                if (key.Key == ConsoleKey.Enter && wantCraft)
                {
                    recipes[recipe_c].craft(inventory);
                }
                if (key.Key == ConsoleKey.Enter && wantUse)
                {
                    usables[recipe_c].use();
                }
                if (popup)
                {
                    wantClosePopup = true;
                }
                if (wantExit)
                {
                    if (key.Key == ConsoleKey.Y) exitLoop = true;
                }
            }
        }

        private static void placeBlock()
        {
            if (getCharUnderPlayer() != ' ') return;
            if (currentBuildItem != null)
            {
                if (item.getItemByName(currentBuildItem.name, inventory).count > 0)
                {
                    writeToLevel(playerX - 1, playerY - 1, item.getItemByName(currentBuildItem.name, inventory).codechar);
                    item.getItemByName(currentBuildItem.name, inventory).count--;
                }
            }
        }

        public static bool wantExit = false;
        public static bool exitLoop = false;
        private static void exitGameInGame()
        {
            wantExit = true;
        }

        private static void useBlock()
        {
            if (getCharBeforePlayer() == '⇉' || getCharBeforePlayer() == '⇶')
            {
                saveGame();
                initPopup(ConsoleColor.DarkGreen, $"Game saved. Name: {gameName}", "GAME SAVED!");
            }

        }

        public static bool wantCraft = false;
        public static bool wantCraftExit = false;
        public static void initCrafting()
        {
            if (wantCraft)
            {
                wantCraftExit = true;
            }
            else
            {
                wantCraft = true;
            }

        }
        public static bool wantUse = false;
        public static bool wantUseExit = false;
        public static void initUse()
        {
            if (wantUse)
            {
                wantUseExit = true;
            }
            else
            {
                wantUse = true;
            }

        }
        public static item currentBuildItem = null;
        public static void useAction(item i)
        {
            if (item.getItemByName(i.name, inventory).count >= i.count)
            {
                if (i.name == "bed")
                {
                    if (getCharUnderPlayer() != ' ') return;
                    placeBed();
                }
                if (i.name == "tnt")
                {
                    initExplodeTnt(getCharXYUnderPlayer()[0], getCharXYUnderPlayer()[1]);
                }
                if (i.useDesc.Contains("build"))
                {
                    currentBuildItem = i;
                }
                item.getItemByName(i.name, inventory).count -= i.count;
                wantUseExit = true;
            }
            else
            {
                initPopup(ConsoleColor.Red, $"You don't have all of: 1x {i.name}", "Not enough items!");
            }
        }
        public static string gameName = "";
        private static void saveGame()
        {
            string str = "";
            foreach (var item in inventory)
            {
                str += $"{item.name}#{item.count}#{item.level};";
            }
            str += "\n";
            foreach (var item in level)
            {
                str += $"{item};";
            }
            str += "\n";
            str += timePassed.ToString();
            File.WriteAllText(gameName+".clicsave", str);
        }

        public static void placeBed()
        {
            writeToLevel(playerX - 1, playerY - 2, '⇉');
            writeToLevel(playerX - 1, playerY - 1, '⇶');
        }

        public static void drawPlane()
        {
            drawBounds();
            for (int i = 1; i < h + 1; i++)
            {
                Console.SetCursorPosition(1, i);
                Console.BackgroundColor = ConsoleColor.Green;
                Console.Write(new string(' ', w));
                Console.BackgroundColor = ConsoleColor.Black;
                for (int i2 = 0; i2 < level[i - 1].Length; i2++)
                {
                    if (level[i - 1][i2] != ' ')
                    {
                        Console.SetCursorPosition(i2 + 1, i);
                        ConsoleWrite(getCharTxt(level[i - 1][i2]).ToString(), (ConsoleColor)getCharColor(level[i - 1][i2]));
                    }
                }


            }
        }
        public static void initGame()
        {
            createEmptyPlane();
            inventory.Clear();
            recipes.Clear();
            usables.Clear();
            inventory.Add(new item("stone", 0, 'S'));
            inventory.Add(new item("wood", 0, 'W'));
            inventory.Add(new item("planks", 0));
            inventory.Add(new item("sticks", 0));
            inventory.Add(new item("iron_ore", 0, 'I'));
            inventory.Add(new item("iron", 0, 'I'));
            inventory.Add(new item("coal", 0, 'C'));
            inventory.Add(new item("bed", 0));
            inventory.Add(new item("diamond", 0, 'D'));
            inventory.Add(new item("tnt", 0, 'T'));
            inventory.Add(new item("obsidian", 0, 'O'));
            inventory.Add(new item("pickaxe", 0, 0));
            inventory.Add(new item("axe", 0, 0));

            recipes.Add(new recipe(new item[] { new item("wood", 1) }.ToList(), new item("planks", 4)));
            recipes.Add(new recipe(new item[] { new item("planks", 2) }.ToList(), new item("sticks", 4)));
            recipes.Add(new recipe(new item[] { new item("iron_ore", 1), new item("coal", 2) }.ToList(), new item("iron", 1)));
            recipes.Add(new recipe(new item[] { new item("iron", 4), new item("wood", 6) }.ToList(), new item("bed", 1)));
            recipes.Add(new recipe(new item[] { new item("iron", 8), new item("coal", 6) }.ToList(), new item("tnt", 1)));
            recipes.Add(new recipe(new item[] { new item("pickaxe", 0, 0), new item("sticks", 16), new item("wood", 6) }.ToList(), new item("pickaxe", 1, 1)));
            recipes.Add(new recipe(new item[] { new item("pickaxe", 1, 1), new item("sticks", 16), new item("stone", 6) }.ToList(), new item("pickaxe", 1, 2)));
            recipes.Add(new recipe(new item[] { new item("pickaxe", 1, 2), new item("sticks", 16), new item("iron", 6) }.ToList(), new item("pickaxe", 1, 3)));
            recipes.Add(new recipe(new item[] { new item("pickaxe", 1, 3), new item("sticks", 16), new item("diamond", 6) }.ToList(), new item("pickaxe", 1, 4)));
            recipes.Add(new recipe(new item[] { new item("axe", 0, 0), new item("sticks", 16), new item("wood", 6) }.ToList(), new item("axe", 1, 1)));
            recipes.Add(new recipe(new item[] { new item("axe", 1, 1), new item("sticks", 16), new item("stone", 6) }.ToList(), new item("axe", 1, 2)));
            recipes.Add(new recipe(new item[] { new item("axe", 1, 2), new item("sticks", 16), new item("iron", 6) }.ToList(), new item("axe", 1, 3)));

            var item = new item("bed", 1);
            usables.Add(item);
            item.useDesc = "Place and sleep to save the game!";
            
            item = new item("tnt", 1);
            usables.Add(item);
            item.useDesc = "Place and see how your world reacts!";

            item = new item("wood", 0);
            usables.Add(item);
            item.useDesc = "Use to select material to build with!";

            item = new item("stone", 0);
            usables.Add(item);
            item.useDesc = "Use to select material to build with!";

            item = new item("iron_ore", 0);
            usables.Add(item);
            item.useDesc = "Use to select material to build with!";

            item = new item("coal", 0);
            usables.Add(item);
            item.useDesc = "Use to select material to build with!";

            item = new item("planks", 0);
            usables.Add(item);
            item.useDesc = "Use to select material to build with!";

            item = new item("diamond", 0);
            usables.Add(item);
            item.useDesc = "Use to select material to build with!";
        }
        public static void tutorial()
        {
            Console.Clear();
            Console.WriteLine("Soooooo. It's my sandbox game: CLICraft!");
            Console.WriteLine("Simply walk with WSAD.");
            Console.WriteLine("If possible take bathes in lava!");
            Console.WriteLine("Your goal is to mine obsidian! You need to have at least 4 Lvl pickaxe!");
            Console.WriteLine("As I said it's sandbox do what you want!");
            Console.WriteLine("You can craft with C, use items wit U and use blocks with F!");
            Console.WriteLine("You can place blocks and create amazing builds! Click U select your material and build with B!");
            Console.WriteLine("Mine with space!");
            Console.WriteLine("Psssst tnt exists!");
            Console.WriteLine("Don't try mining stone with bare hand!!");
            Console.WriteLine();
            Console.WriteLine("Materials:");
            Console.BackgroundColor = ConsoleColor.Red;
            Console.Write("     ");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine("It's lava take bathes in it! Don't relax to much!");
            Console.BackgroundColor = ConsoleColor.DarkYellow;
            Console.Write("     ");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine("It's wood. Basic material");
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.Write("     ");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine("It's stone!");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write("     ");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine("It's coal! Yes it's black :D");
            Console.BackgroundColor = ConsoleColor.White;
            Console.Write("     ");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine("It's iron!");
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.Write("     ");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine("It's diamonds!");
            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.Write("     ");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine("It's THE OBSIDIAN! It's very hard!");

            Console.WriteLine();
            Console.WriteLine("To save the game place down a bed and use it with F!");
            Console.WriteLine("Press any key to continue....");
            Console.ReadKey(true);

        }
        static int msPassed = 0;
        static bool tntQueue = false;
        static int[] tntQueueXY = new int[2];
        private static void gameLoop()
        {
            sessionStart = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            popup = false;
            wantCraft = false;
            wantUse = false;
            clearScreen();

            playerX = w / 2;
            playerY = h / 2;


            Thread t = new Thread(playerInputLoop);
            Thread t2 = new Thread(checkTime);
            t.Start();
            t2.Start();

            while (!gameover && !exitLoop)
            {
                if (!doingProgressBar)
                {
                    drawPlane();
                    drawPlayer();
                    drawMaterials();
                    drawTime();

                    if (tntQueue)
                    {
                        explodeTnt(tntQueueXY[0], tntQueueXY[1]);
                        tntQueue = false;
                    }

                    Console.SetCursorPosition(3, h + 2);
                    Thread.Sleep(200);
                    if (popup)
                    {
                        showPopup(popup_c, popup_txt, popup_title);
                        popup = false;
                        wantExit = false;
                    }
                    if (progressbarquerry)
                    {
                        progressBar(progressbarsteps, false);
                        progressbarquerry = false;
                        if (miningObsidian)
                        {
                            Console.Beep(300, 200);
                            Console.Beep(600, 200);
                            Console.Beep(300, 200);
                            Console.Beep(800, 200);
                            initPopup(ConsoleColor.DarkBlue, $"Obsidian broken in {timePassed}s!", "GAME OVER!");
                            miningObsidian = false;
                        }
                    }
                    if (wantCraft)
                    {
                        wantCraftExit = false;
                        craftingMenu();
                        wantCraft = false;
                    }
                    if (wantUse)
                    {
                        wantUseExit = false;
                        useMenu();
                        wantUse = false;
                    }
                    if (wantExit)
                    {
                        initPopup(ConsoleColor.Red, "Do you want to exit? Make sure to sleep in bed to save state! Click Y/any other key", "EXIT?");

                    }

                }
            }


            if (gameover)
            {
                gameOver(gameover_txt);
                showPopup(popup_c, popup_txt, popup_title);
            }
            t.Abort();
            gameover = false;
            exitLoop = false;
            wantExit = false;
        }
        static long sessionStart = 0;
        static long timePassedBefore = 0;
        private static void checkTime()
        {
            while (!gameover && !exitLoop)
            {
                long ts = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                timePassed = (ts - sessionStart) / 1000 + timePassedBefore;

                Thread.Sleep(200);
            }
        }

        public static void drawTime()
        {
            Console.SetCursorPosition(0, h + 3);
            msPassed += 200;
            if (msPassed >= 1000) msPassed = 0;
            timePassed += 1;
            if (timePassed >= 0)
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write($"Time: {timePassed} secounds");
            }
        }

        static int recipe_c = 0;
        private static void craftingMenu()
        {
            recipe_c = 0;
            while (!popup)
            {
                Console.SetCursorPosition(0 + 2 + 1, 1);
                Console.Write("Crafting:");
                int yoffset = 1;
                int i = 0;
                foreach (var item in recipes)
                {
                    Console.SetCursorPosition(0 + 2 + 1, 1 + yoffset);
                    Console.Write(new string(' ', 75));
                    Console.SetCursorPosition(0 + 2 + 1, 1 + yoffset);

                    Console.Write((i == recipe_c ? "(*) " : "( ) ") + item.getRecipeInfo());
                    i++;
                    yoffset++;
                }
                if (wantCraftExit) break;
                yoffset++;
                Console.SetCursorPosition(2 + 1, 1 + yoffset);
                Console.Write("press c for exit!");

                drawMaterials();
                resetCursor();
                Thread.Sleep(50);
            }

        }

        private static void useMenu()
        {
            recipe_c = 0;
            while (!popup)
            {
                Console.SetCursorPosition(0 + 2 + 1, 1);
                Console.Write("Use:");
                int yoffset = 1;
                int i = 0;
                foreach (var item in usables)
                {
                    Console.SetCursorPosition(0 + 2 + 1, 1 + yoffset);
                    Console.Write(new string(' ', 75));
                    Console.SetCursorPosition(0 + 2 + 1, 1 + yoffset);

                    Console.Write((i == recipe_c ? "(*) " : "( ) ") + item.name + ": " + item.useDesc);
                    i++;
                    yoffset++;
                }
                if (wantUseExit) break;
                yoffset++;
                Console.SetCursorPosition(2 + 1, 1 + yoffset);
                Console.Write("press u for exit!");

                drawMaterials();
                resetCursor();
                Thread.Sleep(50);
            }

        }

        private static void drawMaterials()
        {
            Console.SetCursorPosition(w + 2 + 1, 1);
            Console.Write("Inventory:");
            int yoffset = 1;
            foreach (var item in inventory)
            {
                Console.SetCursorPosition(w + 2 + 1, 1 + yoffset);
                Console.Write(new string(' ', 15));
                Console.SetCursorPosition(w + 2 + 1, 1 + yoffset);
                yoffset++;
                ConsoleWrite(" ", getCharColor(item.codechar));
                if (item.level != -1)
                    Console.Write($"{item.name}: {item.count} - {item.level} lvl");
                else
                    Console.Write($"{item.name}: {item.count}");
            }
            yoffset++;
            Console.SetCursorPosition(w + 2 + 1, 1 + yoffset);
            if (currentBuildItem != null)
                Console.Write($"Building material: {currentBuildItem.name}");
            yoffset++;
            Console.SetCursorPosition(w + 2 + 1, 1 + yoffset);
            Console.Write("press c for crafting!");
            yoffset++;
            Console.SetCursorPosition(w + 2 + 1, 1 + yoffset);
            Console.Write("press u for use!");
        }
        static bool wantClosePopup = false;
        public static void initPopup(ConsoleColor c, string txt, string title)
        {
            if (!popup)
            {
                Console.Beep(600, 200);
            }
            popup = true;
            popup_txt = txt;
            popup_title = title;
            popup_c = c;
        }
        public static void showPopup(ConsoleColor c, string txt, string title)
        {
            popup = true;
            popup_txt = txt;
            popup_title = title;
            popup_c = c;
            int width = 45;
            wantClosePopup = false;

            Console.SetCursorPosition(10, h / 2 - 5);
            for (int i = h / 2 - 5; i < h / 2 + 5; i++)
            {
                Console.SetCursorPosition(10, i);
                Console.BackgroundColor = c;
                Console.Write(new string(' ', width + 2));
                if (i == h / 2 - 5)
                {
                    Console.SetCursorPosition(11, i);
                    Console.BackgroundColor = c;
                    Console.Write(new string(' ', (width - title.Length) / 2) + title);
                }
                Console.BackgroundColor = ConsoleColor.Black;
            }
            for (int i = h / 2 - 5; i < h / 2 + 5; i++)
            {
                if (i > h / 2 - 5 && i < h / 2 + 4)
                {
                    Console.SetCursorPosition(11, i);
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Write(new string(' ', width));
                    Console.BackgroundColor = ConsoleColor.Black;
                }
            }
            txt += "  Press any key to continue!";
            var results = txt.SplitBy(width);


            for (int i = 0; (i < results.Count()) && i < 5; i++)
            {
                string txt2 = results.ElementAt(i);
                Console.SetCursorPosition(11, h / 2 - 6 + i + 2);
                Console.Write(new string(' ', width));
                Console.SetCursorPosition(11, h / 2 - 6 + i + 2);
                Console.Write(txt2);
            }


            Console.BackgroundColor = ConsoleColor.Black;

            while (!wantClosePopup)
            {
                Thread.Sleep(250);
            }
            drawPlane();
            drawPlayer();
            drawMaterials();
        }
        static bool popup = false;
        static string popup_txt = "";
        static string popup_title = "";
        static ConsoleColor popup_c = ConsoleColor.Red;
        public class recipe
        {
            public List<item> inputs;
            public item output;
            public recipe(List<item> inp, item out1)
            {
                inputs = inp;
                output = out1;
            }
            public void craft(List<item> items)
            {
                foreach (var item in inputs)
                {
                    var providedItem = item.getItemByName(item.name, items);
                    if (providedItem.level != -1 && providedItem.level != item.level)
                    {
                        return;
                    }
                    if (providedItem.count < item.count)
                    {


                        initPopup(ConsoleColor.Red, $"You don't have all of: {getRecipeInfo()}", "Not enough items!");

                        return;
                    }
                }
                foreach (var item in inputs)
                {
                    item.getItemByName(item.name, items).count -= item.count;
                }
                item.getItemByName(output.name, items).count += output.count;
                item.getItemByName(output.name, items).level = output.level;
            }
            public string getRecipeInfo()
            {
                string txt = "";
                foreach (var item in inputs)
                {
                    txt += $"{item.name} x {item.count}:{item.level} ";
                }
                return $"{output.name} x {output.count}:{output.level} = {txt}";
            }
        }
        public class item
        {
            public string name = "";
            public int count = 0;
            public int level = 0;
            public string useDesc = "";
            internal char codechar;

            public item(string name, int count, char codechar)
            {
                this.name = name;
                this.count = count;
                this.level = -1;
                this.codechar = codechar;
            }
            public item(string name, int count)
            {
                this.name = name;
                this.count = count;
                this.level = -1;
            }

            public item(string name, int count, int level) : this(name, count)
            {
                this.level = level;
            }

            public static item getItemByName(string name, List<item> items)
            {
                foreach (var item in items)
                {
                    if (item.name == name)
                    {
                        return item;
                    }
                }
                return null;
            }
            public void use()
            {
                useAction(this);
            }
        }



        public static List<item> inventory = new List<item>();
        public static List<item> usables = new List<item>();
        public static List<recipe> recipes = new List<recipe>();
        static bool miningObsidian = false;
        public static void mine()
        {
            if (getCharBeforePlayer() == 'S')
            {
                if (item.getItemByName("pickaxe", inventory).level > 0)
                {
                    miningProgressBar(100 / item.getItemByName("pickaxe", inventory).level);
                    item.getItemByName("stone", inventory).count++;
                    writeToLevel(getCharXYBeforePlayer()[0], getCharXYBeforePlayer()[1], ' ');
                }
                else
                {
                    if (item.getItemByName("pickaxe", inventory).level <= 0)
                    {
                        gameOver("You hit stone too hard. You died from debloodization");
                    }
                    else
                        initPopup(ConsoleColor.Red, "Too weak pickaxe! Needed lvl 1min", "To weak tool!");

                }
            }
            if (getCharBeforePlayer() == 'W')
            {
                miningProgressBar(50 / (item.getItemByName("axe", inventory).level + 1));
                item.getItemByName("wood", inventory).count++;
                writeToLevel(getCharXYBeforePlayer()[0], getCharXYBeforePlayer()[1], ' ');
            }
            if (getCharBeforePlayer() == 'I')
            {
                if (item.getItemByName("pickaxe", inventory).level > 1)
                {
                    miningProgressBar(150 / item.getItemByName("pickaxe", inventory).level);
                    item.getItemByName("iron_ore", inventory).count++;
                    writeToLevel(getCharXYBeforePlayer()[0], getCharXYBeforePlayer()[1], ' ');
                }
                else
                {
                    if (item.getItemByName("pickaxe", inventory).level <= 0)
                    {
                        gameOver("You hit stone too hard. You died from debloodization");
                    }
                    else
                        initPopup(ConsoleColor.Red, "Too weak pickaxe! Needed lvl 2min", "To weak tool!");
                }

            }
            if (getCharBeforePlayer() == 'C')
            {
                if (item.getItemByName("pickaxe", inventory).level > 1)
                {
                    miningProgressBar(120 / item.getItemByName("pickaxe", inventory).level);
                    item.getItemByName("coal", inventory).count++;
                    writeToLevel(getCharXYBeforePlayer()[0], getCharXYBeforePlayer()[1], ' ');
                }
                else
                {
                    if (item.getItemByName("pickaxe", inventory).level <= 0)
                    {
                        gameOver("You hit stone too hard. You died from debloodization");
                    }
                    else
                        initPopup(ConsoleColor.Red, "Too weak pickaxe! Needed lvl 2min", "To weak tool!");
                }

            }
            if (getCharBeforePlayer() == 'D')
            {
                if (item.getItemByName("pickaxe", inventory).level > 2)
                {
                    miningProgressBar(150 / item.getItemByName("diamond", inventory).level);
                    item.getItemByName("diamond", inventory).count++;
                    writeToLevel(getCharXYBeforePlayer()[0], getCharXYBeforePlayer()[1], ' ');
                }
                else
                {
                    if (item.getItemByName("pickaxe", inventory).level <= 0)
                    {
                        gameOver("You hit stone too hard. You died from debloodization");
                    }
                    else
                        initPopup(ConsoleColor.Red, "Too weak pickaxe! Needed lvl 3min", "To weak tool!");
                }

            }
            if (getCharBeforePlayer() == 'O')
            {
                if (item.getItemByName("pickaxe", inventory).level > 3)
                {
                    miningObsidian = true;
                    miningProgressBar(1000 / item.getItemByName("obsidian", inventory).level);
                    item.getItemByName("obsidian", inventory).count++;
                    writeToLevel(getCharXYBeforePlayer()[0], getCharXYBeforePlayer()[1], ' ');


                }
                else
                {
                    if (item.getItemByName("pickaxe", inventory).level < 4)
                    {
                        gameOver("You hit obsidian softly but it broke phisics law and responded with 10000x energy. You died from deatominisation");
                    }
                    else
                        initPopup(ConsoleColor.Red, "Too weak pickaxe! Needed lvl 4min", "To weak tool!");
                }

            }
            if (getCharBeforePlayer() == '⇉')
            {
                miningProgressBar(40);
                item.getItemByName("bed", inventory).count++;
                writeToLevel(getCharXYBeforePlayer()[0], getCharXYBeforePlayer()[1], ' ');
                writeToLevel(getCharXYBeforePlayer()[0], getCharXYBeforePlayer()[1] + 1, ' ');
            }
            if (getCharBeforePlayer() == '⇶')
            {
                miningProgressBar(40);
                item.getItemByName("bed", inventory).count++;
                writeToLevel(getCharXYBeforePlayer()[0], getCharXYBeforePlayer()[1], ' ');
                writeToLevel(getCharXYBeforePlayer()[0], getCharXYBeforePlayer()[1] - 1, ' ');
            }
        }

        static bool progressbarquerry = false;
        static int progressbarsteps = 0;
        public static void miningProgressBar(int steps)
        {
            progressbarsteps = steps;
            progressbarquerry = true;
        }

        static char getCharBeforePlayer()
        {
            return level[getCharXYBeforePlayer()[1]][getCharXYBeforePlayer()[0]];
        }
        static int[] getCharXYBeforePlayer()
        {
            int[] under = getCharXYUnderPlayer();
            if (playerR == 0)
            {
                under[1] = under[1] - 1;
            }
            if (playerR == 1)
            {
                under[1] = under[1] + 1;
            }
            if (playerR == 2)
            {
                under[0] = under[0] - 1;
            }
            if (playerR == 3)
            {
                under[0] = under[0] + 1;
            }
            return under;
        }
        public static void playerMove()
        {
            Console.Beep(400, 100);
            int prevX = playerX;
            int prevY = playerY;
            if (playerR == 0)
            {
                if (playerY > 1)
                {
                    playerY--;
                }
            }
            if (playerR == 1)
            {
                if (playerY < h)
                {
                    playerY++;
                }

            }
            if (playerR == 2)
            {
                if (playerX > 1)
                {
                    playerX--;
                }
            }
            if (playerR == 3)
            {
                if (playerX < w)
                {
                    playerX++;
                }
            }

            if (getCharUnderPlayer() == 'L')
            {
                gameOver("You have just taken hot lava bath and relaxed to that extent so you forgot you are playing a game!");
            }
            if (getCharUnderPlayer() != ' ')
            {
                playerY = prevY;
                playerX = prevX;
            }
        }

        static bool gameover = false;
        static string gameover_txt = "";
        private static void gameOver(string txt)
        {
            initPopup(ConsoleColor.Red, txt, "GAME OVER!");
            if (!gameover)
            {
                Console.Beep(400, 200);
                Console.Beep(300, 200);
                Console.Beep(200, 200);
            }
            gameover = true;
            gameover_txt = txt;

        }

        public static void drawPlayer()
        {
            Console.SetCursorPosition(playerX, playerY);
            ConsoleWrite(" ", ConsoleColor.White, ConsoleColor.White);

            if (playerR == 0)
            {
                Console.SetCursorPosition(playerX, playerY - 1);
                var c = getCharUnderPlayerOffset(0, -1);
                var color = getCharColor(c);
                Console.BackgroundColor = (ConsoleColor)color;
                Console.Write("↑");
            }
            if (playerR == 1)
            {
                Console.SetCursorPosition(playerX, playerY + 1);
                var c = getCharUnderPlayerOffset(0, 1);
                var color = getCharColor(c);
                Console.BackgroundColor = (ConsoleColor)color;
                Console.Write("↓");
            }
            if (playerR == 2)
            {
                Console.SetCursorPosition(playerX - 1, playerY);
                var c = getCharUnderPlayerOffset(-1, 0);
                var color = getCharColor(c);
                Console.BackgroundColor = (ConsoleColor)color;
                Console.Write("←");
            }
            if (playerR == 3)
            {
                Console.SetCursorPosition(playerX + 1, playerY);
                var c = getCharUnderPlayerOffset(1, 0);
                var color = getCharColor(c);
                Console.BackgroundColor = (ConsoleColor)color;
                Console.Write("→");
            }
            Console.BackgroundColor = ConsoleColor.Black;
        }

        private static ConsoleColor getCharColor(char c)
        {
            if (c == '#') return ConsoleColor.DarkBlue;
            if (c == '&') return ConsoleColor.Red;
            if (c == 'S') return ConsoleColor.Gray;
            if (c == 'W') return ConsoleColor.DarkYellow;
            if (c == 'L') return ConsoleColor.Red;
            if (c == 'I') return ConsoleColor.White;
            if (c == 'C') return ConsoleColor.Black;
            if (c == 'D') return ConsoleColor.Cyan;
            if (c == '⇉') return ConsoleColor.Gray;
            if (c == '⇶') return ConsoleColor.Red;

            int iter = rnd.Next(0, 2);
            if (c == 'O' && iter == 0) return ConsoleColor.DarkMagenta;
            if (c == 'O' && iter == 1) return ConsoleColor.White;

            return ConsoleColor.Green;
        }
        private static char getCharTxt(char c)
        {
            if (c == '⇉') return '#';
            if (c == '⇶') return '#';

            return ' ';
        }

        public static void ConsoleWrite(string txt, ConsoleColor fg, ConsoleColor bg)
        {
            Console.ForegroundColor = fg;
            Console.BackgroundColor = bg;
            Console.Write(txt);
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }
        public static void ConsoleWrite(string txt, ConsoleColor bg)
        {
            Console.BackgroundColor = bg;
            Console.Write(txt);
            Console.BackgroundColor = ConsoleColor.Black;
        }

        public static char getCharUnderPlayer()
        {
            return level[playerY - 1][playerX - 1];
        }
        public static int[] getCharXYUnderPlayer()
        {
            return new int[] { playerX - 1, playerY - 1 };
        }
        public static char getCharUnderPlayerOffset(int y, int x)
        {
            try
            {
                return level[playerY - 1 + x][playerX - 1 + y];
            }
            catch
            {
                return ' ';
            }
        }

        private static void exit()
        {
            Console.Clear();
            Console.WriteLine("Are you sure you want exit game? (Y/N)");
            if (Console.ReadKey().Key == ConsoleKey.Y)
            {
                Environment.Exit(0);
            }
            else
            {
                menu();
            }
        }

        public static void clearScreen()
        {
            Console.Clear();
            drawBounds();
        }

        public static void generateBlob(char c, int x, int y, int minSize, int maxSize)
        {
            int size = rnd.Next(minSize, maxSize + 1);
            int actSize = size;

            for (int i = 0; i < size; i++)
            {
                for (int i2 = 0; i2 < size - i; i2++)
                {
                    writeToLevel((x - size / 2) + i2, (y - size / 2) + i, c);
                    writeToLevel((x - size / 2) - i2, (y - size / 2) + i, c);
                    writeToLevel((x - size / 2) + i2, (y - size / 2) - i, c);
                    writeToLevel((x - size / 2) - i2, (y - size / 2) - i, c);
                }
            }

            //for (int i = -size/2; i < size/2; i++)
            //{
            //    for (int i2 = -actSize / 2; i2 < actSize / 2; i2++)
            //    {
            //        writeToLevel(x+i2, y + i, c);
            //    }
            //    if (actSize > 0) actSize--;
            //}
        }

        public static void writeToLevel(int x, int y, char c)
        {
            if (y >= 0 && y < level.Length && x >= 0 && x < w)
            {
                char[] str = level[y].ToCharArray();
                str[x] = c;
                level[y] = new string(str);
            }
        }

        static bool doingProgressBar = false;
        public static void progressBar(int steps = w, bool clear = true)
        {
            doingProgressBar = true;
            if (clear) clearScreen();

            int progress = 2;
            while (progress < w - 1)
            {
                Console.Beep(150, 20);
                Console.SetCursorPosition(1, h / 2);
                Console.Write(new string('#', w - 2));
                Console.SetCursorPosition(1, (h / 2) + 1);
                Console.Write("[");
                Console.SetCursorPosition(w - 1, (h / 2) + 1);
                Console.Write("]");
                Console.SetCursorPosition(1, (h / 2) + 2);
                Console.Write(new string('#', w - 2));
                Console.SetCursorPosition(progress, (h / 2) + 1);
                Console.BackgroundColor = ConsoleColor.White;
                Console.Write(" ");
                Console.BackgroundColor = ConsoleColor.Black;
                progress++;
            }
            doingProgressBar = false;
            Thread.Sleep(200);
            Console.Beep(600, 200);
        }
    }
    public static class EnumerableEx
    {
        public static IEnumerable<string> SplitBy(this string str, int chunkLength)
        {
            if (String.IsNullOrEmpty(str)) throw new ArgumentException();
            if (chunkLength < 1) throw new ArgumentException();

            for (int i = 0; i < str.Length; i += chunkLength)
            {
                if (chunkLength + i > str.Length)
                    chunkLength = str.Length - i;

                yield return str.Substring(i, chunkLength);
            }
        }
    }
}
