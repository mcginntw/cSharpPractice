using System;

namespace ConwayPractice {

    class Creature {
        /* Initializes the Creature class, the individuals and their attributes that make up the grid.
        */
        int state;
        int age;
        int nextState;

        public Creature(int _state = 0, int _age = 0) {
            /* Creature constructor
            */
            this.state = _state;
            this.age = _age;
            this.nextState = 0;
        }

        public void setState(int _state) {
            // Updates the current creature's state to a user's inputted state
            this.state = _state;
        }

        public void setAge(int _age) {
            // Updates the current creature's age to a user's inputted state
            this.age = _age;
        }

        public int getState() {
            // Returns the current state of the specified creature (0 or 1).
            return this.state;
        }

        public int getAge() {
            // Returns the current age of the specified creature.
            return this.age;
        }

        public void liveCheck(int numNeighbors, int maxNeighbors, int minNeighbors) {
            /* Passing through the user's specified upper and lower limits of a creature's neighbors
            determines whether a specified creature will survive in the next state*/
            if ((minNeighbors < numNeighbors) && (numNeighbors < maxNeighbors)) {
                this.nextState = 1;
            }
        }
        
        public void updateState(){
            /* Updates the life state of the specified creature.
            Increments +1 to age if a creature survives for more than one iteration */
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
        /* Initializes the World class, a 2D plane of creatures.
        */

        // Initializes an empty 2D array of creatures
        public Creature[,] grid;
        int sizeX;
        int sizeY;

        public World (int _sizeX, int _sizeY) {
            /* World constructor
            */
            this.sizeX = _sizeX;
            this.sizeY = _sizeY;
            // Populates the grid with creatures based on the user inputted dimensions
            this.grid = new Creature[_sizeX,_sizeY];
            this.populate();
        }

        public void populate() {
            /* Randomly sets the initial life state of each creature in the grid
            */
            Random rand = new Random();
            for (int i = 0; i < this.sizeX; i++) {
                for (int j = 0; j < this.sizeY; j++) {
                    this.grid[i,j] = new Creature(rand.Next(0,2));
                }
            }
        }
        public int coordState(int x, int y){
            /* Returns the state of a creature at a specified coordinate.
            */

            //Modulo wrap-around in case x or y is outside of the grid.
            x = ((((x % this.sizeX) + this.sizeX) % this.sizeX));
            y = ((((y % this.sizeY) + this.sizeY) % this.sizeY));
            // Returns the state of the creature at specified coordinates.
            return this.grid[x, y].getState();
        }

        public int coordAge(int x, int y){
            /* Returns the age of a creature at a specified coordinate.
            */

            //Modulo wrap-around in case x or y is outside of the grid.
            x = ((((x % this.sizeX) + this.sizeX) % this.sizeX));
            y = ((((y % this.sizeY) + this.sizeY) % this.sizeY));
            // Returns the age of the creature at specified coordinates.
            return this.grid[x, y].getAge();
        }

        public void showMap(){
            /* Displays the 2D grid of creatures and attributes (Age/State) in the console
            */
            for (int i = 0; i < this.sizeY; i++){
                for (int j = 0; j < this.sizeX; j++){
                    // Displays each individual creature on the X axis, separated by a space
                    Console.Write($"{this.coordAge(j, i)} ");
                }
                // Once at the end of the X axis, newline to begin the display process again on the next Y value.
                Console.WriteLine();
            }
        }

        public void nextStep(int _maxNeighbors, int _minNeighbors) {
            /* Based upon the survival of each creature, updates their attribute accordingly for the next stage of life
            */
            for (int y = 0; y < this.sizeY; y++){
                for (int x = 0; x < this.sizeX; x++){
                    // Checks if the current creature will survive based upon if the number of surrounding alive neighbors.
                    this.grid[x,y].liveCheck(this.nextNeighbors(x,y), _maxNeighbors, _minNeighbors);
                }
            }
                //Updates the state of each creature to transition into the next stage of life
            for (int b = 0; b < this.sizeY; b++){
                for (int a = 0; a < this.sizeX; a++){
                    this.grid[a,b].updateState();
                }
            }
        }

        public int nextNeighbors(int x, int y) {
            /* Returns the number of alive neighbors surrounding the specified creature in a 3x3 area.
            */
            int aliveNeighbors = 0;
            
            // Iterates through a 2D 3x3 array beginning at a (-1,-1) translation from the specified creature, making it the center of the 3x3 array.
            for (int j = -1; j < 2; j++){
                for (int i = -1; i < 2; i++) {
                    // If the state of the current neighbor is not dead, add it to the total number of alive neighbors
                    if (this.coordState(x - i, y - j) > 0 ){
                        aliveNeighbors++;
                    }
                }
            }
            return aliveNeighbors;
        }

        public void playTime(int iterations, bool animate, int maxNeighbors, int minNeighbors) {
            /* Iterates through the stages of the grid based upon the user's number of iterations, and neighbor threshold
            */

            //Updates the board from the number of user defined iterations
            for (int k = 0; k < iterations; k++) {
                // If the user sets animate to true, each time the board updates, the console clears. One dynamic grid will update in the console every second.
                if (animate) {
                    Thread.Sleep(1000);
                    Console.Clear();
                }  
                // If animate is set to false, you can view the previous stages of the grids above the current grid in the console.
                Console.WriteLine();
                this.showMap();
                this.nextStep(maxNeighbors, minNeighbors);
            }
        }
    }


    class ConwayPractice {
        public static void Main(){
            // Creates a new 8x8 world of creatures
            World world = new World(8,8);
            world.playTime(10, true, 7, 2);
        }
    }
}
