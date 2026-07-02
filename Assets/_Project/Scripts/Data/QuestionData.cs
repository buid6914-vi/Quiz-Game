using System;
using System.Collections.Generic;

[Serializable]
public class QuestionData
{
    public int category;
    public string question;
    public string[] answers;
    public int correctIndex;

    public string questionText => question;
}

[Serializable]
public class QuestionList
{
    public List<QuestionData> questions;
}