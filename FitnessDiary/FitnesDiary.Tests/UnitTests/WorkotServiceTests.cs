using FitnessDiary.Core.Contracts;
using FitnessDiary.Core.Models.Workout;
using FitnessDiary.Core.Services;
using FitnessDiary.Infrastructure.Data.Common;
using FitnessDiary.Infrastructure.Data.WorkoutEntites;
using Microsoft.EntityFrameworkCore;

namespace FitnesDiary.Tests.UnitTests
{
    [TestFixture]
    public class WorkotServiceTests : UnitTestsBase
    {
        private IWorkoutService workoutService;
        private IRepository repo;

        [OneTimeSetUp]
        public void SetUp()
        {
            this.repo = new Repository(this.data);
            this.workoutService = new WorkoutService(repo);
        }

        [Test]
        public async Task CreateTamplateAsync_ShouldCreateAndAddNewTamplateCorrectly()
        {
            var model = new CreateWorkoutViewModel()
            {
                Name = "Lower body Workout",
                Description = "Quads, hams and calfs",
                Exercises = new ExerciseViewModel[]
                {
                    new ExerciseViewModel()
                    {
                        Name = "Squats",
                        BodyPart = "Quads",
                        SetCount = 4,
                    },
                     new ExerciseViewModel()
                    {
                        Name = "Leg Press",
                        BodyPart = "Hamstrings",
                        SetCount = 4,
                    },
                      new ExerciseViewModel()
                    {
                        Name = "Leg Curls",
                        BodyPart = "Glutes",
                        SetCount = 4,
                    },
                }
            };

            var tamplatesCountBeforeCreate = this.AppUser.WorkoutTamplates.Count;

            await workoutService.CreateTamplateAsync(model, this.AppUser.Id);

            var tamplatesCountAfterCreate = this.AppUser.WorkoutTamplates.Count;

            Assert.That(tamplatesCountAfterCreate.Equals(tamplatesCountBeforeCreate + 1));
            Assert.IsTrue(this.AppUser.WorkoutTamplates.Any(t => t.Name == model.Name));
            Assert.IsTrue(this.AppUser.WorkoutTamplates.Any(t => t.Description == model.Description));
        }
        [Test]
        public async Task EditTamplateAsync_ShouldEditCorrectly()
        {
            var exerciseIds = this.TestTamplate.Exercises.Select(e => e.Id).ToList();
            var editModel = new EditTamplateViewModel()
            {
                Id = this.TestTamplate.Id,
                Name = "Edited",
                Description = "Edited",
                Exercises = new List<EditExerciseViewModel>()

            };

            foreach (var id in exerciseIds)
            {
                editModel.Exercises.Add(new EditExerciseViewModel()
                {
                    Id = id,
                    Name = "Edited",
                    BodyPart = "Chest",
                    SetCount = 4,
                });
            }

            await workoutService.EditTamplateAsync(editModel);

            Assert.That(this.TestTamplate.Name.Equals(editModel.Name));
            Assert.That(this.TestTamplate.Description.Equals(editModel.Description));
            Assert.IsTrue(this.TestTamplate.Exercises.All(e => e.Name == "Edited"));
            Assert.IsTrue(this.TestTamplate.Exercises.All(e => e.SetCount == 4));


        }
        [Test]
        public async Task AddExerciseToTamplate_ShouldWorkCorrectly()
        {
            var exercisesCountBefore = this.TestTamplate.Exercises.Count;
            var exerciseModel = new AddExerciseModel()
            {
                WorkoutId = this.TestTamplate.Id,
                ExerciseName = "Chest Press",
                BodyPart = "Chest",
                SetCount = 3
            };

            await workoutService.AddExerciseToTamplateAsync(exerciseModel);
            var exerciseCountAfter = this.TestTamplate.Exercises.Count();

            Assert.That(exerciseCountAfter.Equals(exercisesCountBefore + 1));
            Assert.IsTrue(this.TestTamplate.Exercises.Any(e => e.Name == exerciseModel.ExerciseName));
            Assert.IsTrue(this.TestTamplate.Exercises.Any(e => e.SetCount == exerciseModel.SetCount));
            Assert.IsTrue(this.TestTamplate.Exercises.Any(e => e.BodyPart.ToString() == exerciseModel.BodyPart));
        }
        [Test]
        public async Task RemoveExerciseFromTamplate_ShouldWorkCorrectly()
        {
            var exerciseId = this.TestTamplate.Exercises.First().Id;
            var tamplateId = this.TestTamplate.Id;
            var exerciseCountBefore = this.TestTamplate.Exercises.Count;

            await workoutService.RemoveExerciseAsync(exerciseId, tamplateId);
            var exerciseCountAfter = this.TestTamplate.Exercises.Count;

            Assert.That(exerciseCountAfter.Equals(exerciseCountBefore - 1));
            Assert.IsFalse(this.TestTamplate.Exercises.Any(e => e.Id == exerciseId));
        }
        [Test]
        public async Task RemoveExerciseFromTamplate_ShouldNotRemoveWithInvalidExerciseId()
        {
            var exerciseId = "Invalid";
            var tamplateId = this.TestTamplate.Id;
            var exerciseCountBefore = this.TestTamplate.Exercises.Count;

            await workoutService.RemoveExerciseAsync(exerciseId, tamplateId);
            var exerciseCountAfter = this.TestTamplate.Exercises.Count;

            Assert.That(exerciseCountAfter.Equals(exerciseCountBefore));
        }
        [Test]
        public async Task GetMineTamplatesAsync_ShouldReturnCorrectData()
        {
            var expectedCount = this.AppUser.WorkoutTamplates.Count();
            var expectedWorkoutNames = string.Join(", ", this.AppUser.WorkoutTamplates.Select(t => t.Name).ToList());

            var result = await workoutService.GetMineTamplatesAsync(this.AppUser.Id);
            var resultNames = string.Join(", ", result.Select(r => r.Name).ToList());

            Assert.That(result.Count().Equals(expectedCount));
            Assert.That(resultNames.Equals(expectedWorkoutNames));
        }
        [Test]
        public async Task GetTemplateById_ShouldReturnCorrectTamplate()
        {
            var result = await workoutService.GetTamplateById(this.TestTamplate.Id);

            Assert.IsNotNull(result);
            Assert.That(result.Id.Equals(this.TestTamplate.Id));
            Assert.That(result.Name.Equals(this.TestTamplate.Name));
            Assert.That(result.Description.Equals(this.TestTamplate.Description));
            Assert.That(result.Exercises.Count.Equals(this.TestTamplate.Exercises.Count));
        }
        [Test]
        public async Task GetTamplateForDiaryByIdAsync_ShouldReturnCorrectTamplate()
        {
            var result = await workoutService.GetTamplateForDiaryByIdAsync(this.TestTamplate.Id);

            Assert.IsNotNull(result);
            Assert.That(result.Name.Equals(this.TestTamplate.Name));
            Assert.That(result.Description.Equals(this.TestTamplate.Description));
            Assert.That(result.Exercises.Count.Equals(this.TestTamplate.Exercises.Count));
        }
        [Test]
        public async Task TamlateExistsById_ShouldReturnTrueWithValidId()
        {
            var result = await workoutService.TamplateExistsByIdAsync(this.TestTamplate.Id);

            Assert.IsTrue(result);
        }
        [Test]
        public async Task TamplateExistsById_ShouldReturnFalseWithInvalidId()
        {
            var result = await workoutService.TamplateExistsByIdAsync("Invalid");

            Assert.IsFalse(result);
        }
        [Test]
        public async Task AddToDiary_ShouldAddWorkoutCorrectly()
        {
            var model = await workoutService.GetTamplateForDiaryByIdAsync(this.TestTamplate.Id);


            await workoutService.AddToDiaryAsync(model, this.AppUser.Id);

            var workout = this.AppUser.Diary.Last().Workout;
            Assert.IsNotNull(workout);
            Assert.That(workout.Name.Equals(model.Name));
            Assert.That(workout.Description.Equals(model.Description));
        }
        [Test]
        public async Task WorkoutExistsById_ShouldReturnTrueWithValidId()
        {
            var workoutId = repo.AllReadonly<Workout>().First().Id;
            var result = await workoutService.WorkoutExistsByIdAsync(workoutId);

            Assert.IsTrue(result);
        }
        [Test]
        public async Task WorkoutExistsById_ShouldReturnFalseWithInvalidId()
        {
            var result = await workoutService.WorkoutExistsByIdAsync("Invalid");

            Assert.IsFalse(result);
        }
        [Test]
        public async Task EditWorkoutAsync_ShouldEditCorrectly()
        {
            var workoutBeforeEdit = repo.AllReadonly<Workout>().Include(w => w.Exercises).ThenInclude(e => e.Sets).First();
            var model = new WorkoutViewModel()
            {
                Id = workoutBeforeEdit.Id,
                Name = workoutBeforeEdit.Name,
                Description = workoutBeforeEdit.Description,
                Exercises = workoutBeforeEdit.Exercises.Select(e => new ExerciseWithSetsViewModel()
                {
                    Id = e.Id,
                    Name = e.Name,
                    BodyPart = (int)e.BodyPart,
                    Sets = e.Sets.Select(s => new SetViewModel()
                    {
                        Load = 1,
                        Reps = 1
                    }).ToList()
                }).ToList()
            };

            await workoutService.EditWorkoutAsync(model);
            var workoutAfterEdit = repo.AllReadonly<Workout>().Include(w => w.Exercises).ThenInclude(e => e.Sets).First();

            Assert.IsTrue(workoutAfterEdit.Exercises[0].Sets.All(s => s.Reps == 1 && s.Load == 1));

        }
        [Test]
        public async Task EditWorkoutAsync_ShouldNotEditWithInvalidId()
        {
            var workoutBeforeEdit = repo.AllReadonly<Workout>().Include(w => w.Exercises).ThenInclude(e => e.Sets).First();
            var model = new WorkoutViewModel()
            {
                Id = "Invalid",
                Name = workoutBeforeEdit.Name,
                Description = workoutBeforeEdit.Description,
                Exercises = workoutBeforeEdit.Exercises.Select(e => new ExerciseWithSetsViewModel()
                {
                    Id = e.Id,
                    Name = e.Name,
                    BodyPart = (int)e.BodyPart,
                    Sets = e.Sets.Select(s => new SetViewModel()
                    {
                        Load = 2,
                        Reps = 2
                    }).ToList()
                }).ToList()
            };

            await workoutService.EditWorkoutAsync(model);
            var workoutAfterEdit = repo.AllReadonly<Workout>().Include(w => w.Exercises).ThenInclude(e => e.Sets).First();

            Assert.IsTrue(workoutAfterEdit.Exercises[0].Sets.All(s => s.Reps == 1 && s.Load == 1));
        }
    }
}
