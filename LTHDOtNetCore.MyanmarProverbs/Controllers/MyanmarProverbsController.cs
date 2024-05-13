using LTHDOtNetCore.MyanmarProverbs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace LTHDOtNetCore.MyanmarProverbs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyanmarProverbsController : ControllerBase
    {

        [HttpGet("ProverbsTitles")]
        public async Task<IActionResult> GetAllMMProverbsTitlesAsync()
        {
            var mmProverbs = JsonConvert.DeserializeObject<MyanmarProverbsModel>(await GetJsonDataAsync());
            return Ok(mmProverbs!.MmProverbsTitles);
        }

        [HttpGet("ProverbNames")]
        public async Task<IActionResult> GetAllProverbNamesAsync()
        {
            var mmProverbs = JsonConvert.DeserializeObject<MyanmarProverbsModel>(await GetJsonDataAsync());
            var proverbNames = mmProverbs!.MmProverbs.Select(mmProverb => new MmProverbNamesResponseDTO()
            {
                ProverbId = mmProverb.ProverbId,
                ProverbName = mmProverb.ProverbName,
                TitleId = mmProverb.TitleId,
            }).ToList();

            return Ok(proverbNames);
        }

        [HttpGet("ProverbNames/Title/{titleId}")]
        public async Task<IActionResult> GetProverbNamesByTitleId(int titleId)
        {
            var mmProverbs = JsonConvert.DeserializeObject<MyanmarProverbsModel>(await GetJsonDataAsync());
            var proverbNames = mmProverbs!.MmProverbs
                              .Where(mmProverb => mmProverb.TitleId == titleId)
                              .Select(mmProverb => new MmProverbNamesResponseDTO()
                              {
                                  ProverbId = mmProverb.ProverbId,
                                  ProverbName = mmProverb.ProverbName,
                                  TitleId = mmProverb.TitleId,
                              }).ToList();
            return Ok(proverbNames);
        }

        [HttpGet("ProverbDetail/{proverbId}")]
        public async Task<IActionResult> GetProverbDetailByPorverbId(int proverbId)
        {
            var mmProverbs = JsonConvert.DeserializeObject<MyanmarProverbsModel>(await GetJsonDataAsync());
            var proverbDetail = mmProverbs!.MmProverbs
                              .FirstOrDefault(mmProverb => mmProverb.ProverbId == proverbId);
                              
            return Ok(proverbDetail);
        }

        private static async Task<string> GetJsonDataAsync()
        {
            string jsonString = await System.IO.File.ReadAllTextAsync("MyanmarProverbs.json");
            return jsonString;
        }
    }
}
