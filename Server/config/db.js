const DB_USER = "admin";
const DB_PASSWORD = "admin01";
const DATABASE = "mongodb://" + DB_USER + ":" + DB_PASSWORD + "@ds145486.mlab.com:45486/dentalappointments"


module.exports = {
    'secret':'meansecure',
    'database': DATABASE
};
