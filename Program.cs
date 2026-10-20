namespace Okosotthon;

// A program belépési pontja: létrehozza a központot, és csatlakoztatja az eszközöket.
public class Program
{
    private static void Main(string[] args)
    {
        OkosotthonKozpont kozpont = new OkosotthonKozpont();
        kozpont.OsszesCsatlakoztatasa();
    }
}
