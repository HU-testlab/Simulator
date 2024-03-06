using System;

public class Simpletron
{
    private const int MEMORY_SIZE = 100;
    private int[] memory = new int[MEMORY_SIZE];
    private int accumulator;
    private int instructionCounter;
    private bool running;

    public Simpletron()
    {
        accumulator = 0;
        instructionCounter = 0;
        running = false;
    }

    public void LoadProgram()
    {
        Console.WriteLine("*** Welcome to Simpletron! ***");
        Console.WriteLine("*** Please enter your program one instruction ***");
        Console.WriteLine("*** ( or data word ) at a time into the input ***");
        Console.WriteLine("*** text field. I will display the location ***");
        Console.WriteLine("*** number and a question mark (?). You then ***");
        Console.WriteLine("*** type the word for that location. Enter ***");
        Console.WriteLine("*** -99999 to stop entering your program. ***");

        int input;
        int location = 0;
        Console.Write($"{location:D2} ? ");
        while ((input = Convert.ToInt32(Console.ReadLine())) != -99999)
        {
            if (location >= MEMORY_SIZE || input < -9999 || input > 9999)
            {
                Console.WriteLine("*** Invalid input or memory overflow. ***");
                Console.WriteLine("*** Please enter a number between -9999 and 9999. ***");
                Console.WriteLine("*** Enter -99999 to stop entering your program. ***");
            }
            else
            {
                memory[location++] = input;
                Console.Write($"{location:D2} ? ");
            }
        }

        Console.WriteLine("*** Program loading completed ***");
        Console.WriteLine("*** Program execution begins ***");
        running = true;
    }

    public void ExecuteProgram()
    {
        while (running)
        {
            int instructionRegister = memory[instructionCounter];
            int operationCode = instructionRegister / 100;
            int operand = instructionRegister % 100;

            switch (operationCode)
            {
                case 10: // Read
                    Console.Write("Enter a number: ");
                    memory[operand] = Convert.ToInt32(Console.ReadLine());
                    break;
                case 11: // Write
                    Console.WriteLine(memory[operand]);
                    break;
                case 20: // Load
                    accumulator = memory[operand];
                    break;
                case 21: // Store
                    memory[operand] = accumulator;
                    break;
                case 30: // Add
                    accumulator += memory[operand];
                    break;
                case 31: // Subtract
                    accumulator -= memory[operand];
                    break;
                case 40: // Branch
                    instructionCounter = operand;
                    break;
                case 41: // BranchNeg
                    if (accumulator < 0)
                    {
                        instructionCounter = operand;
                    }
                    break;
                case 42: // BranchZero
                    if (accumulator == 0)
                    {
                        instructionCounter = operand;
                    }
                    break;
                case 43: // Halt
                    running = false;
                    Console.WriteLine("*** Program execution completed ***");
                    break;
                default:
                    Console.WriteLine("*** Invalid operation code ***");
                    running = false;
                    break;
            }

            instructionCounter++;
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Create a Simpletron object
        Simpletron simpletron = new Simpletron();

        // Load the program into the Simpletron
        simpletron.LoadProgram();

        // Execute the program
        simpletron.ExecuteProgram();
    }
}
