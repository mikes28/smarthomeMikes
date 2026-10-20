namespace Okosotthon;

// Biztonsági okoseszköz: PIN kóddal nyitható, és a hibás próbálkozásokra
// csipogással, majd vészjelzéssel válaszol.
public class OkosZar : OkosEszkoz, IHangjelzo, IStatisztika
{
    // A "NYITAS:1234" alakú parancs eleje.
    private const string NyitasParancs = "NYITAS:";

    // A zár bezárását végző parancs.
    private const string ZarasParancs = "ZARAS";

    // A gyári beállítások visszaállításakor beállított PIN kód.
    private const string GyariPinKod = "0000";

    // Ennyi sikertelen nyitási kísérlet után indul a vészjelzés.
    private const int RiasztasiKuszob = 3;

    // A nyitáshoz szükséges PIN kód – szigorúan privát adat.
    private string pinKod;

    // A sikertelen nyitási kísérletek száma.
    private int sikertelenNyitasok;

    // A sikeres nyitások száma.
    private int sikeresNyitasok;

    public OkosZar(string azonosito, string nev, string pinKod)
        : base(azonosito, nev)
    {
        this.pinKod = pinKod;
        this.ZartE = true;
    }

    // Igaz, ha a zár lezárt állapotban van.
    public bool ZartE { get; private set; }

    // Rövid hangjelzés kiadása.
    public void Csipog(int hosszu)
    {
        Console.WriteLine($"Csipogás: {hosszu} ms");
    }

    // Hangos vészjelzés bekapcsolása.
    public void VeszjelzesInditasa()
    {
        Console.WriteLine("VÉSZJELZÉS INDÍTVA!");
    }

    // A nyitásokról szóló összefoglaló.
    public string StatisztikatKeszit()
    {
        return $"OkosZár statisztika: Sikeres nyitások: {this.sikeresNyitasok}, Sikertelen nyitási kísérletek: {this.sikertelenNyitasok}";
    }

    // A "NYITAS:PIN" parancs kinyitja a zárat, ha a PIN egyezik;
    // a "ZARAS" parancs bezárja. Hibás PIN esetén csipogás indul.
    public override void ParancsVegrehajtasa(string parancs)
    {
        if (parancs.StartsWith(NyitasParancs))
        {
            // A parancs végéről levágjuk a PIN kódot, pl. "NYITAS:1234" -> "1234".
            string megadottPinKod = parancs.Substring(NyitasParancs.Length);

            if (megadottPinKod == this.pinKod)
            {
                this.ZartE = false;
                this.sikeresNyitasok++;
                return;
            }

            // Hibás PIN: csipogunk, és a küszöbön túl vészjelzést adunk.
            this.sikertelenNyitasok++;
            this.Csipog(500);

            if (this.sikertelenNyitasok >= RiasztasiKuszob)
            {
                this.VeszjelzesInditasa();
            }
        }
        else if (parancs == ZarasParancs)
        {
            this.ZartE = true;
        }
    }

    // A zár állapotának szöveges leírása.
    public override string AllapotJelents()
    {
        return this.ZartE ? "ZÁRVA" : "NYITVA";
    }

    // A zár öntesztje mindig sikeres.
    protected override bool OnTesztFuttatasa()
    {
        return true;
    }

    // A PIN visszaáll a gyári értékre, és a zár bezáródik.
    public override void GyariBeallitasokVisszaallitasa()
    {
        base.GyariBeallitasokVisszaallitasa();

        this.pinKod = GyariPinKod;
        this.ZartE = true;
    }
}
