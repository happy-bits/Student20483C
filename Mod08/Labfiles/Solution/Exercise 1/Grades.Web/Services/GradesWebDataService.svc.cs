/*
 OO: 1:  

    - skapa databas
    - lägg till denna service
    - referera till DataModel-projektet
    - referera till EF-assemblyt
    - lägg in connectionsträng i web.config
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Grades.DataModel;
using System.Data.Services;
using System.ServiceModel.Web;
using System.Data.Services.Common;

namespace Grades.Web.Services
{
    //TODO: Exercise 1: Task 2a: Replace the object keyword with your data source class name.
    public class GradesWebDataService : DataService<SchoolGradesDBEntities>
    {
        public static void InitializeService(DataServiceConfiguration config)
        {
            // OO: 3b (hela metoden)

            //TODO: Exercise 1: Task 2b: set rules to indicate which entity sets and service operations are visible, updatable, etc
            // Configure the StudentsInClass operation as read-only.
            config.SetServiceOperationAccessRule("StudentsInClass", ServiceOperationRights.AllRead); // OO: StudentsInClass-operationer får läsrättigheter

            // Configure all entity sets to permit read and write access.
            config.SetEntitySetAccessRule("Grades", EntitySetRights.All); // Grades-entityset får alla rättigheter
            config.SetEntitySetAccessRule("Teachers", EntitySetRights.All);
            config.SetEntitySetAccessRule("Students", EntitySetRights.All);
            config.SetEntitySetAccessRule("Subjects", EntitySetRights.All);
            config.SetEntitySetAccessRule("Users", EntitySetRights.All);

        }

        // OO: 3a
        [WebGet]
        public IEnumerable<Student> StudentsInClass(string className)
        {
            var students = from Student s in this.CurrentDataSource.Students
                           where String.Equals(s.Teacher.Class, className)
                           select s;
            return students;
        }
    }
}
