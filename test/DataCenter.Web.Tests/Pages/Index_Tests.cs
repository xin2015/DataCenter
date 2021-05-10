using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace DataCenter.Pages
{
    public class Index_Tests : DataCenterWebTestBase
    {
        [Fact]
        public async Task Welcome_Page()
        {
            var response = await GetResponseAsStringAsync("/");
            response.ShouldNotBeNull();
        }
    }
}
