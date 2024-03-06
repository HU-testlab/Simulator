using System;
using System.Collections.Generic;

namespace SimpletronSimulation
{
    class Simpletron
    {
        private const int MEMORY_SIZE = 1000; // Increased memory size
        private const int FLOAT_PRECISION = 2; // Floating-point precision
        private double[] memory; // Using double for memory to support floating-point values
        private double accumulator;
        private int instructionCounter;

        public Simpletron()
        {
            memory = new double[MEMORY_SIZE];
            accumulator = 0;
            instructionCounter = 0;
        }

        public void LoadProgram(double[] program)
        {
            for (int i = 0; i < program.Length; i++)
            {
                memory[i] = program[i];
            }
        }

        public void Run()
        {
            Console.WriteLine("*** Program loading completed ***");
            Console.WriteLine("*** Program execution begins ***");

            while (instructionCounter < MEMORY_SIZE && memory[instructionCounter] != -99999)
            {
                double instruction = memory[instructionCounter];
                int opcode = (int)(instruction / 100);
                int operand = (int)(instruction % 100);

                switch (opcode)
                {
                    case 10: // Read Integer
                        Console.Write("\nEnter an integer: ");
                        while (!double.TryParse(Console.ReadLine(), out memory[operand]))
                        {
                            Console.WriteLine("Invalid input. Please enter a valid integer.");
                            Console.Write("\nEnter an integer: ");
                        }
                        break;
                    case 11: // Write Integer
                        Console.WriteLine($"\n{memory[operand]}");
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
                    case 32: // Multiply
                        accumulator *= memory[operand];
                        break;
                    case 33: // Divide
                        if (memory[operand] != 0)
                            accumulator /= memory[operand];
                        else
                            Console.WriteLine("*** Attempt to divide by zero ***");
                        break;
                    case 40: // Remainder
                        if (memory[operand] != 0)
                            accumulator %= memory[operand];
                        else
                            Console.WriteLine("*** Attempt to calculate remainder with divisor zero ***");
                        break;
                    case 41: // Exponentiation
                        accumulator = Math.Pow(accumulator, memory[operand]);
                        break;
                    case 42: // Output Newline
                        Console.WriteLine();
                        break;
                    case 43: // Read Floating-Point
                        Console.Write("\nEnter a floating-point number: ");
                        while (!double.TryParse(Console.ReadLine(), out memory[operand]))
                        {
                            Console.WriteLine("Invalid input. Please enter a valid floating-point number.");
                            Console.Write("\nEnter a floating-point number: ");
                        }
                        break;
                    case 44: // Write Floating-Point
                        Console.WriteLine(memory[operand].ToString("F" + FLOAT_PRECISION));
                        break;
                    case 52: // Halt
                        Console.WriteLine("*** Program halted ***");
                        return;
                    default:
                        Console.WriteLine("*** Invalid opcode ***");
                        break;
                }

                instructionCounter++;
            }

            Console.WriteLine("\n*** Simpletron execution abnormally terminated ***");
            Display();
        }

        public void Display()
        {
            Console.WriteLine("*** Registers ***");
            Console.WriteLine("Accumulator: " + accumulator.ToString("F" + FLOAT_PRECISION));
            Console.WriteLine("Instruction Counter: " + instructionCounter);
            Console.WriteLine("*** Memory ***");
            for (int i = 0; i <= instructionCounter; i++)
            {
                Console.WriteLine($"{i:D3}: {memory[i].ToString("F" + FLOAT_PRECISION)}");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Simpletron simpletron = new Simpletron();

            Console.WriteLine("*** Welcome to Simpletron! ***");
            Console.WriteLine("*** Please enter your program one instruction ***");
            Console.WriteLine("*** (or data word) at a time into the input ***");
            Console.WriteLine("*** text field. I will display the location ***");
            Console.WriteLine("*** number and a question mark (?). You then ***");
            Console.WriteLine("*** type the word for that location. Enter ***");
            Console.WriteLine("*** -99999 to stop entering your program. ***");

            double[] program = ReadProgramFromUser();
            simpletron.LoadProgram(program);
            simpletron.Run();
        }

        static double[] ReadProgramFromUser()
        {
            Console.WriteLine("Enter program instructions:");
            Console.WriteLine("Format: LocationNumber Instruction");
            Console.WriteLine("Example: 1010");

            var program = new List<double>();

            while (true)
            {
                Console.Write("?");
                if (!double.TryParse(Console.ReadLine(), out double instruction))
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                    continue;
                }

                if (instruction == -99999)
                    break;

                program.Add(instruction);
            }

            return program.ToArray();
        }
    }
}
