namespace Projekt2.Helpers
{
    public static class FormatHelper
    {
        public static string FormatNip(string nip)
        {
            if (string.IsNullOrWhiteSpace(nip) || nip.Length != 10 || !nip.All(char.IsDigit))
                return nip; // Zwróć oryginalnie, jeśli niepoprawny format

            return $"{nip.Substring(0, 3)}-{nip.Substring(3, 2)}-{nip.Substring(5, 2)}-{nip.Substring(7, 3)}";
        }
    }

}
