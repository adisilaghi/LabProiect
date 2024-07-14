using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabProiect.Models;
using Microsoft.EntityFrameworkCore;

namespace LabProiect
{
    public class StudentMng
    {
        public void AddStudent(string firstName, string lastName, int age, Address address, StudentContext context)
        {
            var existingStudent = context.Students
                .FirstOrDefault(s => s.FirstName == firstName && s.LastName == lastName && s.Age == age);

            if (existingStudent == null)
            {
                var student = new Student { FirstName = firstName, LastName = lastName, Age = age, Address = address };
                context.Students.Add(student);
                context.SaveChanges();
                Console.WriteLine($"Student Created: {firstName} {lastName}, Age: {age}, Address: {address.City} {address.Street} {address.Number}");
            }
            else
            {
                Console.WriteLine($"Student {firstName} {lastName}, Age: {age} already exists.");
            }
        }

        public List<Student> GetAllStudents(StudentContext context)
        {
            return context.Students.Include(s => s.Address).ToList();
        }

        public Student GetStudentById(int id, StudentContext context)
        {

            return context.Students.Include(s => s.Address).FirstOrDefault(s => s.Id == id);
        }

        public void UpdateStudent(int id, string firstName, string lastName, int age, StudentContext context)
        {
            var student = context.Students.Find(id);
            if (student != null)
            {
                student.FirstName = firstName;
                student.LastName = lastName;
                student.Age = age;
                context.SaveChanges();
                Console.WriteLine($"Student Updated: {firstName} {lastName}, Age: {age}");
            }
            else
            {
                Console.WriteLine($"Student with ID {id} not found.");
            }
        }

        public Address GetAddressByStudentId(int studentId, StudentContext context)
        {
            var student = context.Students.Include(s => s.Address).FirstOrDefault(s => s.Id == studentId);
            return student?.Address;
        }

        public void UpdateStudentAddress(int studentId, Address newAddress, StudentContext context)
        {
            var student = context.Students.Include(s => s.Address).FirstOrDefault(s => s.Id == studentId);
            if (student != null)
            {
                if (student.Address == null)
                {
                    student.Address = newAddress;
                    context.Addresses.Add(newAddress);
                }
                else
                {
                    student.Address.City = newAddress.City;
                    student.Address.Street = newAddress.Street;
                    student.Address.Number = newAddress.Number;
                }
                context.SaveChanges();
                Console.WriteLine($"Address Updated for Student ID {studentId}: {newAddress.City} {newAddress.Street} {newAddress.Number}");
            }
            else
            {
                Console.WriteLine($"Student with ID {studentId} not found.");
            }
        }

        public void DeleteStudent(int id, bool deleteAddress, StudentContext context)
        {
            var student = context.Students.Include(s => s.Address).FirstOrDefault(s => s.Id == id);
            if (student != null)
            {
                if (deleteAddress && student.Address != null)
                {
                    context.Addresses.Remove(student.Address);
                    Console.WriteLine($"Address Deleted for Student ID {id}");
                }
                context.Students.Remove(student);
                context.SaveChanges();
                Console.WriteLine($"Student Deleted: ID {id}");
            }
            else
            {
                Console.WriteLine($"Student with ID {id} not found.");
            }
        }

        public void PrintStudentDetails(Student student)
        {
            Console.WriteLine($"ID: {student.Id}, Name: {student.FirstName} {student.LastName}, Age: {student.Age}, Address: {student.Address?.City} {student.Address?.Street} {student.Address?.Number}");
        }

    }
}
