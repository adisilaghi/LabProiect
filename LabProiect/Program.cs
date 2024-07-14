using LabProiect;
using LabProiect.Models;
using Microsoft.EntityFrameworkCore;

using (var context = new StudentContext())
{
    context.Database.EnsureCreated();

    var studentManager = new StudentMng();

    studentManager.AddStudent("Aaron", "Paul", 20, new Address { City = "NewYork", Street = "New street 1", Number = "2" }, context);
    studentManager.AddStudent("Jake", "Peralta", 22, new Address { City = "LasVegas", Street = "Old street 2", Number = "1" }, context);
    studentManager.AddStudent("Stephanos", "Kasper", 24, new Address { City = "Ankara", Street = "Lavern Highway", Number = "5" }, context);
    studentManager.AddStudent("Kuzman", "Ninoslav", 25, new Address { City = "Sydney", Street = "Grant Locks", Number = "3" }, context);

    Console.WriteLine("All Students:");
    var allStudents = studentManager.GetAllStudents(context);
    foreach (var student in allStudents)
    {
        studentManager.PrintStudentDetails(student);
    }

    int studentId = 6;
    Console.WriteLine($"\nStudent with ID {studentId}:");
    var studentById = studentManager.GetStudentById(studentId, context);
    if (studentById != null)
    {
        studentManager.PrintStudentDetails(studentById);
    }

    Console.WriteLine($"\nUpdating Student with ID {studentId}:");
    studentManager.UpdateStudent(studentId, "John", "Smith", 21, context);
    studentById = studentManager.GetStudentById(studentId, context);
    if (studentById != null)
    {
        studentManager.PrintStudentDetails(studentById);
    }

    Console.WriteLine($"\nAddress of Student with ID {studentId}:");
    var addressByStudentId = studentManager.GetAddressByStudentId(studentId, context);
    if (addressByStudentId != null)
    {
        Console.WriteLine($"City: {addressByStudentId.City}, Street: {addressByStudentId.Street}, Number: {addressByStudentId.Number}");
    }

    Console.WriteLine($"\nUpdating Address of Student with ID {studentId}:");
    studentManager.UpdateStudentAddress(studentId, new Address { City = "CityC", Street = "StreetC", Number = "3" }, context);
    studentById = studentManager.GetStudentById(studentId, context);
    if (studentById != null && studentById.Address != null)
    {
        studentManager.PrintStudentDetails(studentById);
    }

   /* Console.WriteLine($"\nDeleting Student with ID {studentId} (including address):");
    studentManager.DeleteStudent(studentId, true, context);*/

    allStudents = studentManager.GetAllStudents(context);
    Console.WriteLine("All Students after deletion:");
    foreach (var student in allStudents)
    {
        studentManager.PrintStudentDetails(student);
    }
}