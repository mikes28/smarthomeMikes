namespace Okosotthon;

// Központi riasztó: egyszerre három képességet valósít meg
// (időzítés, hangjelzés, statisztika).
public class OkosKozpontiRiaszto : OkosEszkoz, IIdozitheto, IHangjelzo, IStatisztika
{
    // Az időzítő hátralévő ideje, percekben.
    private int hatralevoIdo;

    // A kibocsátott hangjelzések száma.
    private int hangjelzesekSzama;

    public OkosKozpontiRiaszto(string azonosito, string nev)
        : base(azonosito, nev)
    {
    }

    // Időzítő beállítása percekben.
    public void IdozitestBeallit(int perc)
    {
        this.hatralevoIdo = perc;
    }

    // Az időzítő hátralévő ideje (perc).
    public int GetMaradtIdo()
    {
        return this.hatralevoIdo;
    }

    // Rövid hangjelzés kiadása, közben nő a hangjelzések száma.
    public void Csipog(int hosszu)
    {
        this.hangjelzesekSzama++;
        Console.WriteLine($"Riasztó csipog: {hosszu} ms");
    }

    // Hangos vészjelzés bekapcsolása.
    public void VeszjelzesInditasa()
    {
        Console.WriteLine("Riasztó VÉSZJELZÉS!");
    }

    // A kibocsátott hangjelzések számáról szóló összefoglaló.
    public string StatisztikatKeszit()
    {
        return $"Riasztó statisztika: Összes hangjelzés: {this.hangjelzesekSzama}";
    }

    // A riasztó nem értelmez szöveges parancsokat.
    public override void ParancsVegrehajtasa(string parancs)
    {
    }

    // A riasztó állapotának szöveges leírása.
    public override string AllapotJelents()
    {
        return "Riasztó aktív";
    }

    // A riasztó öntesztje mindig sikeres.
    protected override bool OnTesztFuttatasa()
    {
        return true;
    }
}
