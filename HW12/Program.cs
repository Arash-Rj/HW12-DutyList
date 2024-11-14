using HW12.Entities;
using HW12.Enum;
using HW12.Interface;
using HW12.Service;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

DutyService dutyService = new DutyService();
UserService userService = new UserService();
bool isfinished = false;
do
{
    Console.Clear();
    Console.WriteLine("*****Welcome To The store*****");
    Console.WriteLine("1.Login");
    Console.WriteLine("2.Sign up");
    Console.WriteLine("3.Logout");
    Console.WriteLine("******************************");
    int choice = 0;
    try
    {
        choice = Int32.Parse(Console.ReadLine());
    }
    catch (FormatException)
    {
        Console.Clear();
        Console.WriteLine("Invalid format entered.Try again.");
        Console.WriteLine("Press any key...");
        Console.ReadKey();
    }
    switch (choice)
    {
        case 1:
            Console.Clear();
            Login();
            Console.ReadKey();
            break;
        case 2:
            Console.Clear();
            Register();
            Console.ReadKey();
            break;
        case 3:
            Console.Clear();
            userService.Logout();
            isfinished = true;
            break;
    }
} while (!isfinished);
void Login()
{
    Console.Clear();
    Console.Write("Please enter your email: ");
    var email = Console.ReadLine();
    Console.Write("Please enter your password: ");
    var password = Console.ReadLine();
    var res = userService.Login(email,password);
    Console.WriteLine(res.Message);
    if (res.IsDone == true)
    { menu(userService.onlineUser); }
    Console.WriteLine("Press any key...");
}
void Register()
{
    Console.Write("Please enter your name: ");
    var name = Console.ReadLine();
    Console.Write("Please enter your email: ");
    var email = Console.ReadLine();
    Console.Write("Please enter your password: ");
    var password = Console.ReadLine();
    var res = userService.Register(name,email,password);
    Console.WriteLine(res.Message);
    if (res.IsDone == true)
    {
        Console.WriteLine("1.Advance to menu     2.Peace out");
        int res1 = 0;
        try
        { res1 = Int32.Parse(Console.ReadLine()); }
        catch (FormatException)
        {
            Console.Clear();
            Console.WriteLine("Invalid format entered.Try again.");
            Console.WriteLine("Press any key...");
        }
        if (res1 == 1)
        {
            Login();
        }
    }
    else
    {
        Console.WriteLine("Press any key...");
    }
}
void menu(User user)
{
    do
    {
        bool IsNOTDone = false;
        Console.Clear();
        Console.WriteLine("******Welcome******");
        Console.WriteLine("1.Add New Duty.");
        Console.WriteLine("2.Remove Duty.");
        Console.WriteLine("3.List Of Duties.");
        Console.WriteLine("4.Edit Duty.");
        Console.WriteLine("5.Search Duty.");
        Console.WriteLine("6.Change Duty Status.");
        Console.WriteLine("7.Peace out.");
        if (!Int32.TryParse(Console.ReadLine(), out int res))
        {
            Console.WriteLine("Invalid format please try again.");
        }
        do
        {
            switch (res)
            {
                case 1:
                    Console.Clear();
                    NewDutyMenu(out Duty duty, out bool isNOTDone);
                    duty.UserId = user.Id;
                    IsNOTDone = isNOTDone;
                    try
                    {
                        Console.WriteLine(dutyService.AddDuty(duty).Message);
                    }
                    catch (ArgumentNullException)
                    {
                        Console.WriteLine("Null Can not be entered on any feilds, try again.");
                        IsNOTDone = true;
                    }
                    break;
                case 2:
                    Console.Clear();
                    DutyList(user);
                    Console.Write("Enter Duty Id to remove: ");
                    if (!int.TryParse(Console.ReadLine(), out int id))
                    {
                        Console.WriteLine("Invalid format entered, please try again.");
                        IsNOTDone = true;
                        break;
                    }
                    var isdeleted = dutyService.DeleteDuty(id);
                    Console.WriteLine(isdeleted.Message);
                    if (!isdeleted.IsDone)
                    {
                        IsNOTDone = true;
                    }
                    break;
                case 3:
                    Console.Clear();
                    DutyList(user);
                    break;
                case 4:
                    Console.Clear();
                    DutyList(user);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("Enter Duty Id: ");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    var DutyId = int.Parse(Console.ReadLine());
                    var GotDuty = dutyService.GetDuty(DutyId);
                    NewDutyMenu(out Duty duty1, out bool isNOTDone1, GotDuty);
                    IsNOTDone = isNOTDone1;
                    try
                    {
                        Console.WriteLine(dutyService.EditDuty().Message);
                    }
                    catch (ArgumentNullException)
                    {
                        Console.WriteLine("Null Can not be entered on any feilds, try again.");
                        IsNOTDone = true;
                    }
                    break;
                case 5:
                    Console.Clear();
                    Console.WriteLine("Enter duty Title: ");
                    string DutyTitle = Console.ReadLine();
                    var foundduty = dutyService.SearchUserDuty(DutyTitle,user);
                    if (foundduty == null)
                    {
                        Console.WriteLine("There is no such duty, please enter the title carefuly.");
                        IsNOTDone = true;
                    }
                    Console.WriteLine(foundduty.ToString());
                    break;
                case 6:
                    Console.Clear();
                    DutyList(user);
                    Console.Write("Enter duty id in order to change state: ");
                    if (!int.TryParse(Console.ReadLine(), out int id1))
                    {
                        Console.WriteLine("Invalid format entered, please try again.");
                        IsNOTDone = true;
                        break;
                    }
                    Console.WriteLine($"States: 1.{StateEnum.Done.ToString()}   " +
                    $"2.{StateEnum.Cancelled.ToString()}  " +
                    $"3.{StateEnum.Pending.ToString()}");
                    Console.Write("Enter the new state number: ");
                    var statnumber = int.Parse(Console.ReadLine());
                    var result2 = GetEnum(statnumber, out StateEnum state);
                    if (!result2.IsDone)
                    {
                        Console.WriteLine(result2.Message);
                        IsNOTDone = true;
                        break;
                    }
                    var newres = dutyService.ChangeDutyStat(id1, state);
                    Console.WriteLine(newres.Message);
                    if (newres.IsDone == false)
                    {
                        IsNOTDone = true;
                        break;
                    }
                    break;
                case 7:
                    isfinished = true;
                    IsNOTDone= false;
                    break;
            }
            if (IsNOTDone == false)
            {
                Console.WriteLine("=======================");
                Console.Write("1.Continue...   2.Exit? ");
                var choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        IsNOTDone = true;
                        break;
                    case 2:
                        break;
                    default:
                        Console.WriteLine("Wrong input.But will be perceived as Exit.");
                        Console.WriteLine("Press any key...");
                        Console.ReadKey();
                        break;
                }
            }
            else
            {
                Console.WriteLine("Press any key to try again.");
                Console.ReadKey();
            }
        } while (IsNOTDone);
        Console.WriteLine("Press any key...");
        Console.ReadKey();
    } while (!isfinished);
}
Result ConvertToDate(string Date,out DateTime date)
{
    date = DateTime.MinValue;
    bool isparsed = DateTime.TryParse(Date, out date);
    if(!isparsed)
    {
        return new Result(false, "Invalid date format.please try again.");
    }
    return new Result(true);
}
Result GetEnum(int num,out StateEnum state )
{
    state = new StateEnum();
    switch(num)
    {
        case 1:
            state = StateEnum.Done;
            return new Result(true);
        case 2:
            state = StateEnum.Cancelled;
            return new Result(true);
        case 3:
            state = StateEnum.Pending;
            return new Result(true);
    }
    return new Result(false, "Status number is wrong.");
}
void DutyList(User user)
{
    try
    {
        Console.WriteLine("*****List of Duties*****");
        dutyService.GetDutyList().Where(d => d.UserId == user.Id).ToList().ForEach(d => Console.WriteLine($"Id: {d.Id} ---" + d.ToString()));
        Console.WriteLine("========================");
    }
    catch(ArgumentNullException ex)
    { Console.WriteLine(ex.Message); }
}
void NewDutyMenu(out Duty duty,out bool IsNOTDone,Duty duty1=null)
{
    IsNOTDone = false;
    duty = null;
    Console.Write("Enter duty title: ");
    string title = Console.ReadLine();
    Console.WriteLine("---------------");
    Console.Write("Enter duty order: ");
    int order = int.Parse(Console.ReadLine());
    Console.WriteLine("---------------");
    Console.Write("Enter duty date (yyyy-mm-dd HH:MM) : ");
    var result = ConvertToDate(Console.ReadLine(), out DateTime dt);
    Console.WriteLine("---------------");
    if (result.IsDone == false)
    {
        Console.WriteLine(result.Message);
        IsNOTDone = true;
    }
    Console.WriteLine($"States: 1.{StateEnum.Done.ToString()}   " +
        $"2.{StateEnum.Cancelled.ToString()}  " +
        $"3.{StateEnum.Pending.ToString()}");
    Console.Write("Enter duty status number: ");
    int enumnumber = int.Parse(Console.ReadLine());
    Console.WriteLine("---------------");
    var result2 = GetEnum(enumnumber, out StateEnum state);
    if (result2.IsDone == false)
    {
        Console.WriteLine(result2.Message);
        IsNOTDone = true;
    }
    if (duty1 == null)
    {
        duty = new Duty(title, order, dt, state);
    }
    else
    {
        duty1.order = order;
        duty1.State = state;
        duty1.DueDate = dt;
        duty1.Title = title;
    }
}

