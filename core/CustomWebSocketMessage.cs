using System;
using System.Collections.Generic;
using System.Linq;
namespace QuizApp.Core.core
{
    public class UserAnswerQuestionSocketMessage
    {
        public bool IsAnswerCorrect { get; set; }
        public string Username { get; set; }
    }
    public class NewQuestionSocketMessage
    {
        public DateTime NewQuestionDate { get; set; }

        public bool LastQuestionIsAnswerCorrect { get; set; }
        public QuestionModel NewQuestion { get; set; }
        public string Username { get; set; }
    }
    public class QuestionModel
    {
        public String QuestionText { get; set; }
        public List<AnswerModel> NewQuestionAnswers { get; set; }

    }
    public class AnswerModel
    {
        public string AnswerText { get; set; }
        public bool IsCorrect { get; set; }
    }
    public class QuestionService
    {
    
        static List<QuestionModel> questions = new List<QuestionModel>(){
            new QuestionModel(){QuestionText="Galatasaray UEFA kupasını ne zaman aldı?",
            NewQuestionAnswers=new List<AnswerModel>(){
            new AnswerModel(){AnswerText="17 May 2000",IsCorrect=true},
            new AnswerModel(){AnswerText="14 May 2000",IsCorrect=false},}},

            new QuestionModel(){QuestionText="Galatasaray kaç kez şampiyon oldu?", 
            NewQuestionAnswers=new List<AnswerModel>(){
            new AnswerModel(){AnswerText="23",IsCorrect=false},
            new AnswerModel(){AnswerText="22",IsCorrect=true},
         }},

            new QuestionModel(){QuestionText="Fenerbahçe kaç kez şampiyon oldu?", 
            NewQuestionAnswers=new List<AnswerModel>(){
            new AnswerModel(){AnswerText="19",IsCorrect=true},
            new AnswerModel(){AnswerText="18",IsCorrect=false},}},

            new QuestionModel(){QuestionText="Beşiktaş kaç kez şampiyon oldu?",
            NewQuestionAnswers=new List<AnswerModel>(){
            new AnswerModel(){AnswerText="15",IsCorrect=true},
            new AnswerModel(){AnswerText="16",IsCorrect=false},}},

            new QuestionModel(){QuestionText="Trabzon kaç kez şampiyon oldu?",
            NewQuestionAnswers=new List<AnswerModel>(){
            new AnswerModel(){AnswerText="5",IsCorrect=false},
            new AnswerModel(){AnswerText="6",IsCorrect=true},}},
            
            new QuestionModel(){QuestionText="Cumhuriyet ne zaman ilan edildi?",
            NewQuestionAnswers=new List<AnswerModel>(){
            new AnswerModel(){AnswerText="23 Nisan 1920",IsCorrect=false},
            new AnswerModel(){AnswerText="29 Ekim 1923",IsCorrect=true},}},

            new QuestionModel(){QuestionText="TBMM ne zaman ilan edildi?",
            NewQuestionAnswers=new List<AnswerModel>(){
            new AnswerModel(){AnswerText="23 Nisan 1920",IsCorrect=true},
            new AnswerModel(){AnswerText="29 Ekim 1923",IsCorrect=false},}},
            
            new QuestionModel(){QuestionText="Türkiye'de kaç il vardır?",
            NewQuestionAnswers=new List<AnswerModel>(){
            new AnswerModel(){AnswerText="80",IsCorrect=false},
            new AnswerModel(){AnswerText="81",IsCorrect=true},
            }},

            new QuestionModel(){QuestionText="Türkiye'de kaç ilçe vardır?",
            NewQuestionAnswers=new List<AnswerModel>(){
            new AnswerModel(){AnswerText="922",IsCorrect=true},
            new AnswerModel(){AnswerText="923",IsCorrect=false},}},

            new QuestionModel(){QuestionText="Bir yıl kaç saatir?",
            NewQuestionAnswers=new List<AnswerModel>(){
            new AnswerModel(){AnswerText="8767",IsCorrect=false},
            new AnswerModel(){AnswerText="8766",IsCorrect=true},}},
        };
        public static QuestionModel GetRandomQuestion()
        {
            return questions.OrderBy(f => Guid.NewGuid()).FirstOrDefault();
        }
    }
}