**Availability to the public**

* [Windows Store](https://www.microsoft.com/en-us/p/dental-clinic-manager/9p6gj9bftjv4?rtc=1),  Release Version 3.


**Minimum Requirements**

* Windows 10
* Internet
* Mouse
* Keyboard
* 1G of data
* 1G of RAM

**Technologies**
* [Firebase](https://firebase.google.com/), to store data remotely in real time and for authentication.
* [SQlite](https://www.sqlite.org/index.html), to store data locally
* [MongoDB](https://www.mongodb.com/what-is-mongodb), to store data remotely(It's a document database and more suited for appointments than Firebase)
* Visual Studio 2017,  as development platform
* [NodeJS](https://nodejs.org/en/) to run the server

**Plugins**
* FirebaseDatabase.net,    Version 4.0.1, use for Firebase
* Microsoft.Data.Sqlite.Core,    Version 2.2.1, use SQLite
* Microsoft.Toolkit.Uwp.UI.Controls.DataGrid,    Version 5.1.0,  use to design the GUI

**Architecture**
* 64
* 86
* ARM




**First Time User**

* Launch the application and go to the login page.
* Click the "Account Management" link.
* Go to the account management page and click "Create new Account" link.
* In the new account page, fill the name, email, password, confirm fields and click the "Create" button.

![New User](https://github.com/JoseIgnacioRetamalThomsen/DentalManagerSystem/blob/Third_Sprint/Images/Screenshots/NewAccount.JPG)

**Synchronize Account**

* After you create an account.
* Launch the application and go to the login page.
* Click the "Account Management" link.
* Go to the account management page and click "Synchronize Account" link.
* In the synchronize account page, fill the email, password, and click the "Synchronization" button.

![Synchronization](https://github.com/JoseIgnacioRetamalThomsen/DentalManagerSystem/blob/Third_Sprint/Images/Screenshots/AccountSyncronization.JPG)

**Login**

* Launch the application and go to the login page.
* Click the "Account Management" link.
* Go to the account management page and click "Reset Password using email..." link.
* In the reset password page, fill the name, email, password, confirm fields and click the "Create" button.

![Login](https://github.com/JoseIgnacioRetamalThomsen/DentalManagerSystem/blob/Third_Sprint/Images/Screenshots/LoginPage.JPG)


**Password Recovery**

* Launch the application and go to the login page.
* Click the "Account Management" link.
* Go to the account management page and click "Password Recovery.." link.
* In the password recovery page, enter your email  and click the "Send" button.
* Go to your email and click the link sent to you.
* In the link enter the new password and click "Save"

![Password Recovery](https://github.com/JoseIgnacioRetamalThomsen/DentalManagerSystem/blob/Third_Sprint/Images/Screenshots/RecoverPasswordReset.PNG)

**Homepage**

* Launch the application, login and go to the homepage.
* Click on the "Patient List" button to refresh the patients display to the screen.
* Click on the "Treatment List" button to see the treatments.
* Click on the "Appointments" button to go to the appointment page.
* Click on the "Income Report" button to go to the income report page.
* Click on the "Settings" button to go to the settings page.

![Homepage](https://github.com/JoseIgnacioRetamalThomsen/DentalManagerSystem/blob/Third_Sprint/Images/Screenshots/Homepage.JPG)

**Patients List**

* Launch the application, login and go to the homepage.
* Click the "Patient Lists" button to refresh page.
* Click on the "New Patients " to add a new patient.
* search the patient by name or surname in the search field.
* Select a patient
* Click on the "Add Payment" button to add a new payment.
* Click on the "Edit" button to go to edit the patients details.


![Patient](https://github.com/JoseIgnacioRetamalThomsen/DentalManagerSystem/blob/Third_Sprint/Images/Screenshots/PatientPage.JPG)

**Treatments List**

* Launch the application, login and go to the homepage.
* Click the "Treatment Lists" to see the treatments.

![Treatmentlist](https://github.com/JoseIgnacioRetamalThomsen/DentalManagerSystem/blob/Third_Sprint/Images/Screenshots/TreatmentPage.JPG)


* Click the "Add New" button to add a new treatment.
* Enter the Treatment name and price and the then click on the "Add New" button to to add the treatment.

![AddNewTreatmentPlan](https://github.com/JoseIgnacioRetamalThomsen/DentalManagerSystem/blob/Third_Sprint/Images/Screenshots/AddTreatment.JPG)


* Select a treatment plan.
* Click on the "Edit" button to go to edit the treatment plan.
* Click on the "Save" button to go to save the treatment plan.

![EditTreatmentPlan](https://github.com/JoseIgnacioRetamalThomsen/DentalManagerSystem/blob/Third_Sprint/Images/Screenshots/EditTreatmentPlan.JPG)

**Appointments**

* Launch the application, login and go to the homepage.
* Click the "Appointments" button to see the appointment schedules.
* Select the date picker label "Select Week" to see the appointments for a particular week.
* To book an appointment, go to the patient page, select the patient and click on the "New Appointment" button, which will take you automatically to the appointment page.
*In the appointment page select the week from the date picker and select the time slot from the schedule.
*Then click the "Create Appointment" button to create a new appointment.

![CreateAppointments](https://github.com/JoseIgnacioRetamalThomsen/DentalManagerSystem/blob/Third_Sprint/Images/Screenshots/CreateAppointment.JPG)


* To cancel an appointment, select the patient's name and click on the "Cancel Appointment" Button.

![Appointments](https://github.com/JoseIgnacioRetamalThomsen/DentalManagerSystem/blob/Third_Sprint/Images/Screenshots/Appointments.JPG)

**Income Report**

* Launch the application, login and go to the homepage.
* Click the "Income Report" button to go to the income report page.
* You can either see the income for the Date(the amount end before and including the selected date) or  the period(the sum amount for the period, day, month, year or all).
* Select a date form the date picker and the sum amount will be shown in the amount field below .
* Select a period from the periods drop down list and the amount will be shown below in the amount field.


![Income Report](https://github.com/JoseIgnacioRetamalThomsen/DentalManagerSystem/blob/Third_Sprint/Images/Screenshots/IncomeReport.JPG)

**Settings**

* Launch the application, login and go to the homepage.
* Click the "Settings" button at the bottom left of the page to go to the setting page.
* Click the button "Administrator Address" to change/update the user's address.
* In the User address page, click the button "EDIT" first, then file the entry fields, and click the Button "Save".
* A window will pop out to tell you, your address was successfully updated.

![UpdateUserAddress](https://github.com/JoseIgnacioRetamalThomsen/DentalManagerSystem/blob/Third_Sprint/Images/Screenshots/UpdateUserAddress.JPG)


* Click the button "Change Password" to change/reset the password which will take you to the reset password page which is explain above.
* Select the working mode online/offline, if you select offline, you can't book an appointment or write to Firebase .
* Select a login mode on/off, if you select offline, you wouldn't be prompted to login each time you launch the application.
* Click the developers name(Mark & Jose) to go their respective GitHub account.
* Click the "Give Feedback" button to send a feedback to the developers.

![Settings](https://github.com/JoseIgnacioRetamalThomsen/DentalManagerSystem/blob/Third_Sprint/Images/Screenshots/SettingPage.JPG)

**Treatment Plan**

* Launch the application, login and go to the homepage.
* Click  "Treatment lists" and create a treatment as explain above.
* Click "Patient List" button, select a patient and and you will be taken to the select patient page.
* Click "Treatment plan" button to add a new treatment and you will be taken to the add new treatment page.
* In the add new treatment page fill the entry fields, then click the "Add" button to add it to the list and then the  "Create" button to create the treatment plan.
* "After you create the Treatment plan, the budget estimation will be display.
* Then click the "Email" button to send it by email or "Print" to print it, or "Email&Print" to do both or "Done" to go back.


![Selected patient](https://github.com/JoseIgnacioRetamalThomsen/DentalManagerSystem/blob/Third_Sprint/Images/Screenshots/SelectPatient.JPG)

![CreateTreatment](https://github.com/JoseIgnacioRetamalThomsen/DentalManagerSystem/blob/Third_Sprint/Images/Screenshots/CreateTreatmentPlan.JPG)


![Receipt](https://github.com/JoseIgnacioRetamalThomsen/DentalManagerSystem/blob/Third_Sprint/Images/Screenshots/Receipt.JPG)

**Change the status of Treatment Plan**

* Launch the application, login and go to the homepage.
* Go to the selected treatment plan as explain above.
* Select a plan from one of the categories, Active treatment plan, Created treatment plans or Complete treatment plan and you will be taken to update treatment plan status page.
* Here you can select either Create, Accepted or Complete from a drop down list to update the status of the plan.
* You can also click on the tick button to mark the plan as complete or the minus sign to mark the plan as incomplete.

![Changetreatmentplan1](https://github.com/JoseIgnacioRetamalThomsen/DentalManagerSystem/blob/Third_Sprint/Images/Screenshots/ChangeStatusTreatmentPlan1.JPG)

![ChangeTreatmentplan](https://github.com/JoseIgnacioRetamalThomsen/DentalManagerSystem/blob/Third_Sprint/Images/Screenshots/ChangeStatusTreatmentPlan.JPG)

**Add Payment**

* Launch the application and go to the login page.
* Click on the Patient list to refresh it.
* Select a patient and click on the "New Payment" button and you will be redirected to the payment page.
* Fill the require fields and click the "Add" button.

![AddPayment](https://github.com/JoseIgnacioRetamalThomsen/DentalManagerSystem/blob/Third_Sprint/Images/Screenshots/AddPaymentPage.JPG)

The main features for the application are:

* Database.
   We use 3 dattabses to store data. SQLite and Firebase to store the patients and users records and Mongodb to store the appointment details.
* Database backup Option.
  In the setting page users can choose to back up the database at any time.
* Databases synchronization.
  We synchronize data on Firebase and SQlite to provide consisted data to users in case of an internet interruption or if the switch from one device to another.
* Account Validation.
A link is sent to the users' email to validate their account upon creating anew account
* Secure login.
Users can login with their email and an encrypted password.
* Password recovery.
Users can recover their password in case they forget.
* Account Synchronization.
 Users are able to change the password.
* User Guide.
There is a link which a well documented guide on how to use and navigate in the application. 
* Mobile app?
 Appointment on a mobile device, to be done.
* Print budget estimation.
Use can print the budget estimation.
* Send budget estimation by email to patients.
Users can send the budget estimation to the patients.
* Income Report
Users can see their income per day, Month, Year...ect.


![Firebase](https://github.com/JoseIgnacioRetamalThomsen/DentalManagerSystem/blob/Third_Sprint/Images/Screenshots/Firebase.JPG)


![SQLIte](https://github.com/JoseIgnacioRetamalThomsen/DentalManagerSystem/blob/Third_Sprint/Images/Screenshots/SQlite.JPG)
