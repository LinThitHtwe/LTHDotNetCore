namespace LTHDOtNetCore.MyanmarProverbs.Models
{

    public class MyanmarProverbsModel
    {
        public MmProverbsTitle[] MmProverbsTitles { get; set; }
        public MmProverb[] MmProverbs { get; set; }
    }

    public class MmProverbsTitle
    {
        public int TitleId { get; set; }
        public string TitleName { get; set; }
    }

    public class MmProverb
    {
        public int TitleId { get; set; }
        public int ProverbId { get; set; }
        public string ProverbName { get; set; }
        public string ProverbDesp { get; set; }
    }

    public class MmProverbNamesResponseDTO
    {
        public int TitleId { get; set; }
        public int ProverbId { get; set; }
        public string ProverbName { get; set; }
    }

}
