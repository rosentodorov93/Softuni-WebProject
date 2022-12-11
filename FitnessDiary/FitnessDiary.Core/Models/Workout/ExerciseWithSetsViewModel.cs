namespace FitnessDiary.Core.Models.Workout
{
    public class ExerciseWithSetsViewModel
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public int BodyPart { get; set; }
        public List<SetViewModel> Sets { get; set; } = null!;
    } 
}