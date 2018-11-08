namespace ConsoleApplication1
{
    public interface IProblemResolver
    {
        void Resolve(IMessageProvider messageProvider);
    }

    public interface IMessageProvider
    {
        string Read();
        void Write(string output);
    }
}