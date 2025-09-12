//Mars Rover Task
//Capture user input (x,y,z coordinates, direction, commands)
//Process / validate user input ()
//Move rover (x,y,z coordinates)
//Rotate rover (direction)
//Store position (x,y,z coordinates, direction)
//Maintain plateua (x,y coordinates)
//Validate next move (against plateau boundaries and obstacles)
//Output position / status (x,y,z coordinates, direction)

class SetCoordinates
{
    static void Main()
    {
        Console.WriteLine("Enter the upper-right coordinates of the plateau (e.g 5 5): ");
        string? plateauInput = Console.ReadLine(); //Splits the input into an array based on spaces
        string[] plateauCoordinates = plateauInput.Split(' '); //Splits the input into an array based on spaces, so the input 5 5 will be split into [5,5]
        int plateauX = int.Parse(plateauCoordinates[0]); //Converts the first element (5) of the array to an integer and assigns it to plateauX
        int plateauY = int.Parse(plateauCoordinates[1]); //Converts the second element (5) of the array to an integer and assigns it to plateauY
        if (plateauX < 0 || plateauY < 0) //Checks if the plateau coordinates are negative
        {
            Console.WriteLine("Invalid Plateau Coordinates!"); //Outputs a message if the plateau coordinates are not correct
            return; //Exits the program
        }
        else if (plateauCoordinates.Length != 2) //Checks if the input has exactly 2 elements (x and y)
        {
            Console.WriteLine("Invalid Input! Please enter in the format: X Y (e.g, 5 5)"); //Outputs a message if the input format is incorrect
            return; //Exits the program
        }
        else
        {
            Console.WriteLine("How many rovers would you like to deploy? "); //Asks the user how many rovers they would like to deploy
            int roverCount = int.Parse(Console.ReadLine()); //Reads the user input as a string
            List<Rover1> rovers = new List<Rover1>();
            for (int i = 0; i < roverCount; i++) //Loops through the number of rovers specified by the user, incrementing it by one, if the requirements meet
            {
                Console.WriteLine("Enter the rover's starting position and direction (e.g., 1 2 N): "); // askes the user to input the starting position and direction of the rover
                string? roverInput = Console.ReadLine(); //Reads the user input as a string
                string[] rovercoordinates = roverInput.Split(' '); //Splits the input into an array based on spaces, so the input 1 2 N will be split into [1,2,N]
                int startX = int.Parse(rovercoordinates[0]); //Converts the first element of the array to an integer and assigns it to startX
                int startY = int.Parse(rovercoordinates[1]); //Converts the second element of the array to an integr and assigns it to startY
                if (startX > plateauX || startY > plateauY || plateauX < 0 || plateauY < 0) //Checks if the starting position of the rover is outside the plateau boundaries
                {
                    Console.WriteLine("Rover is out of bounds!"); //Outputs a message if the rover is out of bounds
                }
                else if (rovercoordinates.Length != 3) //Checks if the input has exactly 3 elements (x, y, direction)
                {
                    Console.WriteLine("Invalid Input! Please enter in the format: X Y Z (e.g., 1 2 N)"); //Outputs a message if the input format is incorrect
                }
                else
                {
                    char startDirection = char.Parse(rovercoordinates[2]); //Converts the third element
                    if (startDirection != 'N' && startDirection != 'E' && startDirection != 'S' && startDirection != 'W') //Checks if the starting direction is valid (N, E, S, W)
                    {
                        Console.WriteLine("Invalid Direction!"); //Outputs a message if the direction is invalid
                    }

                    else
                    {
                        Rover1 rover = new Rover1(startX, startY, startDirection, plateauX, plateauY); //Creates a new instance of the Rover class with the starting position, direction and plateau size
                        Console.WriteLine("Enter the rover's movement commands (e.g., LMLMLMLMM): "); //Asks the user to input the movement commands for the rover
                        string? commands = Console.ReadLine(); //Reads the user input as a string
                        if (commands == null) //Checks if the commands string is null
                        {
                            Console.WriteLine("No comands given!"); //Outputs a message if no commands are given
                        }
                        else
                        {
                            rover.ExecuteCommands(commands);  //this sends the commands to the Rover object
                            Console.WriteLine("Final position of the rover: " + rover); ; //Calls the ExecuteCommands method of the Rover class, passing in the commands string
                        }

                    }
                }
            }

        }
    }
}  

class Rover1 //create a class called Rover
    {
        // Created properties x,y and direction for the Rover's movement
        int x;
        int y;
        char direction;
        int maxPX; //max plateau X coordinate
        int maxPY; //max plateau Y coordinate

        public Rover1(int startX, int startY, char startDirection, int plateauX, int plateauY) //public constructor used to set the Rover's starting position and direction, as well as the max plateau size
        {
            x = startX;
            y = startY;
            direction = startDirection;
            maxPX = plateauX;
            maxPY = plateauY;
        }
        public void ExecuteCommands(string commands) // public method, used get multiple commands in one row
        {
            foreach (char command in commands) // The foreach loop will break down the string into individual characters, where it will go through each character one by one
            {
                Move(command); //It then passes on each broken down character to the Move method (function) to be processed
            }
        }
        public void Move(char command) //public no output method used to set Rover's command (L,R,M)
        {
            switch (command) //switch statement, used to check the input given with the cases set
            {
                case 'L':
                    TurnLeft();
                    break;
                case 'R':
                    TurnRight();
                    break;
                case 'M':
                    MoveForward();
                    break;
                default:
                    Console.WriteLine("Invalid command Given");
                    break;
            }
        }
        private void TurnLeft() //private method, set to change the direction of the Rover left
        {
            direction = direction switch //the variable direction is set to the direction given from the switch statement
            {
                'N' => 'W',
                'W' => 'S',
                'S' => 'E',
                'E' => 'N',
                _ => direction // _ represents a default case, if none of the above apply
            };
        }
        private void TurnRight() //private method, set to change the direction of the Rover right
    {
        direction = direction switch
        {
            'N' => 'E',
            'E' => 'S',
            'S' => 'W',
            'W' => 'N',
            _ => direction // _ represents a default case, if none of the above apply
        };
    }
        private void MoveForward() //private method, set to change the direction of the Rover foward
        {
            switch (direction)
            {
                case 'N': //if the direction is North, increase y by 1. Moving it North (up). 
                    y += 1;
                    break;
                case 'E': //if the direction is East, increase x by 1. Moving it East (right).
                    x += 1;
                    break;
                case 'S': //if the direction is South, decrease y by 1. Moving it South (down).
                    y -= 1;
                    break;
                case 'W':
                    x -= 1; //if the direction is West, decrease x by 1. Moving it West (left). 
                    break;
            }
        }

        public override string ToString() //public method, overriding object ToString, used to output the final position and direction of the Rover
        {
            return $"{x} {y} {direction}"; //returning the values of x,y and direction. Used the $ to embed variables into the string
        }
    }    