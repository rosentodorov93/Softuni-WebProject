namespace FitnessDiary.Core.Models.Workout
{
    public class AddExerciseModel
    {
        public string WorkoutId { get; set; }
        public string ExerciseName { get; set; }
        public string BodyPart { get; set; }
        public string SetCount { get; set; }
    }
}
