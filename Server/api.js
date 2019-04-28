var express = require('express');
var router = express.Router();
var router = express.Router();
var admin = require('firebase-admin');
var mongoose = require('mongoose');
var firebase = require("firebase");
var nodemailer = require('nodemailer');

/* test */
router.get('/test', function (req, res, next) {
  res.send('Express RESTful API for TOTO app');
});


/*
Mongoose schema
*/
var Schema = mongoose.Schema;

mongoose.set('useFindAndModify', false);

var apts = new Schema(
  {

    id: { type: Number, default: 0 },
    patientId: String,
    date: Date,
    status: Number
  },
  {
    collection: "WillBeSetAfter"
  }
);


/*
Firebase setup
*/
var serviceAccount = require('./dentalmanagersystem-firebase-adminsdk-tdva4-e0d2eef82a.json');

admin.initializeApp({
  credential: admin.credential.cert(serviceAccount),
  databaseURL: "https://dentalmanagersystem.firebaseio.com"
});



var config = {
  apiKey: "AIzaSyA7aJYmLOz8g6Rix2P4hgPaYhleD84xuUI",
  authDomain: "dentalmanagersystem.firebaseapp.com",
  databaseURL: "https://dentalmanagersystem.firebaseio.com",
  projectId: "dentalmanagersystem",
  storageBucket: "dentalmanagersystem.appspot.com",
  messagingSenderId: "958684495286"

};

firebase.initializeApp(config);
var auth = firebase.auth();




/*
Node Emailar Setup
*/
var transporter = nodemailer.createTransport({
  service: 'gmail',
  auth: {
    user: 'NoReplay.DentalManager@gmail.com',
    pass: 'Dental@Use'
  }
});


/*************************************************
* API Methods
*/


/*******************
 * Email
*/
router.post('/email', function (req, res) {

  var bodyRequest = JSON.parse(req.body.Body);

  console.log(bodyRequest);

  var mailOptions = {
    from: 'NoReplay.DentalManager@gmail.com',
    to: bodyRequest.EmailToSend,
    subject: bodyRequest.Header,
    text: bodyRequest.Body
  };

  transporter.sendMail(mailOptions, function (error, info) {

    if (error) {

      return res.json({ success: false, msg: 'Email' });

    } else {

      console.log('Email sent: ' + info.response);

      return res.json({ success: false, msg: 'Email' });
    }

  });

});

/***************
 * Firebase Auth
 */
//reset password
router.post('/resetpass', function (req, res) {

  var bodyRes = JSON.parse(req.body.Body);

  auth.sendPasswordResetEmail(bodyRes.Email).then(function () {

    return res.json({ success: false, msg: 'Email' });

  }).catch(function (error) {

    return res.json({ success: false, msg: 'Email' });

  });

});

//create user
router.post('/newuser', function (req, res) {

  var bodyRes = JSON.parse(req.body.Body);

  admin.auth().createUser({
    email: bodyRes.Email,
    emailVerified: false,
    password: bodyRes.Password,
    displayName: bodyRes.Name,
    disabled: false
  })
    .then(function (userRecord) {

      // See the UserRecord reference doc for the contents of userRecord.
      console.log("Successfully created new user:", userRecord.uid);

      return res.json({ success: true, msg: 'New user Created.' });

    })
    .catch(function (error) {

      console.log("Error creating new user:", error);

      return res.json({ sucess: false, msg: error.Error, code: error.code });

    });
});

// sign in
router.post('/signin', function (req, res) {

  var bodyRes = JSON.parse(req.body.Body);
 
  firebase.auth().signInWithEmailAndPassword(bodyRes.Email, bodyRes.Password)
    .then(function () {

      return res.json({ success: true, msg: "login", code: "400" });
    })
    .catch(function (error) {
      
      console.log("error:" + error);

      if (error) {

        return res.json({ success: false, msg: error.Error, code: error.code });

      }

      return res.json({ success: true, msg: "login", code: "400" });

    });

});

//get user data
router.post('/user', function (req, res) {

  var bodyRes = JSON.parse(req.body.Body);

  admin.auth().getUserByEmail(bodyRes.Email)

    .then(function (userRecord) {

      // See the UserRecord reference doc for the contents of userRecord.
      console.log("Successfully fetched user data:", userRecord.toJSON());

      return res.json(userRecord.toJSON())
    })
    .catch(function (error) {

      console.log("Error fetching user data:", error);

      return res.json({ success: false, msg: error.Error, code: error.code });

    });

});

/*************
 * Appointments
 */

// Create appointment
router.post('/appointment', function (req, res) {

  var bodyRequest = JSON.parse(req.body.Body);


  apts['options'].collection = bodyRequest.Email;

  var App = mongoose.model('User', apts);


  var ap = new App({
    id: bodyRequest.ID,
    patientId: bodyRequest.PatientID,
    date: bodyRequest.Date,
    status: bodyRequest.Status
  });

  ap.save(function (err) {

    if (err) {

      console.log(err);

      return res.json({ success: false });

    }
    return res.json({ success: true });

  });

});

// get apointments week
router.post('/appointmentweek', function (req, res) {

  var bodyRequest = JSON.parse(req.body.Body);

  apts['options'].collection = bodyRequest.Email;

  console.log(bodyRequest.Email);
  var App = mongoose.model('User', apts);

  App.find({ date: { $gte: bodyRequest.StartDate, $lte: bodyRequest.EndDate } }, function (err, appointments) {

    console.log(appointments)
    return res.status(200).json(appointments);

  });

});

// get one appointment 
router.get('/appointment', function (req, res) {

  apts['options'].collection = req.headers.email;

  var App = mongoose.model('User', apts);

  try {
    //get appointment
    App.findById(req.headers.id, function (err, appointment) {

      //not found response
      if (!appointment) {
        res.status(404).send({ success: false, msg: 'User not found.' });
      } else if (err) {

        return res.status(500).json({ success: false, msg: 'Server error' });

      } else {

        res.status(200).json(appointment);
      }
    });
  } catch (Error) {

    res.res.status(500).json({ success: false, msg: 'Server error' });
  }

});

//uptdate appointment
router.put('/appointment', function (req, res) {

  apts['options'].collection = req.headers.email;

  var App = mongoose.model('User', apts);

  var bodyRequest = JSON.parse(req.body.Body);

  App.findByIdAndUpdate(bodyRequest._id, {
    id: bodyRequest.Id,
    patientId: bodyRequest.PatientID,
    status: bodyRequest.Status,
    date: bodyRequest.Date
  },
    function (err, data) {
      if (err) {

        if (err.name = 'CasteError') {

          res.status(400).send({ success: false, msg: 'Wrong user id.' });

        } else {

          res.res.status(500).send({ success: false, msg: 'Server error' });

        }

      } else {

        res.status(200).json({ success: true, msg: 'Updated' });
      }

    });
});






//reset email

module.exports = router;
