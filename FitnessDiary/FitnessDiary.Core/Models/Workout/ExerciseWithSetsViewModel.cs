namespace FitnessDiary.Core.Models.Workout
{
    public class ExerciseWithSetsViewModel
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string BodyPart { get; set; } = null!;
        public List<SetViewModel> Sets { get; set; } = null!;
    } 
}