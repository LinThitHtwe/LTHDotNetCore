using Newtonsoft.Json;
using System.Reflection;

Console.WriteLine("Program Started");
Console.WriteLine("--------------");

string jsonString = await File.ReadAllTextAsync("MinTheinKhaData.json");
var minTheinKhaDTO = JsonConvert.DeserializeObject<MinTheinKhaDTO>(jsonString);

foreach (var question in minTheinKhaDTO!.questions)
{
    Console.WriteLine(question.questionNo);
}

Console.ReadLine();

public class MinTheinKhaDTO
{
    public Question[] questions { get; set; }
    public Answer[] answers { get; set; }
    public string[] numberList { get; set; }
}

public class Question
{
    public int questionNo { get; set; }
    public string questionName { get; set; }
}

public class Answer
{
    public int questionNo { get; set; }
    public int answerNo { get; set; }
    public string answerResult { get; set; }
}