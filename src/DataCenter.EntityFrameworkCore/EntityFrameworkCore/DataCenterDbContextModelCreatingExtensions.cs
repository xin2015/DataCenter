using DataCenter.FileCrawlers;
using DataCenter.FileCrawlers.FileCrawlerRecords;
using DataCenter.FileCrawlers.ParameterCombinations;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace DataCenter.EntityFrameworkCore
{
    public static class DataCenterDbContextModelCreatingExtensions
    {
        public static void ConfigureDataCenter(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            /* Configure your own tables/entities inside here */

            builder.Entity<FileCrawler>(b =>
            {
                b.ToTable(DataCenterConsts.DbTablePrefix + "FileCrawlers", DataCenterConsts.DbSchema);
                b.ConfigureByConvention(); //auto configure for the base class props

                b.Property(x => x.Code).IsRequired().HasMaxLength(DataCenterConsts.MaxCodeLength);
                b.Property(x => x.Name).IsRequired().HasMaxLength(DataCenterConsts.MaxNameLength);
                b.Property(x => x.UrlFormat).IsRequired().HasMaxLength(DataCenterConsts.MaxFullNameLength);
                b.Property(x => x.FileNameFormat).IsRequired().HasMaxLength(DataCenterConsts.MaxNameLength);
                b.Property(x => x.StampFormat).IsRequired().HasMaxLength(DataCenterConsts.MaxNameLength);
                b.Property(x => x.Periods).IsRequired().HasMaxLength(DataCenterConsts.MaxJsonStringLength);
                b.Property(x => x.Parameters).IsRequired().HasMaxLength(DataCenterConsts.MaxFullJsonStringLength);
            });

            builder.Entity<ParameterCombination>(b =>
            {
                b.ToTable(DataCenterConsts.DbTablePrefix + "ParameterCombinations", DataCenterConsts.DbSchema);
                b.ConfigureByConvention(); //auto configure for the base class props

                b.Property(x => x.Periods).IsRequired().HasMaxLength(DataCenterConsts.MaxJsonStringLength);
                b.Property(x => x.Parameters).IsRequired().HasMaxLength(DataCenterConsts.MaxJsonStringLength);

                b.HasOne<FileCrawler>().WithMany().HasForeignKey(x => x.FileCrawlerId).IsRequired();
            });

            builder.Entity<FileCrawlerRecord>(b =>
            {
                b.ToTable(DataCenterConsts.DbTablePrefix + "FileCrawlerRecords", DataCenterConsts.DbSchema);
                b.ConfigureByConvention(); //auto configure for the base class props

                b.Property(x => x.Url).IsRequired().HasMaxLength(DataCenterConsts.MaxFullNameLength);
                b.Property(x => x.DirectoryName).IsRequired().HasMaxLength(DataCenterConsts.MaxNameLength);
                b.Property(x => x.FileName).IsRequired().HasMaxLength(DataCenterConsts.MaxNameLength);
                b.Property(x => x.Stamp).IsRequired().HasMaxLength(DataCenterConsts.MaxNameLength);

                b.HasOne<ParameterCombination>().WithMany().HasForeignKey(x => x.ParameterCombinationId).IsRequired();
            });

            //builder.Entity<>(b =>
            //{
            //    b.ToTable(DataCenterConsts.DbTablePrefix + "", DataCenterConsts.DbSchema);
            //    b.ConfigureByConvention(); //auto configure for the base class props
            //});
        }
    }
}