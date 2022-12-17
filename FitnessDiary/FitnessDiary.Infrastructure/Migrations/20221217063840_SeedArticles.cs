﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessDiary.Infrastructure.Data.Migrations
{
    public partial class SeedArticles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "Author", "CategoryId", "Content", "Date", "ImageUrl", "IsActive", "Title" },
                values: new object[,]
                {
                    { "7d719eff-24e9-4628-9291-d8a878a2b004", "Admin", "dd454378-f80d-4380-b6fd-ff592b4aca4d", "If you are past the beginner’s stage and want to gain muscle, one of the best body part splits you can use to accomplish this is the push/pull/legs split routine.\\n'+\r\n                                '\\n'+\r\n                                'The push/pull/legs split is one of the simplest, most enduring and popular workout routines there is. And it’s also extremely effective; assuming of course that it’s done right.\\n'+\r\n                                '\\n'+\r\n                                'So in this blog post I’ll explain what a push/pull/legs split involves and why it’s an effective way to train. And I’ll also give you a sample workout routine that you can get started with in the gym.\\n'+\r\n                                'What Is The Push/Pull/Legs Split Routine?\\n'+\r\n                                '\\n'+\r\n                                'The push/pull/legs split is a very simple training method in which you split your body into three parts. And each part is then trained on its own separate day.\\n'+\r\n                                '\\n'+\r\n                                'In the “push” workout you train all the upper body pushing muscles, i.e. the chest, shoulders and triceps.\\n'+\r\n                                '\\n'+\r\n                                'In the “pull” workout you train all the upper body pulling muscles, i.e. the back and biceps.\\n'+\r\n                                '\\n'+\r\n                                'And in the “legs” workout you train the entire lower body, i.e. the quads, hamstrings, calves and abdominals.\\n'+\r\n                                '\\n'+\r\n                                'These three workouts are then alternated over however many weekly training sessions you choose to do.\\n'+\r\n                                '\\n'+\r\n                                'So for instance if you can only make it to the gym three days per week, you would simply do each workout on its own set day once each week, e.g. Monday, Wednesday and Friday. However this is not the best way to do it as it means that each body part is only being trained once per week, and as I’ve said previously this is not optimal for muscle growth (though it’s fine for a maintenance program).\\n'+\r\n                                '\\n'+\r\n                                'So a better way would be to train four days per week, alternating the workouts over your four training sessions. It doesn’t matter which days you choose as long as you never do more than two days in a row.\\n'+\r\n                                '\\n'+\r\n                                'Another method is the rotating five day cycle, where each workout is done over a five day period. So this means you would train 2 on, 1 off, 1 on, 1 off and then repeat. This is probably the best way to do it as it means that each body part is trained once every 5 days – and this is about ideal for the more experienced trainee. But it does mean that your training days are constantly changing so you need a fairly flexible schedule for this to work.\\n'+\r\n                                'Why Use A Push/Pull/Legs Split?\\n'+\r\n                                '\\n'+\r\n                                'The push/pull/legs split is probably the most efficient workout split there is because all related muscle groups are trained together in the same workout.\\n'+\r\n                                '\\n'+\r\n                                'This means that you get the maximum overlap of movements within the same workout, and the muscle groups being trained get an overall benefit from this overlap.\\n'+\r\n                                '\\n'+\r\n                                'For example when you train chest with say bench press, you are also hitting your anterior deltoids and triceps hard. And when you train shoulders you are again involving your triceps. So it makes sense to work these all together in the same workout for maximum synergy and effectiveness.\\n'+\r\n                                '\\n'+\r\n                                'Similarly when you train your back your biceps are heavily involved, so it again makes sense to train these immediately afterwards so that they can derive the maximum benefit from the additional stimulation.\\n'+\r\n                                '\\n'+\r\n                                'It also means you will have minimum overlap of movements between workouts, and this will facilitate better recovery than most other body part splits.\\n'+\r\n                                'Who Should Use A Push/Pull/Legs Split?\\n'+\r\n                                '\\n'+\r\n                                'The push/pull/legs split is ideal for both the intermediate and advanced trainee.\\n'+\r\n                                '\\n'+\r\n                                'More specifically though, if you are just starting out or have not had much in the way of results from your efforts so far, you’ll almost certainly do best with a full body workout routine, training three days per week. Stick with this for at least six months – more if you are still progressing well.\\n'+\r\n                                '\\n'+\r\n                                'Once you hit the intermediate stage however you’ll probably find you’ll do better with an upper/lower split routine training three or four days per week. And this is in fact one of the best ways to train for the vast majority of the population.\\n'+\r\n                                '\\n'+\r\n                                'But at any time past the beginner stage you may find the push/pull/legs split suits you better. Or you may wish to alternate upper/lower splits with a push/pull/legs split in order to derive all the benefits that each has to offer.\\n'+\r\n                                '\\n'+\r\n                                'Either way the push/pull/legs split is an extremely effective method of training that is certain to give you exceptional results if you apply yourself to it diligently.\\n'+\r\n                                'A Sample Push/Pull/Legs Split Routine\\n'+\r\n                                '\\n'+\r\n                                'Here’s a great sample workout plan that is well structured and properly balanced; and it’s sure to give you exceptional results…\\n'+\r\n                                'Workout 1 – Push\\n'+\r\n                                '\\n'+\r\n                                'Bench Press 3 X 5 – 7\\n'+\r\n                                'Seated Dumbbell Shoulder Press 3 X 6 – 8\\n'+\r\n                                'Incline Dumbbell Press 3 X 8 – 10\\n'+\r\n                                'Side Lateral Raises 2 X 10 – 12\\n'+\r\n                                'Triceps Pressdowns 2 X 8 – 10\\n'+\r\n                                'Overhead Triceps Extension 2 X 8 – 10\\n'+\r\n                                'Workout 2 – Pull\\n'+\r\n                                '\\n'+\r\n                                'Bent-over Row 3 X 5 – 7\\n'+\r\n                                'Pull Ups 3 X 6 – 8\\n'+\r\n                                'Barbell Shrugs 3 X 8 – 10\\n'+\r\n                                'Face Pulls 2 X 10 – 12\\n'+\r\n                                'Barbell Curl 2 X 8 – 10\\n'+\r\n                                'Dumbbell Hammer Curl 2 X 8 – 10\\n'+\r\n                                'Workout 3 – Legs/Abs\\n'+\r\n                                '\\n'+\r\n                                'Squats 3 X 6 – 8\\n'+\r\n                                'Romanian Deadlifts 2 X 8 – 10\\n'+\r\n                                'Leg Press 2 X 10 – 12\\n'+\r\n                                'Leg Curl 2 X 10 – 12\\n'+\r\n                                'Calf Raise 4 X 8 – 10\\n'+\r\n                                'Hanging Leg Raise 2 X 10 – 15\\n'+\r\n                                '\\n'+\r\n                                'The sets listed are your work sets. Always warm up properly first in order to prepare your body for the heavier work, and to help prevent injury. However another advantage of this split routine is that fewer warm-up sets are required as training each exercise/body part warms you up for the next.'", new DateTime(2022, 12, 17, 8, 38, 39, 568, DateTimeKind.Local).AddTicks(2339), "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSY7kG4LXehM-SlLJJIwiY16xCn3VIrei5kQw&usqp=CAU", true, "Healthy Eating" },
                    { "a418bcad-ebf9-4922-808a-6268c2bb3ee2", "Admin", "c4763ddf-d44c-41e2-b2eb-2d9885cddcd0", "Eating a healthy diet is not about strict limitations, staying unrealistically thin, or depriving yourself of the foods you love. Rather, it’s about feeling great, having more energy, improving your health, and boosting your mood.By using these simple tips, you can cut through the confusion and learn how to create—and stick to—a tasty, varied, and nutritious diet that is as good for your mind as it is for your body.While some extreme diets may suggest otherwise, we all need a balance of protein, fat, carbohydrates, fiber, vitamins, and minerals in our diets to sustain a healthy body. You don't need to eliminate certain categories of food from your diet, but rather select the healthiest options from each category.Protein gives you the energy to get up and go—and keep going—while also supporting mood and cognitive function.Too much protein can be harmful to people with kidney disease, but the latest research suggests that many of us need more high-quality protein, especially as we age.That doesn't mean you have to eat more animal products—a variety of plant-based sources of protein each day can ensure your body gets all the essential protein it needs.Fat.Not all fat is the same. While bad fats can wreck your diet and increase your risk of certain diseases, good fats protect your brain and heart. In fact, healthy fats—such as omega - 3s—are vital to your physical and emotional health. Including more healthy fat in your diet can help improve your mood, boost your well - being, and even trim your waistline.Fiber.Eating foods high in dietary fiber(grains, fruit, vegetables, nuts, and beans) can help you stay regular and lower your risk for heart disease, stroke, and diabetes.It can also improve your skin and even help you to lose weight.Calcium.As well as leading to osteoporosis, not getting enough calcium in your diet can also contribute to anxiety, depression, and sleep difficulties.Whatever your age or gender, it's vital to include calcium-rich foods in your diet, limit those that deplete calcium, and get enough magnesium and vitamins D and K to help calcium do its job.Carbohydrates are one of your body's main sources of energy. But most should come from complex, unrefined carbs (vegetables, whole grains, fruit) rather than sugars and refined carbs. Cutting back on white bread, pastries, starches, and sugar can prevent rapid spikes in blood sugar, fluctuations in mood and energy, and a build-up of fat, especially around your waistline.", new DateTime(2022, 12, 17, 8, 38, 39, 568, DateTimeKind.Local).AddTicks(2335), "https://www.helpguide.org/wp-content/uploads/calories-counting-diet-food-control-and-weight-loss-concept-calorie-768.jpg", true, "Healthy Eating" },
                    { "b3df26fc9770424f9915c930d359fa09", "Admin", "351a06c6-9c12-45d4-9cd1-c9ff5db75212", "Exercising regularly, every day if possible, is the single most important thing you can do for your health. In the short term, exercise helps to control appetite, boost mood, and improve sleep. In the long term, it reduces the risk of heart disease, stroke, diabetes, dementia, depression, and many cancers.Whether you were once much more physically active or have never been one to exercise regularly, now is a great time to start an exercise and fitness regimen. Getting and staying in shape is just as important for seniors as it is for younger people.The benefits of exercise on mental health are well documented. For example, one major study found that sedentary people are 44% more likely to be depressed. Another found that those with mild to moderate depression could get similar results to those obtained through antidepressants just by exercising for 90 minutes each week. The key appears to be the release of brain chemicals such as serotonin and dopamine, which help lift mood and combat stress.", new DateTime(2022, 12, 17, 8, 38, 39, 568, DateTimeKind.Local).AddTicks(2303), "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSbWHiBti3UenQIRzTJ03BATIcUowBOPZSqyA&usqp=CAU", true, "Exercise and Fitness" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02b52032-ec58-496e-b58e-0533479ff27d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "197f679d-edcb-483c-af5f-cbd50c605b24", "AQAAAAEAACcQAAAAEFGBciFcOTQ4v1ma7Zu1pl5V6b6tys7Byq+wTDNyZV4Y4K7TE4okomaGNyLPlh/4Hg==", "038adb9f-0896-40ee-ae98-8343bb34d2e6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cf28b02f-bcd9-4464-9100-6343cc8ca939",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4e2d9178-3b82-4931-b8ec-908d4600ed35", "AQAAAAEAACcQAAAAEPrPW7Yt90YpA0ZHdCczFsIUKI7cuhPVT/hnPRkRlnYxS5/6lPGAzh4xyJsmUU8R7A==", "86caf1d7-0d16-434a-b376-0736c21fd98d" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: "7d719eff-24e9-4628-9291-d8a878a2b004");

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: "a418bcad-ebf9-4922-808a-6268c2bb3ee2");

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: "b3df26fc9770424f9915c930d359fa09");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02b52032-ec58-496e-b58e-0533479ff27d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8dfb20fc-5fcc-44ac-889e-1cb51aae45bf", "AQAAAAEAACcQAAAAECPlNtHzm0OSrBgAaelrs0C/O+v73Zc22lMkqpG8v/aZVy0PEPK328B9Ko5oCtzNHw==", "fd68a1ca-ac02-4e49-9aed-4ef67f33abc5" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cf28b02f-bcd9-4464-9100-6343cc8ca939",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "51fbda83-3567-43d1-9ea2-11cbac86de17", "AQAAAAEAACcQAAAAEKOe7OYtaQZRQpRknLcegEnadoZmBwLsOjSyEX46iq05kk0L2OF0C+8KDcHDOUankw==", "f173fc14-2ddf-4763-af51-01ed0ae41323" });
        }
    }
}
