using Microsoft.EntityFrameworkCore;
using SampleProject.Database.Entity;

namespace SampleProject.Database.SeedData
{
    // seeding data into the contact table
    public class SeedContactData
    {
        public static void Seeddata(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>()
            .HasData(new Contact
            {
                Id = 101110,
                FirstName = "John",
                LastName = "Doe",
                Email = "john@example.com",
                Gender = "Male",
                Body = "Dummy Message 1",
                PhoneNumber = "123-456-7890",
                Reason = "Issue",
            },
            new Contact
            {
                Id = 101111,
                FirstName = "Jane",
                LastName = "Smith",
                Email = "jane@example.com",
                Gender = "Female ",
                Body = "Dummy Message 2",
                PhoneNumber = "987-654-3210",
                Reason = "Issue"
            },
            new Contact
            {
                Id = 101112,
                FirstName = "Alice",
                LastName = "Johnson",
                Email = "alice@example.com",
                Gender = "Male",
                Body = "Dummy Message 3",
                PhoneNumber = "555-555-5555",
                Reason = "Issue"
            });
        }
    }
}
