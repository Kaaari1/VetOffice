namespace VetApp.Controllers.Results
{
    public class GetAppointmentResult
    {
        public int PetId { get; set; }
        public int DoctorId { get; set; }
        public string DateTime { get; set; }

        public DateTime Date { get; set; }
    }
}
