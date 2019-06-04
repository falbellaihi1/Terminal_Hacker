using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class Hacker : MonoBehaviour
{
    int level; // Game state
    string Organization; // The name of organization being hacked
    string organizationPassword;
    int IncorrectTrys = 0;

    enum Screen { MainMenu, Password, Win};
 
    
    Screen currentScreen = Screen.MainMenu;
    void Start()
    {
        ShowMainMenu();
    }

    void ShowMainMenu()
    {
        currentScreen = Screen.MainMenu;
        Terminal.ClearScreen();
        Terminal.WriteLine("Who would you like to \n hack today?" +
            "\n 1) Hack bookstore" +
            "\n 2) Hack the PoliceStation" +
            "\n 3) Hack the NASA");
        Terminal.WriteLine("Enter your selection ");
        
    }

    void OnUserInput(string input)
    {

        if (input == "menu")
        {
            ShowMainMenu();
        }
        else if (currentScreen == Screen.MainMenu)
        {
            RunMainMenu(input);
        }
        else if (currentScreen == Screen.Password)
        {

            //Terminal.WriteLine(organizationPassword);
            CheckUserEntery(input);
        }
    }

     void RunMainMenu(string input)
    {
        bool isValidLevelNumber = (input == "1" || input == "2" || input == "3");
        // need refactring my code
        if (isValidLevelNumber)
        {
            level = int.Parse(input);
            TargetedOrganization();
            StartGame(Organization);
        }
        
        else if (input == "menu")
        {
            ShowMainMenu();
            
        }
        else
        {
            Terminal.WriteLine("Invalid choice");
        }
    }
    void TargetedOrganization()
    {


        switch (level)
        {
            case 1:
                Organization = " Bik BookStore ";

                break;
            case 2:
                Organization = " Police station ";
                break;
            default:
                Organization = " NASA ";
                break;
        }
    }

    void PasswordLevelPath()
    {
        string path;
        if (level == 1)
        {
            path = "Assets/Easy.txt";
        }
        else if (level == 2)
        {
            path = "Assets/Medium.txt";
        }
        else
        {
            path = "Assets/High.txt";
        }

        string[] lines = System.IO.File.ReadAllLines(path);


        int line = GenerateRandom(1, 4);
        organizationPassword = lines[line];


    }
    int GenerateRandom(int from, int to)
    {
        int number;
        System.Random rand = new System.Random();
        number = rand.Next(from, to);
        return number;
    }


    void StartGame(string org)
    {
        currentScreen = Screen.Password;
        PasswordLevelPath();
        Terminal.ClearScreen();
        Terminal.WriteLine("Connected to " + Organization + " servers" );
        Terminal.WriteLine("Please Enter your Password :");
    }

  // read password files from assets
 

    void Hints()
    {
        if(IncorrectTrys >= 0 && IncorrectTrys <= 2)
        {
            Terminal.WriteLine("You probably want to find a word that starts with a " + organizationPassword[0]);
        }
        else
        {
            Terminal.WriteLine("Hmmm.... second letter is ..." + organizationPassword[1]);
        }
    }
    void CheckUserEntery(string userGuesses)
    {
        if(userGuesses == organizationPassword)
        {
            Terminal.WriteLine("Sucessfully logged in password " + organizationPassword);
        }
        else
        {
            IncorrectTrys += 1;
            Hints();
            Terminal.WriteLine("Wrong Password!");
        }
    }

    // Update is called once per frame
    void Update()
    {

        
    }
}
