namespace CSharpToCpp
{
    public class Program
    {

        public static void Main()
        {
            string sourceCode = @"
        public class Language
        {
            public static void Main2()
            {
                Console.WriteLine(""Hello World"");

                Person person = new Person();

                person.Name = ""Test"";

                person.SayHello(1);
            }
        }

        public class Person
        {
            public string Name;

            public void SayHello(int number)
            {
                Console.WriteLine(Name + "": Hallo"");
                Console.WriteLine(Name + "": Meine zahl ist "" + number);
            }
        }";

            Tokenizer tokenizer = new Tokenizer(sourceCode);
            List<Token> tokens = tokenizer.Tokenize();



            foreach (var token in tokens)
            {
                Console.WriteLine(token);
            }
        }

    }
}