using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Refactoring_Documentation
{
    /* CLASS: Person
     * PURPOSE: Person class whith defined attributes and two constructors that will run when the class is initiated
     */
    class Person
    {
        public string name, address, number, email;
        /* CONSTRUCTOR: Person
         * PURPOSE: When class is initialized with attributes, sets the attributes for the person class
         */
        public Person(string N, string A, string T, string E)
        {
            name = N; address = A; number = T; email = E;
        }
        /* CONSTRUCTOR: Person
         * PURPOSE: When class is initialized without attributes, asking user for input and sets the attributes
         */
        public Person()
        {
            Console.Write(" 1. ange namn:  ");
            name = Console.ReadLine();
            Console.Write(" 2. ange adress:  ");
            address = Console.ReadLine();
            Console.Write(" 3. ange telefon: ");
            number = Console.ReadLine();
            Console.Write(" 4. ange email:   ");
            email = Console.ReadLine();
        }
        /* METHOD: Print 
         * PURPOSE: Prints the attributes for a person
         */
        public void Print()
        {
            Console.WriteLine("{0}, {1}, {2}, {3}", name, address, number, email);
        }
        /* METHOD: UpdateField
         * PURPOSE: Depending on the user input changes the value of an attribute
         * PARAMETERS: fieldToChange - stores user input of which attribute to change, newValue - stores user input that will be set to the attribute
         */
        public void UpdateField(string fieldToChange, string newValue)
        {
            switch (fieldToChange)
            {
                case "namn": name = newValue; break;
                case "adress": address = newValue; break;
                case "telefon": number = newValue; break;
                case "email": email = newValue; break;
                default: break;
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            List<Person> Dict = new List<Person>();
            LoadListFromFile(Dict);

            Console.WriteLine("Hej och välkommen till adresslistan");
            Console.WriteLine("Skriv 'sluta' för att sluta!");
            string command;
            do
            {
                Console.Write("> ");
                command = Console.ReadLine();
                if (command == "sluta")
                {
                    Console.WriteLine("Hej då!");
                }
                else if (command == "ny")
                {
                    AddNewPerson(Dict);
                }
                else if (command == "ta bort")
                {
                    DeletePerson(Dict);
                }
                else if (command == "visa")
                {
                    ShowList(Dict);
                }
                else if (command == "ändra")
                {
                    UpdatePersonInList(Dict);
                }
                else
                {
                    Console.WriteLine("Okänt kommando: {0}", command);
                }
            } while (command != "sluta");
        }
        /* METHOD: UpdatePersonInList (static)
         * PURPOSE: Updates choosen attribute in person object
         * PARAMETERS: Dict - List that contains person objects with attributes name, address, number and email
         */
        private static void UpdatePersonInList(List<Person> Dict)
        {
            Console.Write("Vem vill du ändra (ange namn): ");
            string personToChange = Console.ReadLine();
            int found = -1;
            for (int i = 0; i < Dict.Count(); i++)
            {
                if (Dict[i].name == personToChange) found = i;
            }
            if (found == -1)
            {
                Console.WriteLine("Tyvärr: {0} fanns inte i telefonlistan", personToChange);
            }
            else
            {
                Console.Write("Vad vill du ändra (namn, adress, telefon eller email): ");
                string fieldToChange = Console.ReadLine();
                Console.Write("Vad vill du ändra {0} på {1} till: ", fieldToChange, personToChange);
                string newValue = Console.ReadLine();
                Dict[found].UpdateField(fieldToChange, newValue);
            }
        }
        /* METHOD: ShowList (static)
         * PURPOSE: loop through list and prints out each person object in the list
         * PARAMETERS: Dict - List that contains person objects with attributes name, address, number and email
         */
        private static void ShowList(List<Person> Dict)
        {
            for (int i = 0; i < Dict.Count(); i++)
            {
                Person P = Dict[i];
                P.Print();
            }
        }
        /* METHOD: DeletePerson (static)
         * PURPOSE: Ask user for input and deletes person in list if available
         * PARAMETERS: Dict - List that contains person objects with attributes name, address, number and email
         */
        private static void DeletePerson(List<Person> Dict)
        {
            Console.Write("Vem vill du ta bort (ange namn): ");
            string personToDelete = Console.ReadLine();
            int found = -1;
            for (int i = 0; i < Dict.Count(); i++)
            {
                if (Dict[i].name == personToDelete) found = i;
            }
            if (found == -1)
            {
                Console.WriteLine("Tyvärr: {0} fanns inte i telefonlistan", personToDelete);
            }
            else
            {
                Dict.RemoveAt(found);
            }
        }
        /* METHOD: AddNewPerson (static)
         * PURPOSE: Asks user for input about new person and adds this person to the list
         * PARAMETERS: Dict - List that contains person objects with attributes name, address, number and email
         */
        private static void AddNewPerson(List<Person> Dict)
        {
            Console.WriteLine("Lägger till ny person");
            Dict.Add(new Person());
        }
        /* METHOD: LoadFile (static)
         * PURPOSE: Loads file, split when '#' and stores the person object in list Dict
         * PARAMETERS: List that will contain person objects
         */
        private static void LoadListFromFile(List<Person> Dict)
        {
            Console.Write("Laddar adresslistan ... ");
            using (StreamReader fileStream = new StreamReader(@"..\..\address.lis"))
            {
                while (fileStream.Peek() >= 0)
                {
                    string line = fileStream.ReadLine();
                    string[] word = line.Split('#');
                    Person P = new Person(word[0], word[1], word[2], word[3]);
                    Dict.Add(P);
                }
            }
            Console.WriteLine("klart!");
        }
    }
}
