namespace SourceGenArrayAttrIssue
{
    [UseCloneFor("SourceGen.SampleDomain.Person", "PersonClone")]
    internal class Program
    {
        static void Main(string[] args)
        {
            var clone = new PersonClone();
            clone.Name = "Hello World!";
            Console.WriteLine(clone.Name);
        }
    }
}
