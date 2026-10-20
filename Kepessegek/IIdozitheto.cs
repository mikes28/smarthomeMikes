namespace Okosotthon;

// Időzítési képesség: az eszköz képes egy időzítőt beállítani,
// és lekérdezni a hátralévő időt.
public interface IIdozitheto
{
    // Időzítő beállítása percekben.
    void IdozitestBeallit(int perc);

    // A hátralévő idő lekérdezése percekben.
    int GetMaradtIdo();
}
