namespace VetApp.Controllers.Results
{
    public class GetAppointmentResult
    {
        public int PetId { get; set; }

        public int DoctorId { get; set; }

        public string DateTime { get; set; }

        public DateTime Date { get; set; }

        public List<Pet> Pets { get; set; }

        public List<Doctor> Doctors { get; set; }
    }

    public class Pet
    {
        public int PetId { get; set; }

        public string PetName { get; set; }
    }

    public class Doctor
    {
        public int DoctorId { get; set; }

        public string DoctorName { get; set; }
    }
}
