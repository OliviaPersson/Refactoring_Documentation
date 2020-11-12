using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Refactoring_Documentation
{
    class Person
    {
        public string name, address, number, email;
        public Person(string N, string A, string T, string E)
        {
            name = N; address = A; number = T; email = E;
        }
        public void Print()
        {
            Console.WriteLine("{0}, {1}, {2}, {3}", name, address, number, email);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            List<Person> Dict = new List<Person>();
            LoadFile(Dict);

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
                switch (fieldToChange)
                {
                    case "namn": Dict[found].name = newValue; break;
                    case "adress": Dict[found].address = newValue; break;
                    case "telefon": Dict[found].number = newValue; break;
                    case "email": Dict[found].email = newValue; break;
                    default: break;
                }
            }
        }

        private static void ShowList(List<Person> Dict)
        {
            for (int i = 0; i < Dict.Count(); i++)
            {
                Person P = Dict[i];
                P.Print();
            }
        }

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

        private static void AddNewPerson(List<Person> Dict)
        {
            Console.WriteLine("Lägger till ny person");
            Console.Write("  1. ange namn:    ");
            string name = Console.ReadLine();
            Console.Write("  2. ange adress:  ");
            string address = Console.ReadLine();
            Console.Write("  3. ange telefon: ");
            string number = Console.ReadLine();
            Console.Write("  4. ange email:   ");
            string email = Console.ReadLine();
            Dict.Add(new Person(name, address, number, email));
        }

        private static void LoadFile(List<Person> Dict)
        {
            Console.Write("Laddar adresslistan ... ");
            using (StreamReader fileStream = new StreamReader(@"..\..\address.lis"))
            {
                while (fileStream.Peek() >= 0)
                {
                    string line = fileStream.ReadLine();
                    // Console.WriteLine(line);
                    string[] word = line.Split('#');
                    // Console.WriteLine("{0}, {1}, {2}, {3}", word[0], word[1], word[2], word[3]);
                    Person P = new Person(word[0], word[1], word[2], word[3]);
                    Dict.Add(P);
                }
            }
            Console.WriteLine("klart!");
        }
    }
}
