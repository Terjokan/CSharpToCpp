
/*
Roule Set:
    Class:
        After the class Keyword always follows the Identifier example: "class Language" Language is always after class 
        Open Breaket is always after the Identifier example: "Language {" { is always after Language

*/
public class Language
{
    public Person MainCaracter;
    public static void Main2()
    {
        Console.WriteLine("Hello World");

        Person person = new Person(); // <- Here Person is not a key word

        person.Name = "Test";

        person.SayHello(1);
    }
}

public class Person
{
    public string Name;


    public void SayHello(int numer)
    {
        Console.WriteLine(Name + ": Hallo");
        Console.WriteLine(Name + ": Meine zahl ist " + numer);
    }
}





