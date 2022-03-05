namespace clinikpezeshki.Models.ViewModels
{
    public class PatientIndexVm
    {
        public string? Message { get; set; }
        public IEnumerable<PatientVm>? PatientsVm { get; set; }
    }
}
