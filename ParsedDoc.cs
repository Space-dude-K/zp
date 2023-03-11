namespace Zp
{
    class ParsedDoc
    {
        public enum ExportFormat
        {
            Png,
            Pdf
        }
        private string fio = "Null";
        public string Fio { get => fio; set => fio = value; }
        private string date = "Null";
        public string Date { get => date; set => date = value; }
        private string totalSummPayout = "Null";
        public string TotalSummPayout { get => totalSummPayout; set => totalSummPayout = value; }
        private string totalSummOrganizationDebt = "Null";
        public string TotalSummOrganizationDebt { get => totalSummOrganizationDebt; set => totalSummOrganizationDebt = value; }
        private string personalAccount = "Null";
        public string PersonalAccount { get => personalAccount; set => personalAccount = value; }
        private string personalNumber = "Null";
        public string PersonalNumber { get => personalNumber; set => personalNumber = value; }
        private string email = "Null";
        public string Email { get => email; set => email = value; }

        private string imageLocation = "Null";
        public string ImageLocation { get => imageLocation; set => imageLocation = value; }


        string topLeftCoordinate;
        public string TopLeftCoordinate { get => topLeftCoordinate; set => topLeftCoordinate = value; }
        string botRightCoordinate;
        public string BotRightCoordinate { get => botRightCoordinate; set => botRightCoordinate = value; }
    }
}