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

            // Load the SML program into memory
            memory[0] = +1007; // Read first number
            memory[1] = +2007; // Load first number into accumulator
            memory[2] = +1008; // Read second number
            memory[3] = +3008; // Add second number to accumulator
            memory[4] = +2109; // Store result in memory location 09
            memory[5] = +1109; // Write result to output
            memory[6] = +4300; // Halt

            int instructionCounter = 0; // Program counter

            // Execute the SML program
            while (memory[instructionCounter] != +4300) // Halt instruction
            {
                int opcode = memory[instructionCounter] / 100;
                int operand = memory[instructionCounter] % 100;

                switch (opcode)
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
                }

                instructionCounter++;
            }

            // Additional tasks from the second program

            // Task A: Sentinel-Controlled Loop to Compute and Display the Sum of Positive Numbers
            Console.WriteLine("Task A: Sentinel-Controlled Loop to Compute and Display the Sum of Positive Numbers");
            Console.WriteLine("Enter positive numbers (enter a negative number to stop):");

            int sum = 0;
            int sumCount = 0;
            int largest = int.MinValue;

            int number;
            do
            {
                Console.Write("Enter a number: ");
                number = int.Parse(Console.ReadLine());

                if (number >= 0)
                {
                    sum += number;
                    sumCount++;
                }

                if (number > largest)
                {
                    largest = number;
                }
            } while (number >= 0);

            Console.WriteLine($"The sum of positive numbers entered is: {sum}");
            Console.WriteLine();

            // Task B: Counter-Controlled Loop to Compute and Display the Average of Seven Numbers
            Console.WriteLine("Task B: Counter-Controlled Loop to Compute and Display the Average of Seven Numbers");
            Console.WriteLine("Enter seven numbers:");

            sum = 0;
            for (int i = 1; i <= 7; i++)
            {
                Console.Write($"Enter number {i}: ");
                int num = int.Parse(Console.ReadLine());
                sum += num;
            }

            double average = (double)sum / 7;
            Console.WriteLine($"The average of the seven numbers is: {average}");
            Console.WriteLine();

            // Task C: Determine and Display the Largest Number from a Series of Numbers
            Console.WriteLine("Task C: Determine and Display the Largest Number from a Series of Numbers");
            Console.Write("Enter the number of numbers to be processed: ");
            int count = int.Parse(Console.ReadLine());

            Console.WriteLine($"Enter {count} numbers:");

            for (int i = 1; i <= count; i++)
            {
                Console.Write($"Enter number {i}: ");
                int num = int.Parse(Console.ReadLine());

                if (num > largest)
                {
                    largest = num;
                }
            }

            Console.WriteLine($"The largest number entered is: {largest}");
        }
    }
}
