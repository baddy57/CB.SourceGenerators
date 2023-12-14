using System.Windows;
using CB.SourceGenerators.Attributes;
using ConsoleApp1;

namespace CB.SourceGenerators.Demo;

[WpfInject]
public partial class Window1 : Window
{
    [Inject] MyService myService;
    [Inject] IMyService2 aaa;
    [InjectOptional] IMyService2? bbbbb;

    [DependencyProperty] int count = 1;
    [DependencyProperty] int _count2 = 1;

    [DependencyProperty] string _nome = "ciccio";

    [DependencyProperty] string surname = nameof(surname);

    partial void OnFirstLoad()
    {
        throw new NotImplementedException();
    }
}
