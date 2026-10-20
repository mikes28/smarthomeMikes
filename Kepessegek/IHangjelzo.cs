namespace Okosotthon;

// Hang- és riasztási képesség: az eszköz tud csipogni, illetve vészjelzést indítani.
public interface IHangjelzo
{
    // Rövid hangjelzés kiadása a megadott hosszúságig (milliszekundum).
    void Csipog(int hosszu);

    // Hangos vészjelzés bekapcsolása.
    void VeszjelzesInditasa();
}
