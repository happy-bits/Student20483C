using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using School.Data;

// OO: EDMX-filen är en XML som definierar en Entity Data Model. Beskriver databasschemat och kopplingen till modellen. (Innehåller också info hur den ska visas visuellt)
namespace School
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Connection to the School database
        private SchoolDBEntities schoolContext = null;

        // Field for tracking the currently selected teacher
        private Teacher teacher = null;

        // List for tracking the students assigned to the teacher's class
        private IList studentsInfoAAAA = null;

        #region Predefined code

        // OO: 1) Vid start
        public MainWindow()
        {
            InitializeComponent();
        }

        // OO: 2) Vid start

        // Connect to the database and display the list of teachers when the window appears
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            // OO: Connection mot SchoolDB i SQLExpress. Connectionsträng finns i App.config
            this.schoolContext = new SchoolDBEntities();

            /* OO:
              
                Kan även hämta alla studenter:
                var test = this.schoolContext.Students.ToList();

                Komponenten (namnet) definieras i XAML

                    <ComboBox x:Name="teachersListXXXXXXXXX" />


            */

            teachersListXXXXXXXXX.DataContext = this.schoolContext.Teachers;
        }

        /*
         
           OO: 3) Körs en gång vid start
        
           Anrop gör från comboboxen

           <ComboBox SelectionChanged="teachersList_SelectionChangedddddddd" />
         
         */
        // When the user selects a different teacher, fetch and display the students for that teacher

        private void teachersList_SelectionChangedddddddd(object sender, SelectionChangedEventArgs e)
        {
            // OO: Plocka ut vald lärare (tack vara att vi fyllt datan ovan med DataContext)
            // Find the teacher that has been selected
            this.teacher = teachersListXXXXXXXXX.SelectedItem as Teacher;            

            // OO: Funkar utan denna så lite oklart vad som händer. Laddar läraren med studenter
            this.schoolContext.LoadProperty<Teacher>(this.teacher, s => s.Students);

            // OO: Variablen används bara här
            // OO: Kan i detta fall skriva
            //     studentsList.DataContext = teacher.Students

            // Find the students for this teacher
            this.studentsInfoAAAA = ((IListSource)teacher.Students).GetList();

            // Use databinding to display these students
            studentsList.DataContext =  this.studentsInfoAAAA;
        }

        #endregion

        // When the user presses a key, determine whether to add a new student to a class, remove a student from a class, or modify the details of a student
        private void studentsList_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                // If the user pressed Enter, edit the details for the currently selected student
                case Key.Enter: Student student = this.studentsList.SelectedItem as Student;

                    // Use the StudentsForm to display and edit the details of the student
                    StudentForm sf = new StudentForm();

                    // Set the title of the form and populate the fields on the form with the details of the student           
                    sf.Title = "Edit Student Details";
                    sf.firstName.Text = student.FirstName;
                    sf.lastName.Text = student.LastName;
                    sf.dateOfBirth.Text = student.DateOfBirth.ToString("d"); // Format the date to omit the time element

                    // OO: Exekveringen stannar först när fönstret stängs
                    // Display the form
                    if (sf.ShowDialog().Value)
                    {
                        // When the user closes the form, copy the details back to the student
                        student.FirstName = sf.firstName.Text;
                        student.LastName = sf.lastName.Text;


                        // OO: Ändrat koden för att slippa fel. Tidigare:
                        // student.DateOfBirth = DateTime.ParseExact(sf.dateOfBirth.Text, "MM/dd/yyyy", CultureInfo.InvariantCulture);

                        student.DateOfBirth = DateTime.Parse(sf.dateOfBirth.Text);

                        
                        // Enable saving (changes are not made permanent until they are written back to the database)
                        saveChanges.IsEnabled = true;

                        // OO: Sparas inte i databasen (bara i gui't)
                    }
                    break;
            }
        }

        #region Predefined code

        private void studentsList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
 
        }

        // Save changes back to the database and make them permanent
        private void saveChanges_Click(object sender, RoutedEventArgs e)
        {

        }

        #endregion
    }

    [ValueConversion(typeof(string), typeof(Decimal))]
    class AgeConverter : IValueConverter
    {
        // OO: Körs massa gånger
        public object Convert(object value, Type targetType, object parameter,
                              System.Globalization.CultureInfo culture)
        {
            return "";
        }

        #region Predefined code

        public object ConvertBack(object value, Type targetType, object parameter,
                                  System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
