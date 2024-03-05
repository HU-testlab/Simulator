using System;

namespace Simpletron
{
    class Program
    {
        static void Main(string[] args)
        {
            // Simpletron memory
            int[] memory = new int[100]; 

            // Accumulator register
            int accumulator = 0; 

            // Instruction register
            int instructionRegister = 0;

            // Operation code
            int operationCode = 0;

            // Operand
            int operand = 0;

            // Instruction counter
            int instructionCounter = 0;

            Console.WriteLine("*** Welcome to Simpletron! ***");
            Console.WriteLine("*** Please enter your program one instruction ***");
            Console.WriteLine("*** (or data word) at a time into the input ***");
            Console.WriteLine("*** text field. I will display the location ***");
            Console.WriteLine("*** number and a question mark (?). You then ***");
            Console.WriteLine("*** type the word for that location. Enter ***");
            Console.WriteLine("*** -99999 to stop entering your program. ***");

            // Program loading phase
            while (true)
            {
                Console.Write($"{instructionCounter:D2} ? ");
                int input = int.Parse(Console.ReadLine());

                if (input == -99999)
                {
                    Console.WriteLine("*** Program loading completed ***");
                    Console.WriteLine("*** Program execution begins ***");
                    break;
                }

                if (input < -9999 || input > 9999)
                {
                    Console.WriteLine("Error: Instruction must be in the range -9999 to +9999.");
                    continue;
                }

                memory[instructionCounter++] = input;
            }

            // Program execution phase
            while (memory[instructionCounter] != +4300) // Halt instruction
            {
                instructionRegister = memory[instructionCounter];
                operationCode = instructionRegister / 100;
                operand = instructionRegister % 100;

                switch (operationCode)
                {
                    case 10: // Read
                        Console.Write("Enter a number: ");
                        memory[operand] = int.Parse(Console.ReadLine());
                        break;
                    case 20: // Load
                        accumulator = memory[operand];
                        break;
                    case 30: // Add
                        accumulator += memory[operand];
                        break;
                    case 21: // Store
                        memory[operand] = accumulator;
                        break;
                    case 11: // Write
                        Console.WriteLine($"Result: {memory[operand]}");
                        break;
                    case 40: // Branch
                        instructionCounter = operand;
                        break;
                    case 41: // BranchNeg
                        if (accumulator < 0)
                            instructionCounter = operand;
                        break;
                    case 42: // BranchZero
                        if (accumulator == 0)
                            instructionCounter = operand;
                        break;
                    default:
                        Console.WriteLine("Error: Invalid operation code.");
                        break;
                }

                instructionCounter++;

                // Check for fatal errors
                if (accumulator > 9999 || accumulator < -9999)
                {
                    Console.WriteLine("*** Accumulator overflow ***");
                    DisplayMemoryDump(memory, accumulator, instructionCounter);
                    return;
                }

                if (operationCode == 30 && memory[operand] == 0)
                {
                    Console.WriteLine("*** Attempt to divide by zero ***");
                    DisplayMemoryDump(memory, accumulator, instructionCounter);
                    return;
                }
            }

            // Display final state
            DisplayMemoryDump(memory, accumulator, instructionCounter);
        }

        static void DisplayMemoryDump(int[] memory, int accumulator, int instructionCounter)
        {
            Console.WriteLine("*** Simpletron execution abnormally terminated ***");
            Console.WriteLine("*** Registers ***");
            Console.WriteLine($"Accumulator: {accumulator}");
            Console.WriteLine($"Instruction Counter: {instructionCounter}");
            Console.WriteLine("*** Memory ***");
            for (int i = 0; i < memory.Length; i++)
            {
                Console.WriteLine($"{i:D2}: {memory[i]:+0000}");
            }
        }
    }
}
