using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitnessDiary.Infrastructure.Data.Configuration
{
    internal class ArticleCategoryConfiguration : IEntityTypeConfiguration<ArticleCategory>
    {
        public void Configure(EntityTypeBuilder<ArticleCategory> builder)
        {
            builder.HasData(
                new ArticleCategory()
                {
                    Id = "c4763ddf-d44c-41e2-b2eb-2d9885cddcd0",
                    Name = "Nutrition"
                },
                new ArticleCategory()
                {
                    Id = "dd454378-f80d-4380-b6fd-ff592b4aca4d",
                    Name = "Training and Tecniques"
                },
                new ArticleCategory()
                {
                    Id = "351a06c6-9c12-45d4-9cd1-c9ff5db75212",
                    Name = "Lifestyle"
                }
                );
        }
    }
}
