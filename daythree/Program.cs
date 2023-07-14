namespace LLPractice {
    class LL {
        /* Defines the linked list (LL) class
        */

        public int pointData;


        LL nextItem;


        LL headItem;


        
        public LL(int _pointData, LL _nextItem = null, LL _headItem = null) {
            /* LL constructor :)
            */
            this.pointData = _pointData;
            this.nextItem = _nextItem;
            this.headItem = _headItem;
        }



        
        public void push(int _pointData){
            /* Adds an item to the end of the linked list.
            Passes through user inputted data point value */

            // If there is an item after the current item
            if (this.nextItem != null) {
                // Recursive call from the next item into push function until we reach the end of the list
                this.nextItem.push(_pointData);
            } else {
                // Check if at the end of the list
                if (this.headItem == null) {
                    // Add this data point to the current position
                    this.nextItem = new LL(_pointData, null, this);
                } else {
                    // If there is no item in the list, make this data point the first item
                    this.nextItem = new LL(_pointData, null, this.headItem);
                }
            }
        }

        // Removes an item from the linked list
        public LL pop(){
            // If the position after the next item contains something
            if (this.nextItem.nextItem != null) {

                // Recursive call from the next item into pop function until above critera is met
                return this.nextItem.pop();
            }
            // Once at the end of the list, create a temp object to place the current item into
            LL temp = this.nextItem;

            // DESTROY THE CURRENT ITEM AND CAST IT INTO THE FIRES OF HELL by setting it equal to null
            this.nextItem = null;
            
            // return the item which we deleted
            return temp;
        }

        // Returns the value in the linked list position passed through
        public int index(int indexNum) {
            
            // If there are no more steps to take return the value associated with the current position
            if (indexNum == 0){
                return this.pointData;
            }

            // If there are more steps to take and the next item in the linked list is not null
            if (this.nextItem != null) {

                // Recursive call from the next item into index, subtracting one step
                return this.nextItem.index(--indexNum);
            }

            //Error catch, if the value inputted does not have a value associated with it, return this monstrosity
            return -1000012345;
        }

        // Returns the position associated with the first instance of the data value passed through
        public int findIndex(int _pointData, int indexNum = 0) {

            // If the current point data is equal to the point data passed through return the position number
            if (this.pointData == _pointData){
                return indexNum;
            }
            
            // If there is an item in the list after the current one
            if (this.nextItem != null) {

                // Recursive call from the next item into findIndex. Adding one to the position in the list, continue until a match is found.
                return this.nextItem.findIndex(_pointData, ++indexNum);
            }
            
            // Error catch if no match is found, return BOOBS!!
            return 8008511;
        }
    }

    // Declares our linked list practice playspace :)
    class LLPractice {

        // Main
        public static void Main(){
            
            // Creates a new linked list called list and applies the properties of a linked list with the head item being 1
            LL list = new LL(1);

            // Adds 2, 3, and 4 respectively to the linked list
            list.push(2);
            list.push(3);
            list.push(4);
            
            // Outputs to console the data value associated with position 2 in the linked list
            Console.WriteLine(list.index(2));

            // Finds the first instance of the data point 2 and returns the index number
            Console.WriteLine(list.findIndex(2));

            // Finds the first instance of the data point 2 and returns the index number, which then returns the data value at said index... which is in the same position.
            Console.WriteLine(list.index(list.findIndex(4)));
        }
    }
}