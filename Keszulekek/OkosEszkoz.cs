using System;

namespace Okosotthon;

// Az összes okoseszköz közös, absztrakt őse.
// Az eszközök innen öröklik az azonosítót, a nevet, a hálózati állapotot,
// és az öntesztelést végző sablonfüggvényt.
public abstract class OkosEszkoz
{
    public OkosEszkoz(string azonosito, string nev)
    {
        this.Azonosito = azonosito;
        this.Nev = nev;
        this.OnlineE = false;
        this.UtolsoFrissites = DateTime.Now;
    }

    // Az eszköz egyedi azonosítója (pl. TH-01).
    public string Azonosito { get; private set; }

    // Az eszköz megnevezése.
    public string Nev { get; private set; }

    // Igaz, ha az eszköz csatlakozva van a hálózathoz.
    public bool OnlineE { get; private set; }

    // Az utolsó frissítés időpontja.
    public DateTime UtolsoFrissites { get; protected set; }

    // Az eszköz hálózatra kapcsolódása.
    public void Csatlakozas()
    {
        this.OnlineE = true;
    }

    // A hálózati kapcsolat megszüntetése.
    public void KapcsolatBontasa()
    {
        this.OnlineE = false;
    }

    // Sablonfüggvény: először megnézzük, hogy online-e az eszköz,
    // és csak utána futtatjuk le az eszköz saját öntesztjét.
    public bool DiagnosztikaFuttatasa()
    {
        if (!this.OnlineE)
        {
            return false;
        }

        return this.OnTesztFuttatasa();
    }

    // Gyári beállítások visszaállítása (alapértelmezett implementáció).
    public virtual void GyariBeallitasokVisszaallitasa()
    {
        Console.WriteLine($"{this.Azonosito} - {this.Nev} eszközön alapértelmezett beállítások visszaállítása.");
    }

    // Az eszköz parancsait értelmező metódus.
    public abstract void ParancsVegrehajtasa(string parancs);

    // Az eszköz aktuális állapotának szöveges leírása.
    public abstract string AllapotJelents();

    // Az eszköz saját öntesztje, amit a DiagnosztikaFuttatasa hív meg.
    protected abstract bool OnTesztFuttatasa();
}
