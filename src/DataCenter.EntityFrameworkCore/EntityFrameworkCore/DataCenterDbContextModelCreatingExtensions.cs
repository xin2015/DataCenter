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

                b.Property(x => x.Code).IsRequired().HasMaxLength(DataCenterSharedConsts.MaxCodeLength);
                b.Property(x => x.Name).IsRequired().HasMaxLength(DataCenterSharedConsts.MaxNameLength);
                b.Property(x => x.UrlFormat).IsRequired().HasMaxLength(DataCenterSharedConsts.MaxFullNameLength);
                b.Property(x => x.FileNameFormat).IsRequired().HasMaxLength(DataCenterSharedConsts.MaxNameLength);
                b.Property(x => x.StampFormat).IsRequired().HasMaxLength(DataCenterSharedConsts.MaxNameLength);
                b.Property(x => x.Periods).IsRequired().HasMaxLength(DataCenterSharedConsts.MaxJsonStringLength);
                b.Property(x => x.Parameters).IsRequired().HasMaxLength(DataCenterSharedConsts.MaxFullJsonStringLength);
            });

            builder.Entity<ParameterCombination>(b =>
            {
                b.ToTable(DataCenterConsts.DbTablePrefix + "ParameterCombinations", DataCenterConsts.DbSchema);
                b.ConfigureByConvention(); //auto configure for the base class props

                b.Property(x => x.Periods).IsRequired().HasMaxLength(DataCenterSharedConsts.MaxJsonStringLength);
                b.Property(x => x.Parameters).IsRequired().HasMaxLength(DataCenterSharedConsts.MaxJsonStringLength);

                b.HasOne<FileCrawler>().WithMany().HasForeignKey(x => x.FileCrawlerId).IsRequired();
            });

            builder.Entity<FileCrawlerRecord>(b =>
            {
                b.ToTable(DataCenterConsts.DbTablePrefix + "FileCrawlerRecords", DataCenterConsts.DbSchema);
                b.ConfigureByConvention(); //auto configure for the base class props

                b.Property(x => x.Url).IsRequired().HasMaxLength(DataCenterSharedConsts.MaxFullNameLength);
                b.Property(x => x.DirectoryName).IsRequired().HasMaxLength(DataCenterSharedConsts.MaxNameLength);
                b.Property(x => x.FileName).IsRequired().HasMaxLength(DataCenterSharedConsts.MaxNameLength);
                b.Property(x => x.Stamp).IsRequired().HasMaxLength(DataCenterSharedConsts.MaxNameLength);

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