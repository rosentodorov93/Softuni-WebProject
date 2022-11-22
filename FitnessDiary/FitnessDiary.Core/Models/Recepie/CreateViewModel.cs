using FitnessDiary.Core.Models.Food;

namespace FitnessDiary.Core.Models.Recepie
{
    public class CreateViewModel
    {
        public string Name { get; set; } = null!;
        public int ServingsSize { get; set; }
        public int Unit { get; set; }
        public string UserId { get; set; } = null!;

    }
}
