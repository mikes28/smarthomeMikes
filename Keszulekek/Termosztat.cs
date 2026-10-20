using System.Globalization;

namespace Okosotthon;

// Fűtő, szabályozó okoseszköz.
// Két kiegészítő képességet valósít meg: időzítés (IIdozitheto) és statisztika (IStatisztika).
public class Termosztat : OkosEszkoz, IIdozitheto, IStatisztika
{
    // A "BEALLIT_HOMERSEKLET:24.5" alakú parancs eleje.
    private const string HomersekletParancs = "BEALLIT_HOMERSEKLET:";

    // A szobahőmérséklet, amit az eszköz induláskor mér.
    private const double MeresHomerseklet = 21.0;

    // Az önteszt csak ebben a tartományban sikeres.
    private const double AlsHomerseklet = 5.0;
    private const double FelsHomerseklet = 35.0;

    // Az időzítő hátralévő ideje, percekben.
    private int hatralevoIdo;

    public Termosztat(string azonosito, string nev, double celHomerseklet)
        : base(azonosito, nev)
    {
        this.CelHomerseklet = celHomerseklet;
        this.JelenlegiHomerseklet = MeresHomerseklet;
    }

    // A helyiségben mért aktuális hőmérséklet (°C).
    public double JelenlegiHomerseklet { get; private set; }

    // A beállított célhőmérséklet (°C).
    public double CelHomerseklet { get; private set; }

    // Az időzítő beállítása percekben.
    public void IdozitestBeallit(int perc)
    {
        this.hatralevoIdo = perc;
    }

    // Az időzítő hátralévő ideje (perc).
    public int GetMaradtIdo()
    {
        return this.hatralevoIdo;
    }

    // Hőmérsékleti adatok összefoglalója.
    public string StatisztikatKeszit()
    {
        return $"Termosztát statisztika: Jelenlegi {this.JelenlegiHomerseklet}°C, Cél: {this.CelHomerseklet}°C";
    }

    // A "BEALLIT_HOMERSEKLET:X" parancs új célhőmérsékletet állít be.
    // A hibás vagy ismeretlen parancsokat figyelmen kívül hagyjuk.
    public override void ParancsVegrehajtasa(string parancs)
    {
        if (!parancs.StartsWith(HomersekletParancs))
        {
            return;
        }

        // A parancs végéről levágjuk a számot, pl. "BEALLIT_HOMERSEKLET:24.5" -> "24.5".
        string ertek = parancs.Substring(HomersekletParancs.Length);

        // Mindig a ponttal írt számot olvassuk (24.5), függetlenül a számítógép beállításaitól.
        if (double.TryParse(ertek, NumberStyles.Float, CultureInfo.InvariantCulture, out double ujCelHomerseklet))
        {
            this.CelHomerseklet = ujCelHomerseklet;
        }
    }

    // Aktuális és célhőmérséklet szöveges leírása.
    public override string AllapotJelents()
    {
        return $"Jelenlegi hőmérséklet: {this.JelenlegiHomerseklet} °C, célhőmérséklet: {this.CelHomerseklet} °C";
    }

    // Az önteszt akkor sikeres, ha a célhőmérséklet 5 és 35 °C között van.
    protected override bool OnTesztFuttatasa()
    {
        return this.CelHomerseklet >= AlsHomerseklet && this.CelHomerseklet <= FelsHomerseklet;
    }
}
