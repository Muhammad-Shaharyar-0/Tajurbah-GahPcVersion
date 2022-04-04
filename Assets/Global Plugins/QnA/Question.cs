using System;
using System.Collections;
using System.Collections.Generic;

public enum anstype{
    numeric = 1,
    mcq = 2,
    boolean = 3,
    short_text = 4,
    long_text = 5,
    mrq = 6
}

public class Opts{
    public string[] options;
    public Opts(int OptCount){
        options = new string[OptCount];
    }

    public int count(){
        return options.Length;
    }
    public string GetOpts(){
        string allOptions = "";
        foreach (string opt in options){
            allOptions += ", " + opt;
        }
        return allOptions;
    }
}

public class Question 
{
    public int no {get; set;}
    public string q {get; set;}
    public anstype type {get; set;}
    public string ans {get; set;}
    public float ans_min {get; set;}
    public float ans_max {get; set;}
    public bool[] ans_multiple_responses{get; set;}
    public bool in_series {get; set;}
    public int prev_q {get; set;}
    public int next_q {get; set;}

    public Opts ans_opts {get; set;}
}

