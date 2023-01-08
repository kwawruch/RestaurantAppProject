﻿using RestaurantAppProject.Models.People;
using RestaurantAppProject.Tools;
using SimpleHashing.Net;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAppProject.Services
{
    public class PersonService
    {
        public List<Person> People { get; set; }
        public static int PersonId { get; set; }

        public PersonService()
        {
            People = new List<Person>();
            PersonId = 1;
        }

        public Person LogIn()
        {
            var mail = Validator.String("[yellow]Insert your Email: [/]", 5, 50);
            var person = People.FirstOrDefault(p => p.Email == mail);
            if (person is null)
            {
                AnsiConsole.Markup("[red]Person with this email was not found[/]");
                Console.ReadKey();
                return null;
            }
            var password = AnsiConsole
                .Prompt(new TextPrompt<string>("[yellow]Enter your password: [/]")
                .PromptStyle("yellow")
            .Secret());

            ISimpleHash passwordHasher = new SimpleHash();
            bool isPasswordValid = passwordHasher.Verify(password, person.Password);

            if(!isPasswordValid)
            {
                AnsiConsole.Markup("[red]Wrong password[/]");
                Console.ReadKey();
                return null;
            }

            return person;
        }

        public void SingUp()
        {
            People.Add(new Client(

                Validator.String("Insert your Name: ", 3, 25),
                Validator.String("Insert your Surname: ", 3, 25),
                Validator.Date("Insert your birthdate (dd.mm.yyyy): "),
                Validator.String("Insert your Email: ", 5, 50),
                Validator.Password()
            ));
            AnsiConsole.Markup("[green]Account succesfully created[/]");
        }
    }
}
