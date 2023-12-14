using System.Buffers.Binary;
using CB.SourceGenerators.Attributes;

namespace ConsoleApp1;

public partial class MyClass
{
    [Inject] private readonly MyService myService;
    [InjectOptional] private readonly IMyService2? myService2;

    void aa()
    {
        BinaryPrimitives.ReadDoubleBigEndian(null);
    }

    partial void Init()
    {
        Console.WriteLine("aaaaaaaaa");
    }
}
