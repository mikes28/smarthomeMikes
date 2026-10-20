namespace Okosotthon;

// Az okosotthon központja: az eszközöket egy listában tartja,
// és a közös műveleteket (csatlakoztatás, diagnosztika, riasztás, statisztika) rajtuk végzi.
public class OkosotthonKozpont
{
    // A regisztrált eszközök listája.
    private readonly List<OkosEszkoz> eszkozok = new List<OkosEszkoz>();

    // Egy új eszköz hozzáadása a központ eszközlistájához.
    public void EszkozHozzaadasa(OkosEszkoz eszkoz)
    {
        this.eszkozok.Add(eszkoz);
    }

    // Az összes regisztrált eszköz hálózatra kapcsolása.
    public void OsszesCsatlakoztatasa()
    {
        foreach (OkosEszkoz eszkoz in this.eszkozok)
        {
            eszkoz.Csatlakozas();
        }
    }

    // Végigmegy az eszközökön, és visszaadja, hány eszköz öntesztje sikerült.
    public int RendszerDiagnosztikaFuttatasa()
    {
        int sikeresTesztek = 0;

        foreach (OkosEszkoz eszkoz in this.eszkozok)
        {
            if (eszkoz.DiagnosztikaFuttatasa())
            {
                sikeresTesztek++;
            }
        }

        return sikeresTesztek;
    }

    // Az összes hangjelző képességgel rendelkező eszköz vészjelzését indítja el.
    // Az "is" operátorral kiválogatjuk a listából a hangjelző eszközöket.
    public void OsszesHangjelzoAktivasa()
    {
        foreach (OkosEszkoz eszkoz in this.eszkozok)
        {
            if (eszkoz is IHangjelzo hangjelzo)
            {
                hangjelzo.VeszjelzesInditasa();
            }
        }
    }

    // Az összes statisztikai képességgel rendelkező eszköz összefoglalóját kiírja.
    // Az "is" operátorral kiválogatjuk a listából a statisztikát készítő eszközöket.
    public void StatisztikakLekerdezese()
    {
        foreach (OkosEszkoz eszkoz in this.eszkozok)
        {
            if (eszkoz is IStatisztika statisztika)
            {
                Console.WriteLine(statisztika.StatisztikatKeszit());
            }
        }
    }
}
