using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Metrics;

/*
 * Write a serialize and Deserialize function for the contact struct
 * Serialise should write contents of object into file
 * DeSerialize should read the contents of file and assign them to the object's variables
 */


namespace FileExercises
{
    struct Contact 
    {
        public string name;
        public string email;
        public int id;

        public Contact (string name, string email, int id)
        {
            this.name = name;
            this.email = email;
            this.id = id;
        }

        public void Print()
        {
            Console.WriteLine(name + " | " + email + " | " + id);
        }

        public void Serialize(string path)
        {
            try
            {
                //Makes the file
                StreamWriter writer = new StreamWriter(path);
                writer.WriteLine(name);
                writer.WriteLine(email);
                writer.WriteLine(id);
                writer.Close();

                //Dispose of the object
                try
                {
                    writer.Dispose();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

            }
        }

        public void DeSerialize(ref Contact contact, string path)
        {
            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    string currentLine;
                    int step = 0;
                    while ((currentLine = reader.ReadLine()) != null)
                    {
                        step ++;
                        Console.WriteLine(currentLine);
                        if (step == 1)
                        {
                            contact.name = currentLine;
                        }
                        else if (step == 2)
                        { 
                            contact.email = currentLine;
                        }
                        else if (step == 3)
                        {
                            Int32.TryParse(currentLine, out int id);
                            contact.id = id;
                        }

                    }
                    Console.WriteLine("");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
    
    internal class SerializeIO
    {
        public void Run()
        {
            Directory.CreateDirectory(@"contacts");

            //Make bob, fred, and jane contacts
            Contact bob = new Contact("Bob", "bob@email.com", 10000);
            Contact fred = new Contact("Fred", "Fred@email.com", 10001);
            Contact jane = new Contact("Jane", "Jane@email.com", 10002);

            //Write each contact to file
            bob.Serialize(@"contacts\bob.txt");
            fred.Serialize(@"contacts\Fred.txt");
            jane.Serialize(@"contacts\Jane.txt");

            //Clear out contacts
            bob = new Contact();
            fred = new Contact();
            jane = new Contact();

            //Load contacts from file
            bob.DeSerialize(ref bob, @"contacts\bob.txt");
            fred.DeSerialize(ref fred, @"contacts\Fred.txt");
            jane.DeSerialize(ref jane, @"contacts\Jane.txt");

            //Print contacts
            Console.WriteLine("Print Contacts");
            bob.Print();
            fred.Print();
            jane.Print();
        }
    }
}
