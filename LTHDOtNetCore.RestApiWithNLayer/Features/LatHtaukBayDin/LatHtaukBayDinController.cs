using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LTHDOtNetCore.RestApiWithNLayer.Models;
using Newtonsoft.Json;

namespace LTHDOtNetCore.RestApiWithNLayer.Features.LatHtaukBayDin
{
    [Route("api/[controller]")]
    [ApiController]
    public class LatHtaukBayDinController : ControllerBase
    {
        private LatHtaukBayDinDTO _latHtaukBayDin;

        public LatHtaukBayDinController()
        {

        }

        private async Task<LatHtaukBayDinDTO> GetDataAsync()
        {
            string jsonStr = await System.IO.File.ReadAllTextAsync("MinTheinKhaData.json");
            var latHtaukBayDinDTO = JsonConvert.DeserializeObject<LatHtaukBayDinDTO>(jsonStr);
            return latHtaukBayDinDTO!;
        }

        [HttpGet("questions")]
        public async Task<IActionResult> GetQuestions()
        {
            var model = await GetDataAsync();
            return Ok(model.Questions);
        }

        [HttpGet]
        public async Task<IActionResult> GetNumberList()
        {
            var model = await GetDataAsync();
            return Ok(model.NumberList);
        }

        [HttpGet("{questionNo}/{no}")]
        public async Task<IActionResult> GetAnswer(int questionNo, int no)
        {
            var model = await GetDataAsync();
            var answer = model.Answers.FirstOrDefault(x => x.QuestionNo == questionNo && x.AnswerNo == no);
            if (answer is null)
            {
                return NotFound();
            }
            return Ok(answer);
        }
    }
}
