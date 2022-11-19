namespace FitnessDiary.Core.Models.Workout
{
    public class ExerciseWithSetsViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string BodyPart { get; set; }
        public List<SetViewModel> Sets { get; set; }
    }
}