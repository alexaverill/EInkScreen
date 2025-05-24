class GenericController : IScreenController
{
    public void Clean()
    {
        throw new NotImplementedException();
    }

    public void SetColor()
    {
        throw new NotImplementedException();
    }

    public void SetImage(string path)
    {
        Console.WriteLine("Setting image!");
    }
}