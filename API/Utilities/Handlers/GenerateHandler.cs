using System.Xml.Linq;

namespace API.Utilities.Handler;

public class GenerateHandler // Class untuk menghandle generate NIK baru
{
    public static string Nik(string? nik = null)
    {
        if (nik is null) // Jika NIK terakhir belum ada, maka NIK baru akan dimulai dari 111111
            return "111111";

        var generateNik = int.Parse(nik) + 1; // Jika NIK terakhir sudah ada, maka NIK baru akan ditambahkan 1

        return generateNik.ToString(); // Mengembalikan NIK baru dalam bentuk string
    }

    public static string Invoice(string? invoice = null)
    {
        if (invoice is null)
        {
            int currentYear = DateTime.Now.Year;
            return $"TRS-{currentYear}-0001";
        }
        string getYear = invoice.Substring(4, 4);
        string getNumber = invoice.Substring(9, 4);
        int numberInt = int.Parse(getNumber);
        numberInt++;
        string newNumberString = numberInt.ToString("D4");
        return $"TRS-{getYear}-{newNumberString}";

    }
}