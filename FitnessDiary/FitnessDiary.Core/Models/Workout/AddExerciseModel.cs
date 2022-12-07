namespace FitnessDiary.Core.Models.Workout
{
    public class AddExerciseModel
    {
        public string WorkoutId { get; set; } = null!;
        public string ExerciseName { get; set; } = null!;
        public string BodyPart { get; set; } = null!;
        public int SetCount { get; set; } 
    }
}
