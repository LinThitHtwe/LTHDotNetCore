namespace LTHDOtNetCore.RestApiWithNLayer.Models
{

    public class LatHtaukBayDinDTO
    {
        public Question[] Questions { get; set; }
        public Answer[] Answers { get; set; }
        public string[] NumberList { get; set; }
    }

    public class Question
    {
        public int QuestionNo { get; set; }
        public string QuestionName { get; set; }
    }

    public class Answer
    {
        public int QuestionNo { get; set; }
        public int AnswerNo { get; set; }
        public string AnswerResult { get; set; }
    }
}
