using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SupportApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SupportController : ControllerBase
    {
        [HttpGet()]
        public IEnumerable<SupportItem> GetSupportItems()
        {
            for(int i = 1; i <= 10; i++)
            {
                yield return new SupportItem
                {
                    Id = i,
                    Name = $"{Guid.NewGuid():D} - {i}"
                };

            }
        }
    }
}
