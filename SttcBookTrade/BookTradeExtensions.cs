using SttcBookTrade.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SttcBookTrade
{
#pragma warning disable CS1591
    public static class BookTradeExtensions
    {
        public static void EnsureSeedDataForContext(this BookTradeContext context)
        {
            if(context.Schools.Any())
            {
                return;
            }

            var schools = new List<School>()
            {
                new School()
                {
                    Name = "California State University Northridge",
                    Website = "csun.edu",

                    Users = new List<User>()
                    {
                        new User()
                        {
                            Username = "Guyperson",
                            Firstname = "Guy",
                            Lastname = "Person",
                            Email = "guy.person@my.csun.edu",
                            SchoolId = 1,
                            Profile = "I am a real person please trade all your books with me",
                            StudentIdentification = "213182235",
                            Password = "password",

                            Books = new List<Book>()
                            {
                                new Book()
                                {
                                    Name = "Introduction to Algorithms",
                                    Edition = "3rd",
                                    Author = "Thomas M, Cormen",
                                    ISBN13 = "978-0262033848",
                                    ISBN10 = "9780262033848",
                                    Price = 30,
                                    Condition = "Poor",
                                    Notes = "I used it as a coaster for 3 years take it away from me."
                                },
                                new Book()
                                {
                                    Name = " Data Structures",
                                    Edition = "12th",
                                    Author = "Koffman, Eliot",
                                    ISBN13 = "978-1119186540",
                                    ISBN10 = "1119186540",
                                    Price = 25,
                                    Condition = "Poor",
                                    Notes = " Still readable. Few missing pages. "
                                }
                            }
                        },
                        new User()
                        {
                            Username ="JasRay10",
                            Firstname ="Jason",
                            Lastname ="Ray",
                            Email ="jason.ray@my.csun.edu",
                            SchoolId = 1,
                            Profile = "Computer Science major, Junior",
                            StudentIdentification = "123345687",
                            Password = "password",
                            Books = new List<Book>()
                            {
                                new Book()
                                {
                                    Name = " Introduction to Algorithms",
                                    Edition = "3rd",
                                    Author = "Thomas M, Cormen",
                                    ISBN13 = "978-0262033848",
                                    ISBN10 = "0262033848",
                                    Price = 60,
                                    Condition = "Great",
                                    Notes = " Great Condition.Good care. "
                                },
                                new Book()
                                {
                                    Name = " Calculus",
                                    Edition = "8th",
                                    Author = "Stewart, James",
                                    ISBN13 = "978-1285740621",
                                    ISBN10 = "1285740621",
                                    Price = 80,
                                    Condition = "Poor",
                                    Notes = " 3 years old. Worn out a bit still useful. "
                                },
                                new Book()
                                {
                                    Name = " Data Structures",
                                    Edition = "12th",
                                    Author = "Koffman, Eliot",
                                    ISBN13 = "978-1119186540",
                                    ISBN10 = "1119186540",
                                    Price = 50,
                                    Condition = "Good",
                                    Notes = " Practically new. "
                                }
                            }
                        },
                        new User()
                        {
                            Username ="MariLo12",
                            Firstname ="Maria",
                            Lastname ="Lopez",
                            Email ="maria.lopez@my.csun.edu",
                            SchoolId = 1,
                            Profile = "BIOLOGY major ! Allbooks ready to go!",
                            StudentIdentification = "134526987",
                            Password = "password",

                            Books = new List<Book>()
                            {
                                new Book()
                                {
                                    Name = " Calculus",
                                    Edition = "8th",
                                    Author = "Stewart, James",
                                    ISBN13 = "978-1285740621",
                                    ISBN10 = "1285740621",
                                    Price = 80,
                                    Condition = "New",
                                    Notes = " Used for one semester only. "
                                },
                                new Book()
                                {
                                    Name = " Biology",
                                    Edition = "12th",
                                    Author = "Mader, Sylvia",
                                    ISBN13 = "978-0078024269",
                                    ISBN10 = "0078024269",
                                    Price = 100,
                                    Condition = "New",
                                    Notes = " Just used it one semester before calling quits. Take it away. "
                                }
                            }
                        },
                        new User()
                        {
                            Username ="RaySan33",
                            Firstname ="Raymond",
                            Lastname ="Sanchez",
                            Email ="raymond.sanchez@my.csun.edu",
                            SchoolId = 1,
                            Profile = "Electrical engeneering! all books for all 4 years ",
                            StudentIdentification = "154562589",
                            Password = "password",
                            Books = new List<Book>()
                            {
                                new Book()
                                {
                                    Name = " Introduction to Algorithms",
                                    Edition = "3rd",
                                    Author = "Thomas M, Cormen",
                                    ISBN13 = "978-0262033848",
                                    ISBN10 = "0262033848",
                                    Price = 45,
                                    Condition = "Good",
                                    Notes = " Just bought it last semester. Still good condition. "
                                },
                                new Book()
                                {
                                    Name = " Calculus",
                                    Edition = "8th",
                                    Author = "Stewart, James",
                                    ISBN13 = "978-1285740621",
                                    ISBN10 = "1285740621",
                                    Price = 40,
                                    Condition = "Good",
                                    Notes = " Some higlighted pages. Still readable. "
                                },

                                new Book()
                                {
                                    Name = " Biology",
                                    Edition = "12th",
                                    Author = "Mader, Sylvia",
                                    ISBN13 = "978-0078024269",
                                    ISBN10 = "0078024269",
                                    Price = 50,
                                    Condition = "Good",
                                    Notes = " Couple of highlighted pictures. "
                                }
                            }
                        },
                        new User()
                        {
                            Username ="JoyMerc24",
                            Firstname ="Joy",
                            Lastname ="Mercedez",
                            Email ="joy.mercedez@my.csun.edu",
                            SchoolId = 1,
                            Profile = "Performing arts major , Senior",
                            StudentIdentification = "165897435",
                            Password = "password",
                            Books = new List<Book>()
                            {
                                new Book()
                                {
                                    Name = " Calculus",
                                    Edition = "8th",
                                    Author = "Stewart, James",
                                    ISBN13 = "978-1285740621",
                                    ISBN10 = "1285740621",
                                    Price = 80,
                                    Condition = "New",
                                    Notes = " Used for one semester only. "
                                }
                            }
                        },
                        new User()
                        {
                            Username ="Bry2018",
                            Firstname ="Bryan",
                            Lastname = "Yang",
                            Email ="bryan.yang@my.csun.edu",
                            SchoolId = 1,
                            Profile = "Health and Science major",
                            StudentIdentification = "156587946",
                            Password = "password",
                            Books = new List<Book>()
                            {
                                new Book()
                                {
                                    Name = " Introduction to Algorithms",
                                    Edition = "3rd",
                                    Author = "Thomas M, Cormen",
                                    ISBN13 = "978-0262033848",
                                    ISBN10 = "0262033848",
                                    Price = 80,
                                    Condition = "New",
                                    Notes = " Honestly never used it. Practically new. "
                                }
                            }
                        }
                    }
                }
            };

            context.Schools.AddRange(schools);
            context.SaveChanges();
        }
    }
#pragma warning restore CS1591
}
