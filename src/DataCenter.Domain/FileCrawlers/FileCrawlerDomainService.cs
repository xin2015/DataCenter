using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace DataCenter.FileCrawlers
{
    public class FileCrawlerDomainService : DomainService
    {
        protected IFileCrawlerRepository FileCrawlerRepository { get; set; }

        public FileCrawlerDomainService(IFileCrawlerRepository fileCrawlerRepository)
        {
            FileCrawlerRepository = fileCrawlerRepository;
        }

        public async Task CreateAsync([NotNull] FileCrawler fileCrawler)
        {
            Check.NotNull(fileCrawler, nameof(fileCrawler));

            FileCrawler existingFileCrawler = await FileCrawlerRepository.FindAsync(x => x.Code == fileCrawler.Code);
            if (existingFileCrawler != null)
            {
                throw new FileCrawlerAlreadyExistsException(fileCrawler.Code);
            }
        }

        public async Task ChangeCodeAsync([NotNull] FileCrawler fileCrawler, [NotNull] string newCode)
        {
            Check.NotNull(fileCrawler, nameof(fileCrawler));
            Check.NotNullOrWhiteSpace(newCode, nameof(newCode));

            FileCrawler existingFileCrawler = await FileCrawlerRepository.FindAsync(x => x.Code == newCode);
            if (existingFileCrawler == null)
            {
                fileCrawler.SetCode(newCode);
            }
            else
            {
                if (existingFileCrawler.Id != fileCrawler.Id)
                {
                    throw new FileCrawlerAlreadyExistsException(newCode);
                }
            }
        }
    }
}
