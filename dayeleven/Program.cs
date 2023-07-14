using System;

namespace ConwayPractice {

    class Creature {
        int state;
        int age;
        int nextState;

        public Creature(int _state = 0, int _age = 0) {
            /*Creature class constructor, takes in state and age from outside, default values of 0.
            _state - int of 0 or 1 to denote if Creature is alive
            _age - int of phases survived to denote age*/
            this.state = _state;
            this.age = _age;
            this.nextState = 0;
        }

        public void setState(int _state) {
            /*Sets the current creature's state to that given to us
            _state - int of 0 or 1 to denote if Creature is alive*/
            this.state = _state;
        }

        public void setAge(int _age) {
            /*Sets the current creature's age to that given to us
            _age - int of phases survived to denote age*/
            this.age = _age;
        }

        public int getState() {
            /*Returns an int of the current creature's current state*/
            return this.state;
        }

        public int getAge() {
            /*Returns an int of the current creature's current age*/
            return this.age;
        }

        public void liveCheck(int numNeighbors, int maxNeighbors, int minNeighbors) {
            /*If the current creature's number of neighbors is within the allowed range of survival, the creature will survive into the next phase
            numNeighbors - Number of creatures neighbors surrounding it. The creature counts itself as a neighbor
            maxNeighbors - The maximum number of neighbors that can surround a creature (9).
            minNeighbors - The minimum number of neighbors that can surround a creature (0).*/
            if ((minNeighbors < numNeighbors) && (numNeighbors < maxNeighbors)) {
                this.nextState = 1;
            }
        }
        
        public void updateState(){
            /*Takes the creature's next state and sets it equal to it's current state. If the creature was already alive, add 1 to it's age.*/
            this.state = this.nextState;
            this.nextState = 0;
            if (this.state > 0) {
                this.age++;
            } else {
                this.age = 0;
            }
        }


    }

    class World {
        public Creature[,] grid;
        int sizeX;
        int sizeY;

        public World (int _sizeX, int _sizeY) {
            /*World constructor. Takes in specified dimensions from user and creates a grid of creatures based on dimensions, randomly populating it.
            _sizeX - The user submitted size of the grid's x dimension
            _sizeY - The user submitted size of the grid's y dimension
            grid - a grid of creature objects based on user dimensions*/
            this.sizeX = _sizeX;
            this.sizeY = _sizeY;
            this.grid = new Creature[_sizeX,_sizeY];
            this.randPopulate();
        }

        public World (ExtConvert Hugo) {
            /*World constructor when a text file is used to input a specific map of creatures. Takes in dimensions of user's grid and creates a grid of
            creatures and populates according to text file configuration.
            
            Hugo - Data taken in from text file conversion.*/
            this.sizeX = Hugo.inputX;
            this.sizeY = Hugo.inputY;
            this.grid = new Creature[Hugo.inputX,Hugo.inputY];
            this.setPopulate(Hugo.getArray());
        }

        public void randPopulate() {
            /*Iterates through grid of creatures, randomly setting the state of each as either 0 or 1.*/
            Random rand = new Random();
            for (int i = 0; i < this.sizeX; i++) {
                for (int j = 0; j < this.sizeY; j++) {
                    this.grid[i,j] = new Creature(rand.Next(0,2));
                }
            }
        }

        public void setPopulate(int[] boardArray) {
            /*Iterates through grid of creatures, setting the state of each as the corresponding state given in boardArray
            boardArray - An array of creature states in order given by the user's text file*/
            for (int i = 0; i < this.sizeY; i++) {
                for (int j = 0; j < this.sizeX; j++) {
                    this.grid[j,i] = new Creature(boardArray[(j) + (i * this.sizeX)]);
                }
            }
        }
        public int coordState(int x, int y){
            /*Returns a specified creature's state based upon coordinates
            x - Desired creature's x coordinate
            y - Desired creature's y coordinate
            */
            x = ((((x % this.sizeX) + this.sizeX) % this.sizeX));
            y = ((((y % this.sizeY) + this.sizeY) % this.sizeY));
            return this.grid[x, y].getState();
        }

        public int coordAge(int x, int y){
            /*Returns a specified creature's age based upon coordinates
            x - Desired creature's x coordinate
            y - Desired creature's y coordinate
            */
            x = ((((x % this.sizeX) + this.sizeX) % this.sizeX));
            y = ((((y % this.sizeY) + this.sizeY) % this.sizeY));
            return this.grid[x, y].getAge();
        }

        public void showMap(){
            /*Iterates through the grid displaying the corresponding creature's state to the console. Once the end of the x dimension is reached, 
            a new console line is created for the next y level.*/
            for (int i = 0; i < this.sizeY; i++){
                for (int j = 0; j < this.sizeX; j++){
                    Console.Write($"{this.coordState(j, i)} ");
                }
                Console.WriteLine();
            }
        }

        public void nextStep(int _maxNeighbors, int _minNeighbors) {
            /*Iterates through the grid and determines each creature's next state based upon current surrounding neighbors
            _maxNeighbors - The maximum number of neighbors that can surround a creature (9).
            _minNeighbors - The minimum number of neighbors that can surround a creature (0).*/
            for (int y = 0; y < this.sizeY; y++){
                for (int x = 0; x < this.sizeX; x++){
                    this.grid[x,y].liveCheck(this.nextNeighbors(x,y), _maxNeighbors, _minNeighbors);
                }
            }
            // We first determine each creature's next state based upon their surrounding neighbors before updating the states of each creature.
            for (int b = 0; b < this.sizeY; b++){
                for (int a = 0; a < this.sizeX; a++){
                    this.grid[a,b].updateState();
                }
            }
        }

        public int nextNeighbors(int x, int y) {
            /*Returns the number of alive neighbors surrounding the designated creature in a 3x3 area.
            x - Designated creature's x coordinate
            y - Designated creature's y coordinate*/
            int aliveNeighbors = 0;
            for (int j = -1; j < 2; j++){
                for (int i = -1; i < 2; i++) {
                    if (this.coordState(x - i, y - j) > 0 ){
                        aliveNeighbors++;
                    }
                }
            }
            return aliveNeighbors;
        }

        public void playTime(int iterations, bool animate, int maxNeighbors, int minNeighbors) {
            /*Displays each phase of creature grid's life into the console, displaying the current map and iterating the next phase of life.
            iterations - number of user specified phases of life
            animate - bool whether or not previous phases should be cleared from console
            maxNeighbors - user specified maximum number of neighbors surrounding creature to survive
            minNeighbors - user specified minimum number of neighbors surrounding creature to survive*/
            for (int k = 0; k < iterations; k++) {
                if (animate) {
                    Thread.Sleep(1000);
                    Console.Clear();
                }  
                Console.WriteLine();
                this.showMap();
                this.nextStep(maxNeighbors, minNeighbors);
            }
        }
    }

    class ExtConvert {
        public string fileName;
        public string[] inputBoard;

        public int inputX;
        public int inputY;
        
        public ExtConvert (string _fileName) {
            /*ExtConvert constructor. Takes in a txt file path, reads all of the lines and places them into an array of strings,
            deletes all unnecessary spacing, and gets the dimensions of the result.
            _fileName - file path leading to user created creature grid .txt*/
            this.fileName = _fileName;
            this.inputBoard = File.ReadAllLines(this.fileName);
            this.spaceErase();
            this.getDimensions();
        }

        public (int InputWidth, int InputHeight) getDimensions() {
            /*Returns a tuple of dimensions based upon both the number of lines in the text file and number of elements present in text file's first line.
            */
            int inputHeight = this.inputBoard.Length; //input height is equal to the number of lines in the text file
            int inputWidth = 0; //counter that begins at zero and increments based upon number of 0s and 1s present in text file's first line
            for (int i = 0; i < this.inputBoard[0].Length; i++) {
                if ((this.inputBoard[0][i] == '0') || (this.inputBoard[0][i] == '1')){
                    inputWidth++;
                }
            }
            this.inputX = inputWidth;
            this.inputY = inputHeight;
            return (InputWidth:inputWidth, InputHeight:inputHeight);
        }

        public int[] getArray() {
            /*Returns an array of creature states in order based on user's text file*/
            int[] boardArray = new int[this.inputX * this.inputY];
            for (int i = 0; i < this.inputBoard.Length; i++) {
                for (int j = 0; j < this.inputBoard[i].Length; j++) {
                    if ((this.inputBoard[i][j] == '0') || (this.inputBoard[i][j] == '1')){
                        //(j) + (i * this.inputX) Converts 2D array into 1D by setting x value back to 0 when end of dimension is reached.
                        boardArray[(j) + (i * this.inputX)] = this.inputBoard[i][j] - '0';;
                    }
                }
            }
            return boardArray;
        }

        public void spaceErase() {
            /*Eliminates unnecessary spaces in user's text file*/
            for (int i = 0; i < this.inputBoard.Length; i++) {
                this.inputBoard[i] = this.inputBoard[i].Replace(" ", string.Empty);
            }
        }
    }

    class ConwayPractice {
        public static void Main(){
            ExtConvert Hugo = new ExtConvert("dayeleven.txt");
            World world = new World(Hugo);
            world.playTime(10, true, 7, 2);
        }
    }
}
